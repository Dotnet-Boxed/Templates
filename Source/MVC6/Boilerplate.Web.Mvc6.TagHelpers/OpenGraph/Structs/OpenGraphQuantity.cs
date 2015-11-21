namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// A quantity of units.
    /// </summary>
    public class OpenGraphQuantity
    {
        private readonly string units;
        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphQuantity"/> class.
        /// </summary>
        /// <param name="value">The quantity value.</param>
        /// <param name="units">The type of units. Allowed values are Ym, Zm, Em, Pm, Tm, Gm, Mm, km, hm, dam, m, dm, cm, mm, μm, nm, pm, fm, am, zm, ym, Yg, Zg, Eg, Pg, Tg, Gg, Mg, kg, hg, dag, g, dg, cg, mg, μg, ng, pg, fg, ag, zg, yg, Ys, Zs, Es, Ps, Ts, Gs, Ms, ks, hs, das, s, ds, cs, ms, μs, ns, ps, fs, as, zs, ys, YA, ZA, EA, PA, TA, GA, MA, kA, hA, daA, A, dA, cA, mA, μA, nA, pA, fA, aA, zA, yA, YK, ZK, EK, PK, TK, GK, MK, kK, hK, daK, K, dK, cK, mK, μK, nK, pK, fK, aK, zK, yK, Ymol, Zmol, Emol, Pmol, Tmol, Gmol, Mmol, kmol, hmol, damol, mol, dmol, cmol, mmol, μmol, nmol, pmol, fmol, amol, zmol, ymol, Ycd, Zcd, Ecd, Pcd, Tcd, Gcd, Mcd, kcd, hcd, dacd, cd, dcd, ccd, mcd, μcd, ncd, pcd, fcd, acd, zcd, ycd, YJ, ZJ, EJ, PJ, TJ, GJ, MJ, kJ, hJ, daJ, J, dJ, cJ, mJ, μJ, nJ, pJ, fJ, aJ, zJ, yJ, Ym/s, s/Ym, Zm/s, s/Zm, Em/s, s/Em, Pm/s, s/Pm, Tm/s, s/Tm, Gm/s, s/Gm, Mm/s, s/Mm, km/s, s/km, hm/s, s/hm, dam/s, s/dam, m/s, s/m, dm/s, s/dm, cm/s, s/cm, mm/s, s/mm, μm/s, s/μm, nm/s, s/nm, pm/s, s/pm, fm/s, s/fm, am/s, s/am, zm/s, s/zm, ym/s, s/ym, Yg/s, s/Yg, Zg/s, s/Zg, Eg/s, s/Eg, Pg/s, s/Pg, Tg/s, s/Tg, Gg/s, s/Gg, Mg/s, s/Mg, kg/s, s/kg, hg/s, s/hg, dag/s, s/dag, g/s, s/g, dg/s, s/dg, cg/s, s/cg, mg/s, s/mg, μg/s, s/μg, ng/s, s/ng, pg/s, s/pg, fg/s, s/fg, ag/s, s/ag, zg/s, s/zg, yg/s, s/yg, Ys/s, s/Ys, Zs/s, s/Zs, Es/s, s/Es, Ps/s, s/Ps, Ts/s, s/Ts, Gs/s, s/Gs, Ms/s, s/Ms, ks/s, s/ks, hs/s, s/hs, das/s, s/das, s/s, s/s, ds/s, s/ds, cs/s, s/cs, ms/s, s/ms, μs/s, s/μs, ns/s, s/ns, ps/s, s/ps, fs/s, s/fs, as/s, s/as, zs/s, s/zs, ys/s, s/ys, YA/s, s/YA, ZA/s, s/ZA, EA/s, s/EA, PA/s, s/PA, TA/s, s/TA, GA/s, s/GA, MA/s, s/MA, kA/s, s/kA, hA/s, s/hA, daA/s, s/daA, A/s, s/A, dA/s, s/dA, cA/s, s/cA, mA/s, s/mA, μA/s, s/μA, nA/s, s/nA, pA/s, s/pA, fA/s, s/fA, aA/s, s/aA, zA/s, s/zA, yA/s, s/yA, YK/s, s/YK, ZK/s, s/ZK, EK/s, s/EK, PK/s, s/PK, TK/s, s/TK, GK/s, s/GK, MK/s, s/MK, kK/s, s/kK, hK/s, s/hK, daK/s, s/daK, K/s, s/K, dK/s, s/dK, cK/s, s/cK, mK/s, s/mK, μK/s, s/μK, nK/s, s/nK, pK/s, s/pK, fK/s, s/fK, aK/s, s/aK, zK/s, s/zK, yK/s, s/yK, Ymol/s, s/Ymol, Zmol/s, s/Zmol, Emol/s, s/Emol, Pmol/s, s/Pmol, Tmol/s, s/Tmol, Gmol/s, s/Gmol, Mmol/s, s/Mmol, kmol/s, s/kmol, hmol/s, s/hmol, damol/s, s/damol, mol/s, s/mol, dmol/s, s/dmol, cmol/s, s/cmol, mmol/s, s/mmol, μmol/s, s/μmol, nmol/s, s/nmol, pmol/s, s/pmol, fmol/s, s/fmol, amol/s, s/amol, zmol/s, s/zmol, ymol/s, s/ymol, Ycd/s, s/Ycd, Zcd/s, s/Zcd, Ecd/s, s/Ecd, Pcd/s, s/Pcd, Tcd/s, s/Tcd, Gcd/s, s/Gcd, Mcd/s, s/Mcd, kcd/s, s/kcd, hcd/s, s/hcd, dacd/s, s/dacd, cd/s, s/cd, dcd/s, s/dcd, ccd/s, s/ccd, mcd/s, s/mcd, μcd/s, s/μcd, ncd/s, s/ncd, pcd/s, s/pcd, fcd/s, s/fcd, acd/s, s/acd, zcd/s, s/zcd, ycd/s, s/ycd, YJ/s, s/YJ, ZJ/s, s/ZJ, EJ/s, s/EJ, PJ/s, s/PJ, TJ/s, s/TJ, GJ/s, s/GJ, MJ/s, s/MJ, kJ/s, s/kJ, hJ/s, s/hJ, daJ/s, s/daJ, J/s, s/J, dJ/s, s/dJ, cJ/s, s/cJ, mJ/s, s/mJ, μJ/s, s/μJ, nJ/s, s/nJ, pJ/s, s/pJ, fJ/s, s/fJ, aJ/s, s/aJ, zJ/s, s/zJ, yJ/s, s/yJ, mi/s, s/mi, ft/s, s/ft, in/s, s/in, ton/s, s/ton, lb/s, s/lb, oz/s, s/oz, mi, ft, in, ton, lb, oz and %.</param>
        /// <exception cref="System.ArgumentNullException">units</exception>
        public OpenGraphQuantity(double value, string units)
        {
            if (units == null) { throw new ArgumentNullException(nameof(units)); }

            this.units = units;
            this.value = value;
        }

        /// <summary>
        /// Gets the type of units. Allowed values are Ym, Zm, Em, Pm, Tm, Gm, Mm, km, hm, dam, m, dm, cm, mm, μm, nm, pm, fm, am, zm, ym, Yg, Zg, Eg, Pg, Tg, Gg, Mg, kg, hg, dag, g, dg, cg, mg, μg, ng, pg, fg, ag, zg, yg, Ys, Zs, Es, Ps, Ts, Gs, Ms, ks, hs, das, s, ds, cs, ms, μs, ns, ps, fs, as, zs, ys, YA, ZA, EA, PA, TA, GA, MA, kA, hA, daA, A, dA, cA, mA, μA, nA, pA, fA, aA, zA, yA, YK, ZK, EK, PK, TK, GK, MK, kK, hK, daK, K, dK, cK, mK, μK, nK, pK, fK, aK, zK, yK, Ymol, Zmol, Emol, Pmol, Tmol, Gmol, Mmol, kmol, hmol, damol, mol, dmol, cmol, mmol, μmol, nmol, pmol, fmol, amol, zmol, ymol, Ycd, Zcd, Ecd, Pcd, Tcd, Gcd, Mcd, kcd, hcd, dacd, cd, dcd, ccd, mcd, μcd, ncd, pcd, fcd, acd, zcd, ycd, YJ, ZJ, EJ, PJ, TJ, GJ, MJ, kJ, hJ, daJ, J, dJ, cJ, mJ, μJ, nJ, pJ, fJ, aJ, zJ, yJ, Ym/s, s/Ym, Zm/s, s/Zm, Em/s, s/Em, Pm/s, s/Pm, Tm/s, s/Tm, Gm/s, s/Gm, Mm/s, s/Mm, km/s, s/km, hm/s, s/hm, dam/s, s/dam, m/s, s/m, dm/s, s/dm, cm/s, s/cm, mm/s, s/mm, μm/s, s/μm, nm/s, s/nm, pm/s, s/pm, fm/s, s/fm, am/s, s/am, zm/s, s/zm, ym/s, s/ym, Yg/s, s/Yg, Zg/s, s/Zg, Eg/s, s/Eg, Pg/s, s/Pg, Tg/s, s/Tg, Gg/s, s/Gg, Mg/s, s/Mg, kg/s, s/kg, hg/s, s/hg, dag/s, s/dag, g/s, s/g, dg/s, s/dg, cg/s, s/cg, mg/s, s/mg, μg/s, s/μg, ng/s, s/ng, pg/s, s/pg, fg/s, s/fg, ag/s, s/ag, zg/s, s/zg, yg/s, s/yg, Ys/s, s/Ys, Zs/s, s/Zs, Es/s, s/Es, Ps/s, s/Ps, Ts/s, s/Ts, Gs/s, s/Gs, Ms/s, s/Ms, ks/s, s/ks, hs/s, s/hs, das/s, s/das, s/s, s/s, ds/s, s/ds, cs/s, s/cs, ms/s, s/ms, μs/s, s/μs, ns/s, s/ns, ps/s, s/ps, fs/s, s/fs, as/s, s/as, zs/s, s/zs, ys/s, s/ys, YA/s, s/YA, ZA/s, s/ZA, EA/s, s/EA, PA/s, s/PA, TA/s, s/TA, GA/s, s/GA, MA/s, s/MA, kA/s, s/kA, hA/s, s/hA, daA/s, s/daA, A/s, s/A, dA/s, s/dA, cA/s, s/cA, mA/s, s/mA, μA/s, s/μA, nA/s, s/nA, pA/s, s/pA, fA/s, s/fA, aA/s, s/aA, zA/s, s/zA, yA/s, s/yA, YK/s, s/YK, ZK/s, s/ZK, EK/s, s/EK, PK/s, s/PK, TK/s, s/TK, GK/s, s/GK, MK/s, s/MK, kK/s, s/kK, hK/s, s/hK, daK/s, s/daK, K/s, s/K, dK/s, s/dK, cK/s, s/cK, mK/s, s/mK, μK/s, s/μK, nK/s, s/nK, pK/s, s/pK, fK/s, s/fK, aK/s, s/aK, zK/s, s/zK, yK/s, s/yK, Ymol/s, s/Ymol, Zmol/s, s/Zmol, Emol/s, s/Emol, Pmol/s, s/Pmol, Tmol/s, s/Tmol, Gmol/s, s/Gmol, Mmol/s, s/Mmol, kmol/s, s/kmol, hmol/s, s/hmol, damol/s, s/damol, mol/s, s/mol, dmol/s, s/dmol, cmol/s, s/cmol, mmol/s, s/mmol, μmol/s, s/μmol, nmol/s, s/nmol, pmol/s, s/pmol, fmol/s, s/fmol, amol/s, s/amol, zmol/s, s/zmol, ymol/s, s/ymol, Ycd/s, s/Ycd, Zcd/s, s/Zcd, Ecd/s, s/Ecd, Pcd/s, s/Pcd, Tcd/s, s/Tcd, Gcd/s, s/Gcd, Mcd/s, s/Mcd, kcd/s, s/kcd, hcd/s, s/hcd, dacd/s, s/dacd, cd/s, s/cd, dcd/s, s/dcd, ccd/s, s/ccd, mcd/s, s/mcd, μcd/s, s/μcd, ncd/s, s/ncd, pcd/s, s/pcd, fcd/s, s/fcd, acd/s, s/acd, zcd/s, s/zcd, ycd/s, s/ycd, YJ/s, s/YJ, ZJ/s, s/ZJ, EJ/s, s/EJ, PJ/s, s/PJ, TJ/s, s/TJ, GJ/s, s/GJ, MJ/s, s/MJ, kJ/s, s/kJ, hJ/s, s/hJ, daJ/s, s/daJ, J/s, s/J, dJ/s, s/dJ, cJ/s, s/cJ, mJ/s, s/mJ, μJ/s, s/μJ, nJ/s, s/nJ, pJ/s, s/pJ, fJ/s, s/fJ, aJ/s, s/aJ, zJ/s, s/zJ, yJ/s, s/yJ, mi/s, s/mi, ft/s, s/ft, in/s, s/in, ton/s, s/ton, lb/s, s/lb, oz/s, s/oz, mi, ft, in, ton, lb, oz and %.
        /// </summary>
        public string Units { get { return this.units; } }

        /// <summary>
        /// Gets the quantity value.
        /// </summary>
        public double Value { get { return this.value; } }
    }
}
