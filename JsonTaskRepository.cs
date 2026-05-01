using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimBioseTasks
{
    /// <summary>
    /// Implementação de <see cref="ITaskRepository"/> que persiste tarefas em ficheiro JSON.
    /// Toda a lógica de I/O que antes residia no Model foi movida para aqui,
    /// respeitando o Princípio da Responsabilidade Única (SRP):
    /// o Model gere dados e regras de negócio; este repositório trata exclusivamente
    /// de ler e escrever ficheiros.
    /// Substituir esta classe por outra implementação (ex: SqlTaskRepository)
    /// não requer qualquer alteração ao Model.
    /// </summary>
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="JsonTaskRepository"/>.
        /// </summary>
        /// <param name="filePath">Caminho para o ficheiro JSON de persistência.</param>
        public JsonTaskRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("O caminho do ficheiro não pode ser nulo ou vazio.", nameof(filePath));

            _filePath = filePath;
        }

        /// <inheritdoc/>
        public List<BaseTask> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<BaseTask>();

                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json))
                    return new List<BaseTask>();

                return JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao carregar tasks.json", ex);
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<BaseTask> tasks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Pasta de destino não encontrada.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"O ficheiro '{_filePath}' está aberto por outro aplicativo.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Sem permissão para gravar o ficheiro '{_filePath}'.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro crítico ao guardar o ficheiro '{_filePath}'.", ex);
            }
        }
    }
}
