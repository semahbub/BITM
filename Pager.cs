public class Pager
{
	public int TotalRecords { get; set; }
	public int TotalPages { get; set; }
	public int CurrentPage { get; set; }
	public int PageSize { get; set; }
	public int StartPage { get; set; }
	public int StopPage { get; set; }
	public int ShowNoOfButtons { get; set; } = 3;
	public Pager(int totalRecords, int curpage, int pageSize)
	{
		TotalRecords = totalRecords;
		PageSize = pageSize;
		TotalPages = (int)Math.Ceiling(((decimal)totalRecords/(decimal)pageSize));
		
		if(curpage<=0) { curpage = 1; }
		CurrentPage = curpage;

		if (curpage <= 0) { curpage = 1; }
		if (curpage>TotalPages) { curpage = TotalPages; }

		StartPage = curpage-ShowNoOfButtons;
		StopPage = curpage+ShowNoOfButtons;

		if(StartPage<=0) { StartPage = 1; }
		if(StopPage>TotalPages) { StopPage = TotalPages; }

	}
}
