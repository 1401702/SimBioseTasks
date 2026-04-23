using System;
using System.Collections.Generic;
using System.Text;

namespace SimBioseTasks
{
    /// <summary>
    /// Define os tipos de operação que podem ser solicitados sobre uma tarefa.
    /// No contexto do padrão MVC, este enumerador permite representar de forma
    /// clara (tipo) a intenção da ação enviada pela View ao Controller,
    /// evitando o uso de strings e reduzindo erros de interpretação.
    /// anteriormente eram commandos em string.
    /// </summary>
    public enum TaskOp
    {
        /// <summary>
        /// Indica a criação de uma nova tarefa.
        /// </summary>
        Create,

        /// <summary>
        /// Comando atualização de uma tarefa.
        /// </summary>
        Update,

        /// <summary>
        /// Comando delete de uma tarefa.
        /// </summary>
        Delete,

        /// <summary>
        /// Comando Save de uma tarefa ???.
        /// talvez separar a persistência da criação.
        /// </summary>
        Save,

        /// <summary>
        /// Comando Confirm de uma tarefa .
        /// Pode ser usada para validar ou confirmar alterações antes de as aplicar no Model???.
        /// </summary>
        Confirm
    }

    /// <summary>
    /// Representa um comando de operação sobre uma tarefa.
    /// No padrão MVC, esta classe é necessária para transportar da View para o Controller,
    /// e posteriormente para o Model, a ação pedida pelo utilizador juntamente com os dados
    /// da tarefa a processar, mantendo as camadas desacopladas.
    /// Desta forma, a View não executa regras de negócio diretamente e o Controller
    /// passa a trabalhar com um objeto de mensagem bem definido.
    /// </summary>
    public class OperTask
    {
        /// <summary>
        /// Obtém ou define o tipo de operação a executar sobre a tarefa.
        /// Esta propriedade permite ao Controller interpretar a intenção enviada pela View
        /// e encaminhá-la corretamente para o Model.
        /// </summary>
        public TaskOp Operation { get; set; }

        /// <summary>
        /// Obtém ou define a tarefa associada à operação.
        /// Esta propriedade contém os dados que o Model irá criar, atualizar ou remover,
        /// conforme a operação indicada.
        /// </summary>
        public BaseTask Task { get; set; }
    }
}