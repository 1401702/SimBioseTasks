using Newtonsoft.Json;
using SimBioseTasks;

/// <summary>
/// Model do MVC
/// </summary>
public class Model : ITaskModel
{
    private const string FileName = "tasks.json";
    private List<BaseTask> _tasks;

    /// <summary>
    /// Obtém a lista de tarefas em modo de leitura.
    /// </summary>
    public IReadOnlyList<BaseTask> Tasks => _tasks;

    /// <summary>
    /// Ocorre quando a coleção de tarefas é alterada.
    /// </summary>
    public event EventHandler<OperTaskEventArgs>? TasksChanged;

    /// <summary>
    /// Inicializa uma nova instância do modelo e carrega as tarefas do ficheiro JSON.
    /// </summary>
    public Model()
    {
        _tasks = new List<BaseTask>();
        LoadTasksFromJson();
    }

    /// <summary>
    /// Carrega as tarefas a partir do ficheiro JSON para memória.
    /// </summary>
    /// <returns>
    /// Devolve true se o carregamento foi efetuado com sucesso;
    /// devolve false se o ficheiro não existir ou estiver vazio.
    /// </returns>
    private bool LoadTasksFromJson()
    {
        if (!File.Exists(FileName))
        {
            _tasks = new List<BaseTask>();
            return false;
        }

        string json = File.ReadAllText(FileName);

        if (string.IsNullOrWhiteSpace(json))
        {
            _tasks = new List<BaseTask>();
            return false;
        }

        _tasks = JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
        return true;
    }

    /// <summary>
    /// Guarda a lista atual de tarefas no ficheiro JSON.
    /// </summary>
    private void SaveTasks()
    {
        string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
        File.WriteAllText(FileName, json);
    }

    /// <summary>
    /// Executa uma operação sobre uma tarefa com base no comando recebido.
    /// </summary>
    /// <param name="operTask">
    /// Objeto que contém a operação a executar e a tarefa associada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Lançada quando o comando ou a tarefa são nulos.
    /// </exception>
    public void Execute(OperTask operTask)
    {
        if (operTask == null)
            throw new ArgumentNullException(nameof(operTask));

        if (operTask.Task == null)
            throw new ArgumentNullException(nameof(operTask.Task));

        switch (operTask.Operation)
        {
            case TaskOp.Create:
                Create(operTask.Task);
                break;

            case TaskOp.Update:
                Update(operTask.Task);
                break;

            case TaskOp.Delete:
                Delete(operTask.Task);
                break;
        }
    }

    /// <summary>
    /// Cria uma nova tarefa, atribui um identificador único,
    /// guarda os dados e notifica a alteração.
    /// </summary>
    /// <param name="task">Tarefa a criar.</param>
    private void Create(BaseTask task)
    {
        ValidateTask(task);
        task.Id = GetNextId();
        _tasks.Add(task);
        SaveTasks();
        OnTasksChanged(new OperTask { Operation = TaskOp.Create, Task = task });
    }

    /// <summary>
    /// Atualiza os dados de uma tarefa existente,
    /// guarda as alterações e notifica a alteração.
    /// </summary>
    /// <param name="task">Tarefa com os novos dados.</param>
    private void Update(BaseTask task)
    {
        ValidateTask(task);

        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing == null) return;

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.IsCompleted = task.IsCompleted;

        SaveTasks();
        OnTasksChanged(new OperTask { Operation = TaskOp.Update, Task = existing });
    }

    /// <summary>
    /// Remove uma tarefa existente,
    /// guarda as alterações e notifica a alteração.
    /// </summary>
    /// <param name="task">Tarefa a remover.</param>
    private void Delete(BaseTask task)
    {
        if (task.Id == null) return;

        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing == null) return;

        _tasks.Remove(existing);
        SaveTasks();
        OnTasksChanged(new OperTask { Operation = TaskOp.Delete, Task = existing });
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
    /// Valida os dados de uma tarefa antes de serem persistidos.
    /// </summary>
    /// <param name="task">Tarefa a validar.</param>
    /// <exception cref="ArgumentNullException">
    /// Lançada quando a tarefa é nula.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Lançada quando o título da tarefa está vazio ou inválido.
    /// </exception>
    private void ValidateTask(BaseTask task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Task title is required.");
    }

    /// <summary>
    /// Dispara o evento de alteração de tarefas.
    /// </summary>
    /// <param name="operTask">
    /// Informação sobre a operação executada e a tarefa afetada.
    /// </param>
    protected virtual void OnTasksChanged(OperTask operTask)
    {
        TasksChanged?.Invoke(this, new OperTaskEventArgs(operTask));
    }
}