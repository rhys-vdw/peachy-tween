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
        public static void Validate<TInc1> (this ref EcsFilterExt<TInc1> f, EcsWorld world)
            where TInc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
        }

        public static void Validate<TInc1, TExc1> (this ref EcsFilterExt<TInc1>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TExc1, TExc2> (this ref EcsFilterExt<TInc1>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2> (this ref EcsFilterExt<TInc1, TInc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
        }

        public static void Validate<TInc1, TInc2, TExc1> (this ref EcsFilterExt<TInc1, TInc2>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TInc8 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Inc<TInc8> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._inc8 = world.GetPool<TInc8> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8, TExc1> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8>.Exc<TExc1> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TInc8 : struct
            where TExc1 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Inc<TInc8> ().Exc<TExc1> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._inc8 = world.GetPool<TInc8> ();
            f._exc1 = world.GetPool<TExc1> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8, TExc1, TExc2> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8>.Exc<TExc1, TExc2> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TInc8 : struct
            where TExc1 : struct
            where TExc2 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Inc<TInc8> ().Exc<TExc1> ().Exc<TExc2> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._inc8 = world.GetPool<TInc8> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8, TExc1, TExc2, TExc3> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8>.Exc<TExc1, TExc2, TExc3> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TInc8 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Inc<TInc8> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._inc8 = world.GetPool<TInc8> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
        }

        public static void Validate<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8, TExc1, TExc2, TExc3, TExc4> (this ref EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8>.Exc<TExc1, TExc2, TExc3, TExc4> f, EcsWorld world)
            where TInc1 : struct
            where TInc2 : struct
            where TInc3 : struct
            where TInc4 : struct
            where TInc5 : struct
            where TInc6 : struct
            where TInc7 : struct
            where TInc8 : struct
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            if (f._filter != null) { return; }
            f._filter = world.Filter<TInc1> ().Inc<TInc2> ().Inc<TInc3> ().Inc<TInc4> ().Inc<TInc5> ().Inc<TInc6> ().Inc<TInc7> ().Inc<TInc8> ().Exc<TExc1> ().Exc<TExc2> ().Exc<TExc3> ().Exc<TExc4> ().End ();
            f._inc1 = world.GetPool<TInc1> ();
            f._inc2 = world.GetPool<TInc2> ();
            f._inc3 = world.GetPool<TInc3> ();
            f._inc4 = world.GetPool<TInc4> ();
            f._inc5 = world.GetPool<TInc5> ();
            f._inc6 = world.GetPool<TInc6> ();
            f._inc7 = world.GetPool<TInc7> ();
            f._inc8 = world.GetPool<TInc8> ();
            f._exc1 = world.GetPool<TExc1> ();
            f._exc2 = world.GetPool<TExc2> ();
            f._exc3 = world.GetPool<TExc3> ();
            f._exc4 = world.GetPool<TExc4> ();
        }

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

    public struct EcsFilterExt<TInc1>
        where TInc1 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2>
        where TInc1 : struct
        where TInc2 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3, TInc4>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;
        internal EcsPool<TInc4> _inc4;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc4> Inc4 () => _inc4;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
        where TInc5 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;
        internal EcsPool<TInc4> _inc4;
        internal EcsPool<TInc5> _inc5;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc4> Inc4 () => _inc4;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc5> Inc5 () => _inc5;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
        where TInc5 : struct
        where TInc6 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;
        internal EcsPool<TInc4> _inc4;
        internal EcsPool<TInc5> _inc5;
        internal EcsPool<TInc6> _inc6;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc4> Inc4 () => _inc4;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc5> Inc5 () => _inc5;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc6> Inc6 () => _inc6;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
        where TInc5 : struct
        where TInc6 : struct
        where TInc7 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;
        internal EcsPool<TInc4> _inc4;
        internal EcsPool<TInc5> _inc5;
        internal EcsPool<TInc6> _inc6;
        internal EcsPool<TInc7> _inc7;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc4> Inc4 () => _inc4;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc5> Inc5 () => _inc5;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc6> Inc6 () => _inc6;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc7> Inc7 () => _inc7;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }

    public struct EcsFilterExt<TInc1, TInc2, TInc3, TInc4, TInc5, TInc6, TInc7, TInc8>
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
        where TInc5 : struct
        where TInc6 : struct
        where TInc7 : struct
        where TInc8 : struct {
        internal EcsFilter _filter;
        internal EcsPool<TInc1> _inc1;
        internal EcsPool<TInc2> _inc2;
        internal EcsPool<TInc3> _inc3;
        internal EcsPool<TInc4> _inc4;
        internal EcsPool<TInc5> _inc5;
        internal EcsPool<TInc6> _inc6;
        internal EcsPool<TInc7> _inc7;
        internal EcsPool<TInc8> _inc8;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsFilter Filter () => _filter;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc1> Inc1 () => _inc1;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc2> Inc2 () => _inc2;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc3> Inc3 () => _inc3;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc4> Inc4 () => _inc4;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc5> Inc5 () => _inc5;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc6> Inc6 () => _inc6;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc7> Inc7 () => _inc7;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public EcsPool<TInc8> Inc8 () => _inc8;

        public struct Exc<TExc1>
            where TExc1 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TInc8> _inc8;
            internal EcsPool<TExc1> _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc8> Inc8 () => _inc8;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;
        }

        public struct Exc<TExc1, TExc2>
            where TExc1 : struct
            where TExc2 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TInc8> _inc8;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc8> Inc8 () => _inc8;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;
        }

        public struct Exc<TExc1, TExc2, TExc3>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TInc8> _inc8;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc8> Inc8 () => _inc8;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;
        }

        public struct Exc<TExc1, TExc2, TExc3, TExc4>
            where TExc1 : struct
            where TExc2 : struct
            where TExc3 : struct
            where TExc4 : struct {
            internal EcsFilter _filter;
            internal EcsPool<TInc1> _inc1;
            internal EcsPool<TInc2> _inc2;
            internal EcsPool<TInc3> _inc3;
            internal EcsPool<TInc4> _inc4;
            internal EcsPool<TInc5> _inc5;
            internal EcsPool<TInc6> _inc6;
            internal EcsPool<TInc7> _inc7;
            internal EcsPool<TInc8> _inc8;
            internal EcsPool<TExc1> _exc1;
            internal EcsPool<TExc2> _exc2;
            internal EcsPool<TExc3> _exc3;
            internal EcsPool<TExc4> _exc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsFilter Filter () => _filter;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc1> Inc1 () => _inc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc2> Inc2 () => _inc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc3> Inc3 () => _inc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc4> Inc4 () => _inc4;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc5> Inc5 () => _inc5;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc6> Inc6 () => _inc6;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc7> Inc7 () => _inc7;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TInc8> Inc8 () => _inc8;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc1> Exc1 () => _exc1;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc2> Exc2 () => _exc2;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc3> Exc3 () => _exc3;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public EcsPool<TExc4> Exc4 () => _exc4;
        }
    }
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