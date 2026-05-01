using System.Collections.Generic;

namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato de persistência de tarefas.
    /// Ao programar para esta interface em vez de uma implementação concreta,
    /// o Model torna-se independente do meio de armazenamento (ficheiro, base de dados,
    /// memória para testes, etc.), fomentando a reutilização sem alteração de código.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Carrega e devolve todas as tarefas persistidas.
        /// </summary>
        List<BaseTask> Load();

        /// <summary>
        /// Persiste a coleção de tarefas fornecida.
        /// </summary>
        /// <param name="tasks">Coleção de tarefas a guardar.</param>
        void Save(IEnumerable<BaseTask> tasks);
    }
}
