using System;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa uma tarefa base da aplicação.
    /// Contém os dados essenciais usados pelo model, controller e view.
    /// </summary>
    public class BaseTask
    {
        /// <summary>
        /// Obtém ou define o identificador único da tarefa.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Obtém ou define o título da tarefa.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a descrição da tarefa.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define um valor que indica se a tarefa está concluída.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}