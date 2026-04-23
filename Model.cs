using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimBioseTasks
{
    public class Model
    {
        #region Fields and Properties

        private const string FileName = "tasks.json";
        private List<BaseTask> _tasks;

        public IReadOnlyList<BaseTask> Tasks => _tasks;

        #endregion

        #region Constructor

        public Model()
        {
            _tasks = new List<BaseTask>();
            LoadTasksFromJson();
        }

        #endregion

        #region Load and Save

        public bool LoadTasksFromJson()
        {
            if (!File.Exists(FileName))
            {
                _tasks = new List<BaseTask>();
                return false;
            }

            string json = File.ReadAllText(FileName);

            if (string.IsNullOrWhiteSpace(json))
            {
                _tasks = new List<BaseTask>();
                return false;
            }

            _tasks = JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
            return true;
        }

        public void SaveTasks()
        {
            string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }

        #endregion

        #region CRUD

        public void CreateTask(BaseTask task)
        {
            ValidateTask(task);

            task.Id = GetNextId();
            _tasks.Add(task);

            SaveTasks();
        }
        public BaseTask ReadTask(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }
        public void UpdateTask(BaseTask updatedTask)
        {
            ValidateTask(updatedTask);

            BaseTask existing = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (existing == null) return;

            existing.Title = updatedTask.Title;
            existing.Description = updatedTask.Description;
            existing.IsCompleted = updatedTask.IsCompleted;

            SaveTasks();
        }

        public void DeleteTask(int id)
        {
            BaseTask task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return;

            _tasks.Remove(task);
            SaveTasks();
        }

        public List<BaseTask> GetTasks()
        {
            return _tasks.ToList();
        }

        #endregion

        #region Rules

        private void ValidateTask(BaseTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Task title is required.");
        }

        private int GetNextId()
        {
            if (_tasks.Count == 0)
                return 1;

            return _tasks.Max(t => t.Id ?? 0) + 1;
        }

        #endregion
    }
}