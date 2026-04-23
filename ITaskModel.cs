using System;
using System.Collections.Generic;
using System.Text;

namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato do Model no padrão MVC.
    /// Esta interface existe para desacoplar o Controller da implementação concreta
    /// do modelo de dados, permitindo que a lógica de negócio, a persistência e a
    /// gestão de tarefas fiquem isoladas da interface gráfica.
    /// No contexto de MVC, esta separação é necessária para facilitar manutenção,
    /// reutilização, testes e evolução da aplicação sem alterar a View ou o Controller.
    /// </summary>
    public interface ITaskModel
    {
        /// <summary>
        /// Evento disparado sempre que o estado das tarefas é alterado.
        /// Permite ao Controller ser notificado de mudanças no Model e atualizar a View,
        /// sem que o Model conheça diretamente a interface gráfica.
        /// </summary>
        event EventHandler<OperTaskEventArgs>? TasksChanged;

        /// <summary>
        /// Obtém a coleção atual de tarefas em modo de leitura.
        /// Esta propriedade permite ao Controller consultar os dados do Model
        /// sem expor diretamente a estrutura interna de armazenamento.
        /// </summary>
        IReadOnlyList<BaseTask> Tasks { get; }

        /// <summary>
        /// Executa uma operação de negócio sobre uma tarefa.
        /// Este método centraliza no Model a criação, atualização e remoção de tarefas,
        /// garantindo que as regras de negócio e persistência fiquem fora da View e do Controller,
        /// conforme a separação de responsabilidades exigida pelo padrão MVC.
        /// </summary>
        /// <param name="operTask">
        /// Objeto que contém a operação a executar e a tarefa associada.
        /// </param>
        void Execute(OperTask operTask);
    }
}