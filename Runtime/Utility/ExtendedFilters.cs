// -------------------------------------------------------------------------------------
// The MIT License
// Extended filters for LeoECS Lite https://github.com/Leopotam/ecslite-extendedfilters
// Copyright (c) 2021 Leopotam <leopotam@gmail.com>
// -------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace Leopotam.EcsLite.ExtendedFilters {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class Extensions {
        static int[] _filterSortPool = new int[512];

        public static EcsFilter Reorder (this EcsFilter filter, EcsFilterReorderHandler cb) {
            var count = filter.GetEntitiesCount ();
            if (count > 1) {
                var entities = filter.GetRawEntities ();
                if (_filterSortPool.Length < entities.Length) {
                    Array.Resize (ref _filterSortPool, entities.Length);
                }
                for (int i = 0, iMax = count; i < iMax; i++) {
                    _filterSortPool[i] = cb (entities[i]);
                }
                Array.Sort (_filterSortPool, entities, 0, count);
                var sparseIndex = filter.GetSparseIndex ();
                for (int i = 0, iMax = count; i < iMax; i++) {
                    sparseIndex[entities[i]] = i + 1;
                }
            }
            return filter;
        }
    }
    public delegate int EcsFilterReorderHandler (int entity);
}

#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices {
    enum Option {
        NullChecks = 1,
        ArrayBoundsChecks = 2
    }

    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute (Option option, object value) { Option = option; Value = value; }
    }
}
#endif