namespace ProjectManagement.Shared.Utility;

public record PagingList<T>(IEnumerable<T> Data, int TotalItems);
