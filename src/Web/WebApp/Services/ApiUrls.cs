namespace WebApp.Services;

public static class ApiUrls
{
    //Tasks url
    public static string CreateTaskUrl(string baseUrl) => $"{baseUrl}/task/createTask";
    public static string UpdateTaskUrl(string baseUrl) => $"{baseUrl}/task/updateTask";
    public static string RemoveTaskUrl(string baseUrl, Guid taskId) => $"{baseUrl}/task/removeTask?taskId={taskId}";
    public static string ExecuteTaskUrl(string baseUrl) => $"{baseUrl}/task/executeTask";
    public static string CompleteTaskUrl(string baseUrl) => $"{baseUrl}/task/completeTask";
    
    public static string GetTaskUrl(string baseUrl, Guid taskId) => $"{baseUrl}/task/getTask?taskId={taskId}";
    
    //Group urls
    public static string CreateGroupUrl(string baseUrl) => $"{baseUrl}/group/createGroup";
    public static string RemoveGroupUrl(string baseUrl, Guid groupId) => $"{baseUrl}/group/removeGroup?groupId={groupId}";
    public static string AddUserToGroupUrl(string baseUrl) => $"{baseUrl}/group/addUserToGroup";
    public static string RemoveUserFromGroupUrl(string baseUrl) => $"{baseUrl}/group/removeUserFromGroup";
    
    public static string GetGroupsByUserIdUrl(string baseUrl, Guid userId) => $"{baseUrl}/group/GetGroupCollectionByUserId?userId={userId}";
    public static string GetGroupAggregatorByIdUrl(string baseUrl, Guid groupId) => $"{baseUrl}/aggregators/getGroupById?groupId={groupId}";
    
}