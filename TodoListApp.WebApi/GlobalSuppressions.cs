// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:Using directives should be placed correctly", Justification = "<Ожидание>")]
[assembly: SuppressMessage("Performance", "CA1849:Вызов асинхронных методов в методе async", Justification = "<Ожидание>")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApi.Controllers.HomeArea")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.WebApi.Controllers.AuthArea")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.WebApi.Controllers.AuthArea.AuthController._authService")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.WebApi.Controllers.HomeArea.GroupController._groupService")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApi.Controllers.HomeArea.GroupController.Delete(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApi.Controllers.HomeArea.GroupController.Update(System.Int32,TodoListApp.Contracts.DTO.GroupUpdateDto)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.WebApi.Controllers.HomeArea.GroupController._logger")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApi.Controllers.HomeArea.GroupController.GetAll(System.Int32,System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApi.Controllers.HomeArea.GroupController.GetById(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.WebApi.Controllers.HomeArea.GroupController.Create(TodoListApp.Contracts.DTO.GroupCreateDto)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.WebApi.Controllers.HomeArea.GroupController._mapper")]
[assembly: SuppressMessage("Maintainability", "CA1515:Рассмотрите возможность сделать общедоступные типы внутренними", Justification = "<Ожидание>", Scope = "type", Target = "~T:TodoListApp.WebApi.Controllers.HomeArea.GroupController")]
