using System;
using System.Collections.Generic;

namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato responsável pela notificação de alterações na lista de tarefas.
    /// </summary>
    public interface ITaskNotifier
    {
        /// <summary>
        /// Ocorre quando a lista de tarefas é alterada.
        /// </summary>
        event Action<IReadOnlyList<BaseTask>>? OnTasksChanged;
    }
}