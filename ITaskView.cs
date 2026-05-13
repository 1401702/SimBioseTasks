using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato da View da aplicação.
    /// Expõe eventos de interação do utilizador e métodos de atualização da interface.
    /// </summary>
    public interface ITaskView
    {
        /// <summary>
        /// Ocorre quando o utilizador pede o save de uma nova tarefa.
        /// </summary>
        event Action<BaseTask>? OnSaveRequest;

        /// <summary>
        /// Ocorre quando o utilizador pede a remoção de uma tarefa existente.
        /// </summary>
        event Action<int>? OnDeleteRequest;

        /// <summary>
        /// Carrega na interface a coleção de tarefas recebida do model.
        /// </summary>
        /// <param name="tasks">Coleção de tarefas a apresentar.</param>
        void LoadTasks(IReadOnlyList<BaseTask> tasks);

        /// <summary>
        /// Mostra uma mensagem de erro ao utilizador.
        /// </summary>
        /// <param name="message">Mensagem de erro a apresentar.</param>
        void ShowError(string message);

        /// <summary>
        /// Devolve a instância concreta do formulário para arranque da aplicação.
        /// Sem este metodo não se conseguia arrancar o form View
        /// </summary>
        /// <returns>A instância do formulário associado à view.</returns>
        Form GetForm();
    }
}