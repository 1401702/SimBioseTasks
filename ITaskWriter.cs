namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato responsável pelas operações de escrita de tarefas.
    /// </summary>
    public interface ITaskWriter
    {
        /// <summary>
        /// Guarda uma tarefa.
        /// Se a tarefa não tiver identificador(ID), é criada;
        /// caso contrário, é atualizada.
        /// </summary>
        /// <param name="task">Tarefa a guardar.</param>
        void Save(BaseTask task);

        /// <summary>
        /// Remove uma tarefa existente a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa a remover.</param>
        void Delete(int id);
    }
}