using System.Linq.Expressions;

/// <summary>
/// 代表一個Repository的interface。
/// </summary>
/// <typeparam name="TEntity">任意model的class</typeparam>
public interface IEFGenericRepository<TEntity>
{
    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    void Create(TEntity entity);

    /// <summary>
    /// 新增一筆資料 (非同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// 取得第一筆符合條件的內容 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>第一筆符合條件的資料</returns>
    TEntity ReadSingle(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 取得第一筆符合條件的內容 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>第一筆符合條件的資料</returns>
    Task<TEntity> ReadSingleAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <returns>Entity 全部筆數的資料</returns>
    IQueryable<TEntity> ReadAll();

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <returns>Entity 全部筆數的資料</returns>
    Task<List<TEntity>> ReadAllAsync();

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>Entity 全部筆數的資料</returns>
    IQueryable<TEntity> ReadAll(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <returns>Entity 全部筆數的資料</returns>
    Task<List<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的內容</param>
    void Insert(TEntity entity);

    /// <summary>
    /// 更新一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    void Update(TEntity entity);

    /// <summary>
    /// 更新一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    /// <param name="updateProperties">更新的屬性</param>
    void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties);

    /// <summary>
    /// 刪除一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    void Delete(TEntity entity);

    /// <summary>
    /// 儲存異動 (同步)
    /// </summary>
    string SaveChanges();

    /// <summary>
    /// 儲存異動 (非同步)
    /// </summary>
    Task<string> SaveChangesAsync();
}