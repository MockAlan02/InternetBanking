﻿namespace InternetBanking.Domain.Common
{
    public class BaseEntity<TKey>
    {
        public TKey? Id { get; set; }
    }

    public class BaseEntity
    : BaseEntity<int>
    {
    }
}
