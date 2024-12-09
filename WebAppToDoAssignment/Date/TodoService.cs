using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WebAppToDoAssignment.Models;

namespace WebAppToDoAssignment.Date
    {
        public class TodoService
        {
            private static List<Todo> todoItems = new List<Todo>();

            public int Size()
            {
                return todoItems.Count;
            }

            public Todo[] FindAll()
            {
                return todoItems.ToArray();
            }

            public Todo FindById(int todoId)
            {
                return todoItems.FirstOrDefault(todo => todo.Id == todoId);
            }

            public Todo CreateTodoItem(string description, bool done, Person assignee)
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new ArgumentException("Description cannot be null or empty.", nameof(description));
                }

                Todo newTodo = new Todo(TodoSequencer.NextTodoId(), description)
                {
                    Done = done,
                    Assignee = assignee
                };

                todoItems.Add(newTodo);
                return newTodo;
            }

            public void UpdateTodoItem(Todo updatedTodo)
            {
                var existingTodo = FindById(updatedTodo.Id);
                if (existingTodo != null)
                {
                    existingTodo.Description = updatedTodo.Description;
                    existingTodo.Done = updatedTodo.Done;
                    existingTodo.Assignee = updatedTodo.Assignee;
                }
            }

            public void Clear()
            {
                todoItems.Clear();
            }

            public void RemoveTodoItem(int todoId)
            {
                var todoToRemove = FindById(todoId);
                if (todoToRemove != null)
                {
                    todoItems.Remove(todoToRemove);
                }
            }

        // New methods (point 10)

        public Todo[] FindByDoneStatus(bool doneStatus)
        {
            return todoItems.Where(todo => todo.Done == doneStatus).ToArray();
        }

        public Todo[] FindByAssignee(int personId)
        {
            return todoItems.Where(todo => todo.Assignee != null && todo.Assignee.Id == personId).ToArray();
        }

        public Todo[] FindByAssignee(Person assignee)
            {
                if (assignee == null) throw new ArgumentNullException(nameof(assignee));

                return FindByAssignee(assignee.Id);
            }

            public Todo[] FindUnassignedTodoItems()
            {
                return todoItems.Where(todo => todo.Assignee == null).ToArray();
            }
        }
    }



    //public class TodoService
    //{
    //    private static Todo[] todoItems = new Todo[0];

    //    public int Size()
    //    {
    //        return todoItems.Length;
    //    }

    //    public Todo[] FindAll()
    //    {
    //        return todoItems;
    //    }

    //    public Todo FindById(int todoId)
    //    {
    //        return todoItems.FirstOrDefault(todo => todo.Id == todoId);
    //    }

    //    public Todo CreateTodoItem(string description, bool done, Person assignee)
    //    {
    //        Todo newTodo = new Todo(TodoSequencer.NextTodoId(), description)
    //        {
    //            Done = done,
    //            Assignee = assignee
    //        };
    //        Array.Resize(ref todoItems, todoItems.Length + 1);
    //        todoItems[^1] = newTodo;
    //        return newTodo;
    //    }

    //    public void UpdateTodoItem(Todo updatedTodo)
    //    {
    //        var existingTodo = todoItems.FirstOrDefault(todo => todo.Id == updatedTodo.Id);
    //        if (existingTodo != null)
    //        {
    //            existingTodo.Description = updatedTodo.Description;
    //            existingTodo.Done = updatedTodo.Done;
    //            existingTodo.Assignee = updatedTodo.Assignee;
    //        }
    //    }

    //    public void Clear()
    //    {
    //        todoItems = new Todo[0];
    //    }

    //    public void RemoveTodoItem(int todoId)
    //    {
    //        int index = Array.FindIndex(todoItems, todo => todo.Id == todoId);
    //        if (index != -1)
    //        {
    //            todoItems = todoItems.Where((source, i) => i != index).ToArray();
    //        }
    //    }

    //// Add New methods point 10 
    //public Todo[] FindByDoneStatus(bool doneStatus)
    //{
    //return todoItems.Where(todo => todo.Done == doneStatus).ToArray();
    //}
    //public Todo[] FindByAssignee(int personId)
    //{
    //return todoItems.Where(todo => todo.Assignee != null && todo.Assignee.Id == personId).ToArray();
    // }
    //public Todo[] FindByAssignee(Person assignee)
    //{
    //return todoItems.Where(todo => todo.Assignee == assignee).ToArray();
    //}
    //public Todo[] FindUnassignedTodoItems()
    //{
    //return todoItems.Where(todo => todo.Assignee == null).ToArray();
    //}

