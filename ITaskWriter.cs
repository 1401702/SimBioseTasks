namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato responsável pelas operações de escrita de tarefas.
    /// </summary>
    public interface ITaskWriter
    {
        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="task">Tarefa a criar.</param>
        void Create(BaseTask task);

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="task">Tarefa com os novos dados.</param>
        void Update(BaseTask task);

        /// <summary>
        /// Remove uma tarefa existente a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa a remover.</param>
        void Delete(int id);
    }
}