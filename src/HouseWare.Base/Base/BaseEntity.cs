using LinqToDB.Mapping;

namespace HouseWare.Base
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    [Serializable]
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [PrimaryKey, NotNull]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }

    #region Fix Id (DataType, Lowercase) cho các DB khác

    /// <summary>
    /// Base class for entities
    /// </summary>
    [Serializable]
    public abstract partial class BaseIntEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [PrimaryKey, NotNull]
        public int Id { get; set; }

    }

    /// <summary>
    /// Base class for entities
    /// </summary>
    [Serializable]
    public abstract partial class BaseIntLowercaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [PrimaryKey, NotNull]
        public int id { get; set; }

    }

    #endregion Fix Id (DataType, Lowercase) cho các DB khác
}