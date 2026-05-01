using SimBioseTasks;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Representa o Model da aplicação no padrão MVC.
/// É responsável por gerir os dados e aplicar regras de negócio.
/// A persistência é delegada a um <see cref="ITaskRepository"/>, injetado
/// no construtor — o Model não sabe nem precisa de saber como ou onde os dados
/// são guardados, tornando-o independente e reutilizável com qualquer backend.
/// </summary>
public class Model
{
    private readonly ITaskRepository _repository;

    private List<BaseTask> _tasks;

    /// <summary>
    /// Obtém a coleção atual de tarefas em modo de leitura.
    /// </summary>
    public IReadOnlyList<BaseTask> Tasks => _tasks;

    /// <summary>
    /// Evento emitido pelo Model quando ocorre uma alteração nas tarefas.
    /// Permite ao Controller ser notificado sem dependência direta da View.
    /// </summary>
    public event Action<OperTask>? OnModelEvent;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Model"/>.
    /// Recebe o repositório por injeção de dependência, ficando independente
    /// de qualquer implementação concreta de persistência.
    /// </summary>
    /// <param name="repository">Repositório responsável por carregar e guardar tarefas.</param>
    public Model(ITaskRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _tasks = _repository.Load();
    }

    /// <summary>
    /// Trata os pedidos emitidos pelo Controller.
    /// Recebe uma operação sobre uma tarefa e encaminha-a para o método interno adequado.
    /// </summary>
    /// <param name="operTask">
    /// Objeto que contém a operação pedida e a tarefa associada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Lançada quando a operação ou a tarefa são nulas.
    /// </exception>
    public void EventOnController(OperTask operTask)
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

            case TaskOp.Save:
                break;

            case TaskOp.Confirm:
                break;
        }
    }

    /// <summary>
    /// Cria uma nova tarefa, atribui-lhe um identificador,
    /// persiste os dados e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa a criar.</param>
    private void Create(BaseTask task)
    {
        ValidateTask(task, requireId: false);
        task.Id = GetNextId();
        _tasks.Add(task);
        _repository.Save(_tasks);
        OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Create, Task = task });
    }

    /// <summary>
    /// Atualiza uma tarefa existente, persiste a alteração
    /// e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa com os novos dados.</param>
    private void Update(BaseTask task)
    {
        ValidateTask(task, requireId: true);

        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);

        if (existing == null)
            throw new InvalidOperationException($"Tarefa com ID {task.Id} não existe.");

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.IsCompleted = task.IsCompleted;

        _repository.Save(_tasks);
        OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Update, Task = existing });
    }

    /// <summary>
    /// Remove uma tarefa existente, persiste a alteração
    /// e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa a remover.</param>
    private void Delete(BaseTask task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (task.Id == null || task.Id <= 0)
            throw new InvalidOperationException("ID inválido para remoção.");

        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing == null)
            throw new InvalidOperationException($"Tarefa com ID {task.Id} não existe.");

        _tasks.Remove(existing);
        _repository.Save(_tasks);
        OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Delete, Task = existing });
    }

    /// <summary>
    /// Calcula o próximo identificador disponível para uma nova tarefa.
    /// </summary>
    /// <returns>
    /// O próximo identificador inteiro disponível.
    /// </returns>
    private int GetNextId()
    {
        if (_tasks.Count == 0)
            return 1;

        return _tasks.Max(t => t.Id ?? 0) + 1;
    }

    /// <summary>
    /// Valida os dados de uma tarefa antes da sua persistência.
    /// </summary>
    /// <param name="task">Tarefa a validar.</param>
    /// <returns>
    /// Devolve true se a tarefa for válida; caso contrário, devolve false.
    /// </returns>
    private void ValidateTask(BaseTask task, bool requireId = false)
    {
        if (task == null)
            throw new Exception("Tarefa inválida.");

        if (string.IsNullOrWhiteSpace(task.Title))
            throw new Exception("O título é obrigatório mas está em branco.");

        if (requireId && (task.Id == null || task.Id <= 0))
            throw new Exception("ID inválido.");

        if (task.Title.Length < 3)
            throw new Exception("Título muito pequeno (mínimo 3 caráteres).");
    }
}