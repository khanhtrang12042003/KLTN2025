using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class KiemTraDangNhapAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session.GetString("UserName");
        if (string.IsNullOrEmpty(session))
        {
            context.Result = new RedirectToActionResult("DangNhap", "TaiKhoan", null);
        }
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var response = context.HttpContext.Response;
        response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        response.Headers["Pragma"] = "no-cache";
        response.Headers["Expires"] = "0";
        base.OnResultExecuting(context);
    }
}
