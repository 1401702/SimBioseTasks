using System;
using System.Collections.Generic;
using System.Text;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa os dados associados ao evento de alteração de tarefas.
    /// No contexto do padrão MVC, esta classe é usada pelo Model para notificar o Controller
    /// de que ocorreu uma operação sobre uma tarefa, sem criar dependência direta entre as camadas.
    /// Ao herdar de <see cref="EventArgs"/>, segue o padrão de eventos do .NET e permite
    /// transportar informação sobre a operação executada de forma estruturada e desacoplada.
    /// </summary>
    public class OperTaskEventArgs : EventArgs
    {
        /// <summary>
        /// Obtém os dados da operação efetuada sobre a tarefa.
        /// Esta propriedade permite ao Controller saber que ação foi realizada pelo Model
        /// e qual a tarefa envolvida, para depois decidir como atualizar a View.
        /// </summary>
        public OperTask OperationData { get; }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="OperTaskEventArgs"/>.
        /// No padrão MVC, este construtor permite encapsular os dados da operação
        /// que o Model irá enviar ao Controller através de um evento.
        /// </summary>
        /// <param name="operationData">
        /// Objeto que contém a operação realizada e a tarefa associada.
        /// </param>
        public OperTaskEventArgs(OperTask operationData)
        {
            OperationData = operationData;
        }
    }
}