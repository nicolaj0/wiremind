using System.Collections.Generic;

namespace ConsoleApplication2.Model
{
    public class IataCodes : Dictionary<string, string>
    {
        public IataCodes()
        {
            {
                SetValue("AGA", "AGADIR");
                SetValue("AEY", "AKUREYRI");
                SetValue("AHU", "AL HOCEIMA");
                SetValue("ALG", "ALGER");
                SetValue("ALC", "ALICANTE");
                SetValue("LEI", "ALMERIA");
                SetValue("AMM", "AMMAN");
                SetValue("AMS", "AMSTERDAM");
                SetValue("ATH", "ATHENES");
                SetValue("BCN", "BARCELONE");
                SetValue("BRI", "BARI");
                SetValue("BEG", "BELGRADE");
                SetValue("EGC", "BERGERAC");
                SetValue("SXF", "BERLIN");
                SetValue("BEY", "BEYROUTH");
                SetValue("BLQ", "BOLOGNE");
                SetValue("BDS", "BRINDISI");
                SetValue("BUD", "BUDAPEST");
                SetValue("SID", "CAP-VERT");
                SetValue("CMN", "CASABLANCA");
                SetValue("EFL", "CEPHALONIE");
                SetValue("CMF", "CHAMBERY");
                SetValue("LCA", "LARNACA");
                SetValue("PFO", "PAPHOS");
                SetValue("CPH", "COPENHAGUE");
                SetValue("CFU", "CORFOU");
                SetValue("KRK", "CRACOVIE");
                SetValue("HER", "HERAKLION");
                SetValue("CHQ", "CRETE");
                SetValue("VIL", "DAKHLA");
                SetValue("DJE", "DJERBA");
                SetValue("DXB", "DUBAI;");
                SetValue("DUB", "DUBLIN");
                SetValue("DBV", "DUBROVNIK");
                SetValue("EDI", "EDIMBOURG");
                SetValue("EIN", "EINDHOVEN");
                SetValue("ESU", "ESSAOUIRA");
                SetValue("FAO", "FARO");
                SetValue("FEZ", "FES");
                SetValue("FUE", "FUERTEVENTURA");
                SetValue("GVA", "GENEVE");
                SetValue("GRO", "GERONE");
                SetValue("LPA", "GRANDE CANARIE");
                SetValue("GNB", "GRENOBLE");
                SetValue("GRQ", "GRONINGUE");
                SetValue("HEL", "HELSINKI");
                SetValue("IBZ", "IBIZA");
                SetValue("INN", "INNSBRUCK");
                SetValue("IST", "ISTANBUL AIRPORT");
                SetValue("KLX", "KALAMATA");
                SetValue("KTW", "KATOWICE");
                SetValue("KLU", "KLAGENFURT");
                SetValue("KGS", "KOS");
                SetValue("SPC", "LA PALMA");
                SetValue("SUF", "LAMEZIA TERME");
                SetValue("ACE", "LANZAROTE");
                SetValue("LIS", "LISBONNE");
                SetValue("LJU", "LJUBLJANA");
                SetValue("LYS", "LYON");
                SetValue("FNC", "MADERE");
                SetValue("MAD", "MADRID");
                SetValue("AGP", "MALAGA");
                SetValue("MLA", "MALTE");
                SetValue("RAK", "MARRAKECH");
                SetValue("MAH", "MINORQUE");
                SetValue("MIR", "MONASTIR");
                SetValue("MPL", "MONTPELLIER");
                SetValue("MUC", "MUNICH");
                SetValue("JMK", "MYKONOS");
                SetValue("NDR", "NADOR");
                SetValue("NTE", "NANTES");
                SetValue("NAP", "NAPLES");
                SetValue("NCE", "NICE");
                SetValue("ORN", "ORAN");
                SetValue("OUD", "OUJDA");
                SetValue("PMI", "PALMA DE MAJORQUE");
                SetValue("ORY", "PARIS");
                SetValue("PSA", "PISE");
                SetValue("OPO", "PORTO");
                SetValue("PRG", "PRAGUE");
                SetValue("PVK", "PREVEZA (LEFKAS)");
                SetValue("PUY", "PULA");
                SetValue("RBA", "RABAT");
                SetValue("ETM", "RAMON");
                SetValue("REU", "REUS");
                SetValue("KEF", "REYKJAVIK");
                SetValue("RHO", "RHODES");
                SetValue("RJK", "RIJEKA");
                SetValue("FCO", "ROME");
                SetValue("RTM", "ROTTERDAM/LA HAYE");
                SetValue("SZG", "SALZBOURG");
                SetValue("SMI", "SAMOS");
                SetValue("JTR", "SANTORIN");
                SetValue("OLB", "OLBIA");
                SetValue("SVQ", "SEVILLE");
                SetValue("CTA", "CATANE");
                SetValue("PMO", "PALERME");
                SetValue("SOF", "SOFIA");
                SetValue("SPU", "SPLIT");
                SetValue("ARN", "STOCKHOLM");
                SetValue("TNG", "TANGER");
                SetValue("TLV", "TEL AVIV");
                SetValue("TFS", "TENERIFE");
                SetValue("SKG", "THESSALONIQUE");
                SetValue("TIA", "TIRANA");
                SetValue("TIV", "TIVAT");
                SetValue("TLN", "TOULON");
                SetValue("TUN", "TUNIS");
                SetValue("VLC", "VALENCE");
                SetValue("VCE", "VENISE");
                SetValue("VRN", "VERONE");
                SetValue("VIE", "VIENNE");
                SetValue("VOL", "VOLOS");
                SetValue("ZAD", "ZADAR");
                SetValue("ZTH", "ZAKYNTHOS");
            }
            ;
        }

        private void SetValue(string aga, string agadir)
        {
            Add(agadir,aga);
        }
    }
}