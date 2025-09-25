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
