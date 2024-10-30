public class UserParameters
{
  const int maxPageSize = 50;
  public int PageNumber { get; set; } = 1;
  private int _pageSize = 10;
  public int PageSize
  {
    get
    {
      return _pageSize;
    }
    set
    {
      _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
  }

  public string? Nome { get; set; }
  public string? Email { get; set; }
}
