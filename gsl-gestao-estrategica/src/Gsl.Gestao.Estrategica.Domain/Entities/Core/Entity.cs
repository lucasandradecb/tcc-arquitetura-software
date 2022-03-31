﻿using Flunt.Notifications;
using System;

namespace Gsl.Gestao.Estrategica.Domain.Entities.Core
{
    public abstract class Entity : Notifiable
    {
        private Guid _id;
        public virtual Guid Id
        {
            get => _id;
            set => _id = value;
        }

        protected Entity() => Id = Guid.NewGuid();
    }
}
