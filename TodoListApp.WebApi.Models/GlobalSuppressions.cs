// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApi.Models.UserModels")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApi.Models.Models")]
[assembly: SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>", Scope = "member", Target = "~P:TodoListApp.WebApi.Models.Models.TodoList.TodoItems")]
[assembly: SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>", Scope = "member", Target = "~P:TodoListApp.WebApi.Models.UserModels.User.UserTodoLists")]
[assembly: SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>", Scope = "member", Target = "~P:TodoListApp.WebApi.Models.Models.Group.TodoLists")]
[assembly: SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>", Scope = "member", Target = "~P:TodoListApp.WebApi.Models.Models.TodoList.UserTodoLists")]
