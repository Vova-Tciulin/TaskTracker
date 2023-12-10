namespace WebApp.Services;

public static class ApiUrls
{
    //Tasks url
    public static string CreateTaskUrl(string baseUrl) => $"{baseUrl}/task.cmd/createTask";
    public static string UpdateTaskUrl(string baseUrl) => $"{baseUrl}/task.cmd/updateTask";
    public static string RemoveTaskUrl(string baseUrl, string taskId) => $"{baseUrl}/task.cmd/removeTask?taskId={taskId}";
    public static string ExecuteTaskUrl(string baseUrl) => $"{baseUrl}/task.cmd/executeTask";
    public static string CompleteTaskUrl(string baseUrl) => $"{baseUrl}/task.cmd/completeTask";
    public static string ReturnTaskToNewState(string baseUrl) => $"{baseUrl}/task.cmd/ReturnTaskToNew";
    
    public static string GetTaskUrl(string baseUrl, Guid taskId) => $"{baseUrl}/task.query/getTask?taskId={taskId}";
    
    //Group urls
    public static string CreateGroupUrl(string baseUrl) => $"{baseUrl}/group.cmd/createGroup";
    public static string RemoveGroupUrl(string baseUrl, Guid groupId) => $"{baseUrl}/group.cmd/removeGroup?groupId={groupId}";
    public static string AddUserToGroupUrl(string baseUrl) => $"{baseUrl}/group.cmd/addUserToGroup";
    public static string RemoveUserFromGroupUrl(string baseUrl) => $"{baseUrl}/group.cmd/removeUserFromGroup";
    
    public static string GetGroupsByUserIdUrl(string baseUrl, string userId) => $"{baseUrl}/group.query/GetGroupCollectionByUserId?userId={userId}";
    public static string GetGroupAggregatorByIdUrl(string baseUrl, string groupId) => $"{baseUrl}/aggregators/getGroupById?groupId={groupId}";
    
}