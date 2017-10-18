namespace ECApplication.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static CartRepository GetCartRepository()
		{
			var repository = new CartRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CartRepository GetCartRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CartRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static CommentRepository GetCommentRepository()
		{
			var repository = new CommentRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static CommentRepository GetCommentRepository(IUnitOfWork unitOfWork)
		{
			var repository = new CommentRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static MemberRepository GetMemberRepository()
		{
			var repository = new MemberRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static MemberRepository GetMemberRepository(IUnitOfWork unitOfWork)
		{
			var repository = new MemberRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static OrderDetailRepository GetOrderDetailRepository()
		{
			var repository = new OrderDetailRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static OrderDetailRepository GetOrderDetailRepository(IUnitOfWork unitOfWork)
		{
			var repository = new OrderDetailRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static OrderHeaderRepository GetOrderHeaderRepository()
		{
			var repository = new OrderHeaderRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static OrderHeaderRepository GetOrderHeaderRepository(IUnitOfWork unitOfWork)
		{
			var repository = new OrderHeaderRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ProductRepository GetProductRepository()
		{
			var repository = new ProductRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ProductRepository GetProductRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ProductRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ProductCategoryRepository GetProductCategoryRepository()
		{
			var repository = new ProductCategoryRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ProductCategoryRepository GetProductCategoryRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ProductCategoryRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}