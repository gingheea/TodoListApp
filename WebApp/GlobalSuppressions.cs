// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.AuthService.LoginAsync(System.String,System.String)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.String}}")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.AuthService.RegisterAsync(System.String,System.String,System.String,System.String)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.String}}")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApp.Service")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.TodoListService.GetUserListsAsync~System.Threading.Tasks.Task{System.Collections.Generic.List{TodoListApp.WebApp.Model.TodoListModel}}")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.TodoItemService.GetTasksByListIdAsync(System.Int32)~System.Threading.Tasks.Task{System.Collections.Generic.List{TodoListApp.WebApp.Model.TodoItemModel}}")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApp.Handler")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Удалить ненужное подавление", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApp.Handler")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Удалить ненужное подавление", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApp.Service")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.TodoItemService.GetTasksByListIdAsync(System.Int32,System.Int32,System.Int32)~System.Threading.Tasks.Task{System.Collections.Generic.List{TodoListApp.WebApp.Model.TodoItemModel}}")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApp.Service.TodoListService.GetUserListsAsync(System.Int32,System.Int32)~System.Threading.Tasks.Task{System.Collections.Generic.List{TodoListApp.WebApp.Model.TodoListModel}}")]
