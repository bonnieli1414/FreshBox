using System.Linq.Expressions;

/// <summary>
/// 實作Entity Framework Generic Repository 的 Class。
/// </summary>
/// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
public class EFGenericRepository<TEntity> : IEFGenericRepository<TEntity> where TEntity : class
{
    protected readonly dbEntities Context;
    /// <summary>
    /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
    /// </summary>
    /// <param name="inContext">Entity所在的Context</param>
    public EFGenericRepository(dbEntities inContext)
    {
        Context = inContext;
    }

    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// 新增一筆資料 (非同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    /// <returns></returns>
    public async Task CreateAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    /// <summary>
    /// 取得第一筆符合條件的內容 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>第一筆符合條件的資料</returns>
    public TEntity ReadSingle(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
    }

    /// <summary>
    /// 取得第一筆符合條件的內容 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>第一筆符合條件的資料</returns>
    public async Task<TEntity> ReadSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <returns>Entity 全部筆數的資料</returns>
    public IQueryable<TEntity> ReadAll()
    {
        return Context.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <returns>Entity 全部筆數的資料</returns>
    public async Task<List<TEntity>> ReadAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public IQueryable<TEntity> ReadAll(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).AsQueryable();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public async Task<List<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的內容</param>
    public void Insert(TEntity entity)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Added;
    }

    /// <summary>
    /// 更新一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    public void Update(TEntity entity)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// 更新一筆Entity的內容。只更新有指定的Property。
    /// </summary>
    /// <param name="entity">要更新的內容。</param>
    /// <param name="updateProperties">需要更新的欄位。</param>
    public void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

        if (updateProperties != null)
        {
            foreach (var property in updateProperties)
            {
                Context.Entry<TEntity>(entity).Property(property).IsModified = true;
            }
        }
    }

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    public void Delete(TEntity entity)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// 儲存異動。
    /// </summary>
    public string SaveChanges()
    {
        string str_message = "";
        try
        {
            Context.SaveChanges();
        }
        catch (Exception ex) { str_message = ex.Message; }
        return str_message;
    }

    /// <summary>
    /// 儲存異動。
    /// </summary>
    public async Task<string> SaveChangesAsync()
    {
        string str_message = "";
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (Exception ex) { str_message = ex.Message; }
        return str_message;
    }
}