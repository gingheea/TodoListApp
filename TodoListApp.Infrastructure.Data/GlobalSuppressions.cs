// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Database")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Helpers")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Identity")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Migrations.TodoListDB")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Repositories")]
[assembly: SuppressMessage("Design", "CA1031:Не перехватывать исключения общих типов", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Helpers.ErrorHandlingMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Style", "IDE0065:Неправильно расположенная директива using", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Migrations.UsersDB")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Infrastructure.Data.Helpers.ErrorHandlingMiddleware._next")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Ожидание>", Scope = "member", Target = "~F:TodoListApp.Infrastructure.Data.Helpers.ErrorHandlingMiddleware._logger")]
[assembly: SuppressMessage("Performance", "CA1848:Использовать делегаты LoggerMessage", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Helpers.ErrorHandlingMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Helpers.ErrorHandlingMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.InitTodoListDb.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.InitTodoListDb.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.UpdateOnModelCreating.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.UpdateOnModelCreating.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.AddNewEntityUserGroup.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.TodoListDB.AddNewEntityUserGroup.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.UsersDB.InitUsersDb.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TodoListApp.Infrastructure.Data.Migrations.UsersDB.InitUsersDb.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Удалить ненужное подавление", Justification = "<Ожидание>", Scope = "namespace", Target = "~N:TodoListApp.Infrastructure.Data.Repositories")]
