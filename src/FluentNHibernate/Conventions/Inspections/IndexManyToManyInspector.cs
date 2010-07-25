﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class IndexManyToManyInspector : IIndexManyToManyInspector
    {
        private readonly InspectorModelMapper<IIndexManyToManyInspector, IndexManyToManyMapping> mappedProperties = new InspectorModelMapper<IIndexManyToManyInspector, IndexManyToManyMapping>();
        private readonly IndexManyToManyMapping mapping;

        public IndexManyToManyInspector(IndexManyToManyMapping mapping)
        {
            this.mapping = mapping;
            mappedProperties.Map(x => x.Class, x => x.Type);
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Type.Name; }
        }

        public bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
        }
        
        public TypeReference Class
        {
            get { return mapping.Type; }
        }
        
        public string ForeignKey
        {
            get { return mapping.ForeignKey; }
        }

        public IEnumerable<IColumnInspector> Columns
        {
            get
            {
                return mapping.Columns
                    .Select(x => new ColumnInspector(mapping.ContainingEntityType, x))
                    .Cast<IColumnInspector>();
            }
        }
    }
}
