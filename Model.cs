using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o Model da aplicação.
    /// É responsável por gerir a coleção de tarefas em memória,
    /// persistir os dados em ficheiro JSON e notificar alterações aos subscritores.
    /// </summary>
    public class Model : ITaskBase
    {
        /// <summary>
        /// Nome do ficheiro onde as tarefas são guardadas.
        /// </summary>
        private static readonly string FileName = "tasks.json";

        /// <summary>
        /// Lista interna de tarefas mantida em memória.
        /// </summary>
        private List<BaseTask> _tasks;

        /// <summary>
        /// Ocorre quando a lista de tarefas é alterada.
        /// Envia para os subscritores a coleção atual em modo apenas de leitura.
        /// </summary>
        public event Action<IReadOnlyList<BaseTask>>? OnTasksChanged;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Model"/>.
        /// </summary>
        public Model()
        {
            _tasks = new List<BaseTask>();
        }

        /// <summary>
        /// Inicializa o model carregando as tarefas persistidas em ficheiro
        /// e notificando a interface com o estado atual.
        /// </summary>
        public void Initialize()
        {
            LoadTasksFromJson();
            NotifyTasksChanged();
        }

        /// <summary>
        /// Obtém todas as tarefas atualmente carregadas.
        /// </summary>
        /// <returns>Uma coleção apenas de leitura com todas as tarefas.</returns>
        public IReadOnlyList<BaseTask> GetAll()
        {
            return _tasks.AsReadOnly();
        }

        /// <summary>
        /// Obtém uma tarefa a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa.</param>
        /// <returns>A tarefa encontrada, ou <c>null</c> se não existir.</returns>
        public BaseTask? GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Cria uma nova tarefa, atribui-lhe um identificador,
        /// guarda a alteração em ficheiro e notifica os subscritores.
        /// </summary>
        /// <param name="task">Tarefa a criar.</param>
        /// <exception cref="ArgumentNullException">Lançada quando a tarefa é nula.</exception>
        /// <exception cref="Exception">Lançada quando os dados da tarefa são inválidos.</exception>
        private void Create(BaseTask task)
        {
            ValidateTask(task, requireId: false);

            task.Id = GetNextId();
            _tasks.Add(task);

            SaveTasks();
            NotifyTasksChanged();
        }

        /// <summary>
        /// Atualiza uma tarefa existente, guarda a alteração em ficheiro
        /// e notifica os subscritores.
        /// </summary>
        /// <param name="task">Tarefa com os novos dados.</param>
        /// <exception cref="ArgumentNullException">Lançada quando a tarefa é nula.</exception>
        /// <exception cref="Exception">Lançada quando os dados da tarefa são inválidos.</exception>
        /// <exception cref="InvalidOperationException">Lançada quando a tarefa não existe.</exception>
        private void Update(BaseTask task)
        {
            ValidateTask(task, requireId: true);

            BaseTask? existing = _tasks.FirstOrDefault(t => t.Id == task.Id);

            if (existing == null)
                throw new InvalidOperationException($"Tarefa com ID {task.Id} não existe.");

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;

            SaveTasks();
            NotifyTasksChanged();
        }
        
        /// <summary>
        /// Cria ou atualiza uma tarefa existente, guarda a alteração em ficheiro
        /// </summary>
        /// <param name="task">Tarefa com os novos dados.</param>
        public void Save(BaseTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Tarefa inválida.");

            if (task.Id == null)
            {
                Create(task);
                return;
            }

            Update(task);
        }

        /// <summary>
        /// Remove uma tarefa pelo identificador, guarda a alteração em ficheiro
        /// e notifica os subscritores.
        /// </summary>
        /// <param name="id">Identificador da tarefa a remover.</param>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando o identificador é inválido ou a tarefa não existe.
        /// </exception>
        public void Delete(int id)
        {
            if (id <= 0)
                throw new InvalidOperationException("ID inválido para remoção.");

            BaseTask? existing = _tasks.FirstOrDefault(t => t.Id == id);

            if (existing == null)
                throw new InvalidOperationException($"Tarefa com ID {id} não existe.");

            _tasks.Remove(existing);

            SaveTasks();
            NotifyTasksChanged();
        }

        /// <summary>
        /// Notifica os subscritores de que a lista de tarefas foi alterada.
        /// </summary>
        private void NotifyTasksChanged()
        {
            OnTasksChanged?.Invoke(_tasks.AsReadOnly());
        }

        /// <summary>
        /// Carrega as tarefas a partir do ficheiro JSON.
        /// Se o ficheiro não existir ou estiver vazio, é criada uma lista vazia.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Lançada quando ocorre um erro durante a leitura ou desserialização do ficheiro.
        /// </exception>
        private void LoadTasksFromJson()
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    _tasks = new List<BaseTask>();
                    return;
                }

                string json = File.ReadAllText(FileName);

                if (string.IsNullOrWhiteSpace(json))
                {
                    _tasks = new List<BaseTask>();
                    return;
                }

                _tasks = JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
            }
            catch (Exception ex)
            {
                _tasks = new List<BaseTask>();
                throw new InvalidOperationException("Erro ao carregar tasks.json", ex);
            }
        }

        /// <summary>
        /// Guarda a lista atual de tarefas no ficheiro JSON.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">
        /// Lançada quando a pasta de destino não existe.
        /// </exception>
        /// <exception cref="IOException">
        /// Lançada quando o ficheiro está em uso por outro processo.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Lançada quando não existem permissões para gravar no ficheiro.
        /// </exception>
        /// <exception cref="Exception">
        /// Lançada em caso de erro crítico inesperado.
        /// </exception>
        private void SaveTasks()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
                File.WriteAllText(FileName, json);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Pasta de destino não encontrada.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("O ficheiro tasks.json está aberto por outro aplicativo.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Sem permissão para gravar o ficheiro tasks.json.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro crítico ao guardar o ficheiro tasks.json.", ex);
            }
        }

        /// <summary>
        /// Calcula o próximo identificador disponível para uma nova tarefa.
        /// </summary>
        /// <returns>O próximo identificador inteiro disponível.</returns>
        private int GetNextId()
        {
            if (_tasks.Count == 0)
                return 1;

            return _tasks.Max(t => t.Id ?? 0) + 1;
        }

        /// <summary>
        /// Valida os dados de uma tarefa antes de a criar ou atualizar.
        /// </summary>
        /// <param name="task">Tarefa a validar.</param>
        /// <param name="requireId">
        /// Indica se a validação deve exigir um identificador válido.
        /// </param>
        /// <exception cref="ArgumentNullException">Lançada quando a tarefa é nula.</exception>
        /// <exception cref="Exception">Lançada quando os dados da tarefa são inválidos.</exception>
        private void ValidateTask(BaseTask task, bool requireId = false)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Tarefa inválida.");

            if (string.IsNullOrWhiteSpace(task.Title))
                throw new Exception("O título é obrigatório mas está em branco.");

            if (task.Title.Length < 3)
                throw new Exception("Título muito pequeno (mínimo 3 caráteres).");

            if (requireId && (task.Id == null || task.Id <= 0))
                throw new Exception("ID inválido.");
        }
    }
}