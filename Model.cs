using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimBioseTasks
{
    public class Model
    {
        private const string FileName = "tasks.json";
        private List<BaseTask> _tasks;

        public IReadOnlyList<BaseTask> Tasks => _tasks;
        public Model()
        {
            _tasks = new List<BaseTask>();
            //LoadTasksFromJson();
        }
        public void getAllTasks()
        {
            bool _state = LoadTasksFromJson();
        }

        public bool LoadTasksFromJson()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    _tasks = JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _tasks = new List<BaseTask>();
                return false;
            }
        }
        public void SaveTasks()
        {
            string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
        public void AddTask(BaseTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            var maxId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) : 0;
            task.Id = maxId + 1;
            _tasks.Add(task);
            SaveTasks();
        }
        public void UpdateTask(BaseTask updatedTask)
        {
            if (updatedTask == null) throw new ArgumentNullException(nameof(updatedTask));

            BaseTask exists = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (exists != null)
            {
                // Copia os valores da tarefa alterada para o item existente
                exists.Title = updatedTask.Title;
                exists.Description = updatedTask.Description;
                exists.IsCompleted = updatedTask.IsCompleted;


                SaveTasks(); // grava imediatamente em disk
                exists = null;
            }
        }
        public void DeleteTask(int id)
        {
            BaseTask task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                SaveTasks();
            }
        }
        /// <summary>
        ///  Devolve a tarefa com o Id dado, ou null se não existir.
        /// </summary>
        public BaseTask GetTaskById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }
        /// <summary>
        ///  Devolve uma cópia da lista de tarefas.
        /// </summary>
        public List<BaseTask> GetTasks()
        {
            // não sei se deve ser por metodo ou ir diretamente 
            // a _tasks que é a lista
            return _tasks.ToList();
        }

    }
}
