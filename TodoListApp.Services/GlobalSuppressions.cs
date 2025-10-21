// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Identity")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Helpers")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Migrations")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Database")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Migrations.TodoListDB")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Database.Entities")]
[assembly: SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>", Scope = "member", Target = "~P:TodoListApp.Services.Database.Entities.Group.TodoLists")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Identity.Entities")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Repositories")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Interfaces")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Services")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.AuthService._userManager")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.AuthService._signInManager")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.AuthService._roleManager")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.AuthService._tokenService")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.TokenService._config")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.TokenService._userManager")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Services.Mappings")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Services.Services.AuthService.LoginAsync(TodoListApp.Contracts.DTO.LoginDto)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.Object}}")]
[assembly: SuppressMessage("Globalization", "CA1307:Используйте StringComparison, чтобы ясно указать намерение.", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Services.Services.AuthService.LoginAsync(TodoListApp.Contracts.DTO.LoginDto)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.Object}}")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Services.Services.TaskService._taskRepository")]
