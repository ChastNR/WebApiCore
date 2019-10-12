using System.ComponentModel;

namespace AuthenticationProcessor.UserData
{
    public enum CountryCodes : byte
    {
        [Description("Andorra"), DefaultValue("376")]
        AD = 1,

        [Description("United Arab Emirates"), DefaultValue("971")]
        AE,

        [Description("Afghanistan"), DefaultValue("93")]
        AF,

        [Description("Antigua and Barbuda"), DefaultValue("1-268")]
        AG,

        [Description("Anguilla"), DefaultValue("1-264")]
        AI,

        [Description("Albania"), DefaultValue("355")]
        AL,

        [Description("Armenia"), DefaultValue("374")]
        AM,

        [Description("Angola"), DefaultValue("244")]
        AO,

        [Description("Antarctica"), DefaultValue("672")]
        AQ,

        [Description("Argentina"), DefaultValue("54")]
        AR,

        [Description("American Samoa"), DefaultValue("1-684")]
        AS,

        [Description("Austria"), DefaultValue("43")]
        AT,

        [Description("Australia"), DefaultValue("61")]
        AU,

        [Description("Aruba"), DefaultValue("297")]
        AW,

        [Description("Åland Islands"), DefaultValue("")]
        AX,

        [Description("Azerbaijan"), DefaultValue("994")]
        AZ,

        [Description("Bosnia and Herzegovina"), DefaultValue("387")]
        BA,

        [Description("Barbados"), DefaultValue("1-246")]
        BB,

        [Description("Bangladesh"), DefaultValue("880")]
        BD,

        [Description("Belgium"), DefaultValue("32")]
        BE,

        [Description("Burkina Faso"), DefaultValue("226")]
        BF,

        [Description("Bulgaria"), DefaultValue("359")]
        BG,

        [Description("Bahrain"), DefaultValue("973")]
        BH,

        [Description("Burundi"), DefaultValue("257")]
        BI,

        [Description("Benin"), DefaultValue("229")]
        BJ,

        [Description("Saint Barthélemy"), DefaultValue("590")]
        BL,

        [Description("Bermuda"), DefaultValue("1-441")]
        BM,

        [Description("Brunei"), DefaultValue("673")]
        BN,

        [Description("Bolivia"), DefaultValue("591")]
        BO,

        [Description("Bonaire"), DefaultValue("")]
        BQ,

        [Description("Brazil"), DefaultValue("55")]
        BR,

        [Description("Bahamas"), DefaultValue("1-242")]
        BS,

        [Description("Bhutan"), DefaultValue("975")]
        BT,

        [Description("Bouvet Island"), DefaultValue("")]
        BV,

        [Description("Botswana"), DefaultValue("267")]
        BW,

        [Description("Belarus"), DefaultValue("375")]
        BY,

        [Description("Belize"), DefaultValue("501")]
        BZ,

        [Description("Canada"), DefaultValue("1")]
        CA,

        [Description("Cocos Islands"), DefaultValue("61")]
        CC,

        [Description("Democratic Republic of the Congo"), DefaultValue("243")]
        CD,

        [Description("Central African Republic"), DefaultValue("236")]
        CF,

        [Description("Congo"), DefaultValue("243")]
        CG,

        [Description("Switzerland"), DefaultValue("41")]
        CH,

        [Description("Côte d'Ivoire"), DefaultValue("")]
        CI,

        [Description("Cook Islands"), DefaultValue("682")]
        CK,

        [Description("Chile"), DefaultValue("56")]
        CL,

        [Description("Cameroon"), DefaultValue("237")]
        CM,

        [Description("China"), DefaultValue("86")]
        CN,

        [Description("Colombia"), DefaultValue("57")]
        CO,

        [Description("Costa Rica"), DefaultValue("506")]
        CR,

        [Description("Cuba"), DefaultValue("53")]
        CU,

        [Description("Cape Verde"), DefaultValue("238")]
        CV,

        [Description("Curacao"), DefaultValue("599")]
        CW,

        [Description("Christmas Island"), DefaultValue("61")]
        CX,

        [Description("Cyprus"), DefaultValue("357")]
        CY,

        [Description("Czech Republic"), DefaultValue("420")]
        CZ,

        [Description("Germany"), DefaultValue("49")]
        DE,

        [Description("Djibouti"), DefaultValue("253")]
        DJ,

        [Description("Denmark"), DefaultValue("45")]
        DK,

        [Description("Dominica"), DefaultValue("1-767")]
        DM,

        [Description("Dominican Republic"), DefaultValue("1-809, 1-829, 1-849")]
        DO,

        [Description("Algeria"), DefaultValue("213")]
        DZ,

        [Description("Ecuador"), DefaultValue("593")]
        EC,

        [Description("Estonia"), DefaultValue("372")]
        EE,

        [Description("Egypt"), DefaultValue("20")]
        EG,

        [Description("Western Sahara"), DefaultValue("212")]
        EH,

        [Description("Eritrea"), DefaultValue("291")]
        ER,

        [Description("Spain"), DefaultValue("34")]
        ES,

        [Description("Ethiopia"), DefaultValue("251")]
        ET,

        [Description("Finland"), DefaultValue("358")]
        FI,

        [Description("Fiji"), DefaultValue("679")]
        FJ,

        [Description("Falkland Islands"), DefaultValue("500")]
        FK,

        [Description("Micronesia"), DefaultValue("691")]
        FM,

        [Description("Faroe Islands"), DefaultValue("298")]
        FO,

        [Description("France"), DefaultValue("33")]
        FR,

        [Description("Gabon"), DefaultValue("241")]
        GA,

        [Description("United Kingdom"), DefaultValue("44")]
        GB,

        [Description("Grenada"), DefaultValue("1-473")]
        GD,

        [Description("Georgia"), DefaultValue("995")]
        GE,

        [Description("French Guiana"), DefaultValue("")]
        GF,

        [Description("Guernsey"), DefaultValue("44-1481")]
        GG,

        [Description("Ghana"), DefaultValue("233")]
        GH,

        [Description("Gibraltar"), DefaultValue("350")]
        GI,

        [Description("Greenland"), DefaultValue("299")]
        GL,

        [Description("Gambia"), DefaultValue("220")]
        GM,

        [Description("Guinea"), DefaultValue("224")]
        GN,

        [Description("Guadeloupe"), DefaultValue("")]
        GP,

        [Description("Equatorial Guinea"), DefaultValue("240")]
        GQ,

        [Description("Greece"), DefaultValue("30")]
        GR,

        [Description("South Georgia and the South Sandwich Islands"), DefaultValue("")]
        GS,

        [Description("Guatemala"), DefaultValue("502")]
        GT,

        [Description("Guam"), DefaultValue("1-671")]
        GU,

        [Description("Guinea-Bissau"), DefaultValue("245")]
        GW,

        [Description("Guyana"), DefaultValue("592")]
        GY,

        [Description("Hong Kong"), DefaultValue("852")]
        HK,

        [Description("Heard Island and McDonald Islands"), DefaultValue("")]
        HM,

        [Description("Honduras"), DefaultValue("504")]
        HN,

        [Description("Croatia"), DefaultValue("385")]
        HR,

        [Description("Haiti"), DefaultValue("509")]
        HT,

        [Description("Hungary"), DefaultValue("36")]
        HU,

        [Description("Indonesia"), DefaultValue("62")]
        ID,

        [Description("Ireland"), DefaultValue("353")]
        IE,

        [Description("Israel"), DefaultValue("972")]
        IL,

        [Description("Isle of Man"), DefaultValue("44-1624")]
        IM,

        [Description("India"), DefaultValue("91")]
        IN,

        [Description("British Indian Ocean Territory"), DefaultValue("246")]
        IO,

        [Description("Iraq"), DefaultValue("964")]
        IQ,

        [Description("Iran"), DefaultValue("98")]
        IR,

        [Description("Iceland"), DefaultValue("354")]
        IS,

        [Description("Italy"), DefaultValue("39")]
        IT,

        [Description("Jersey"), DefaultValue("44-1534")]
        JE,

        [Description("Jamaica"), DefaultValue("1-876")]
        JM,

        [Description("Jordan"), DefaultValue("962")]
        JO,

        [Description("Japan"), DefaultValue("81")]
        JP,

        [Description("Kenya"), DefaultValue("254")]
        KE,

        [Description("Kyrgyzstan"), DefaultValue("996")]
        KG,

        [Description("Cambodia"), DefaultValue("855")]
        KH,

        [Description("Kiribati"), DefaultValue("686")]
        KI,

        [Description("Comoros"), DefaultValue("269")]
        KM,

        [Description("Saint Kitts and Nevis"), DefaultValue("1-869")]
        KN,

        [Description("North Korea"), DefaultValue("850")]
        KP,

        [Description("South Korea"), DefaultValue("82")]
        KR,

        [Description("Kuwait"), DefaultValue("965")]
        KW,

        [Description("Cayman Islands"), DefaultValue("1-345")]
        KY,

        [Description("Kazakhstan"), DefaultValue("7")]
        KZ,

        [Description("Laos"), DefaultValue("856")]
        LA,

        [Description("Lebanon"), DefaultValue("961")]
        LB,

        [Description("Saint Lucia"), DefaultValue("1-758")]
        LC,

        [Description("Liechtenstein"), DefaultValue("423")]
        LI,

        [Description("Sri Lanka"), DefaultValue("94")]
        LK,

        [Description("Liberia"), DefaultValue("231")]
        LR,

        [Description("Lesotho"), DefaultValue("266")]
        LS,

        [Description("Lithuania"), DefaultValue("370")]
        LT,

        [Description("Luxembourg"), DefaultValue("352")]
        LU,

        [Description("Latvia"), DefaultValue("371")]
        LV,

        [Description("Libya"), DefaultValue("218")]
        LY,

        [Description("Morocco"), DefaultValue("212")]
        MA,

        [Description("Monaco"), DefaultValue("377")]
        MC,

        [Description("Moldova"), DefaultValue("373")]
        MD,

        [Description("Montenegro"), DefaultValue("382")]
        ME,

        [Description("Saint Martin"), DefaultValue("590")]
        MF,

        [Description("Madagascar"), DefaultValue("261")]
        MG,

        [Description("Marshall Islands"), DefaultValue("692")]
        MH,

        [Description("Macedonia"), DefaultValue("389")]
        MK,

        [Description("Mali"), DefaultValue("223")]
        ML,

        [Description("Myanmar"), DefaultValue("95")]
        MM,

        [Description("Mongolia"), DefaultValue("976")]
        MN,

        [Description("Macau"), DefaultValue("853")]
        MO,

        [Description("Northern Mariana Islands"), DefaultValue("1-670")]
        MP,

        [Description("Martinique"), DefaultValue("")]
        MQ,

        [Description("Mauritania"), DefaultValue("222")]
        MR,

        [Description("Montserrat"), DefaultValue("1-664")]
        MS,

        [Description("Malta"), DefaultValue("356")]
        MT,

        [Description("Mauritius"), DefaultValue("230")]
        MU,

        [Description("Maldives"), DefaultValue("960")]
        MV,

        [Description("Malawi"), DefaultValue("265")]
        MW,

        [Description("Mexico"), DefaultValue("52")]
        MX,

        [Description("Malaysia"), DefaultValue("60")]
        MY,

        [Description("Mozambique"), DefaultValue("258")]
        MZ,

        [Description("Namibia"), DefaultValue("264")]
        NA,

        [Description("New Caledonia"), DefaultValue("687")]
        NC,

        [Description("Niger"), DefaultValue("227")]
        NE,

        [Description("Norfolk Island"), DefaultValue("")]
        NF,

        [Description("Nigeria"), DefaultValue("234")]
        NG,

        [Description("Nicaragua"), DefaultValue("505")]
        NI,

        [Description("Netherlands"), DefaultValue("31")]
        NL,

        [Description("Norway"), DefaultValue("47")]
        NO,

        [Description("Nepal"), DefaultValue("977")]
        NP,

        [Description("Nauru"), DefaultValue("674")]
        NR,

        [Description("Niue"), DefaultValue("683")]
        NU,

        [Description("New Zealand"), DefaultValue("64")]
        NZ,

        [Description("Oman"), DefaultValue("968")]
        OM,

        [Description("Panama"), DefaultValue("507")]
        PA,

        [Description("Peru"), DefaultValue("51")]
        PE,

        [Description("French Polynesia"), DefaultValue("689")]
        PF,

        [Description("Papua New Guinea"), DefaultValue("675")]
        PG,

        [Description("Philippines"), DefaultValue("63")]
        PH,

        [Description("Pakistan"), DefaultValue("92")]
        PK,

        [Description("Poland"), DefaultValue("48")]
        PL,

        [Description("Saint Pierre and Miquelon"), DefaultValue("508")]
        PM,

        [Description("Pitcairn"), DefaultValue("64")]
        PN,

        [Description("Puerto Rico"), DefaultValue("1-787, 1-939")]
        PR,

        [Description("Palestine"), DefaultValue("970")]
        PS,

        [Description("Portugal"), DefaultValue("351")]
        PT,

        [Description("Palau"), DefaultValue("680")]
        PW,

        [Description("Paraguay"), DefaultValue("595")]
        PY,

        [Description("Qatar"), DefaultValue("974")]
        QA,

        [Description("Réunion"), DefaultValue("262")]
        RE,

        [Description("Romania"), DefaultValue("40")]
        RO,

        [Description("Serbia"), DefaultValue("381")]
        RS,

        [Description("Russia"), DefaultValue("7")]
        RU,

        [Description("Rwanda"), DefaultValue("250")]
        RW,

        [Description("Saudi Arabia"), DefaultValue("966")]
        SA,

        [Description("Solomon Islands"), DefaultValue("677")]
        SB,

        [Description("Seychelles"), DefaultValue("248")]
        SC,

        [Description("Sudan"), DefaultValue("249")]
        SD,

        [Description("Sweden"), DefaultValue("46")]
        SE,

        [Description("Singapore"), DefaultValue("65")]
        SG,

        [Description("Saint Helena"), DefaultValue("290")]
        SH,

        [Description("Slovenia"), DefaultValue("386")]
        SI,

        [Description("Svalbard and Jan Mayen"), DefaultValue("47")]
        SJ,

        [Description("Slovakia"), DefaultValue("421")]
        SK,

        [Description("Sierra Leone"), DefaultValue("232")]
        SL,

        [Description("San Marino"), DefaultValue("378")]
        SM,

        [Description("Senegal"), DefaultValue("221")]
        SN,

        [Description("Somalia"), DefaultValue("252")]
        SO,

        [Description("Suriname"), DefaultValue("597")]
        SR,

        [Description("South Sudan"), DefaultValue("211")]
        SS,

        [Description("Sao Tome and Principe"), DefaultValue("239")]
        ST,

        [Description("El Salvador"), DefaultValue("503")]
        SV,

        [Description("Sint Maarten"), DefaultValue("1-721")]
        SX,

        [Description("Syria"), DefaultValue("963")]
        SY,

        [Description("Swaziland"), DefaultValue("268")]
        SZ,

        [Description("Turks and Caicos Islands"), DefaultValue("1-649")]
        TC,

        [Description("Chad"), DefaultValue("235")]
        TD,

        [Description("French Southern Territories"), DefaultValue("")]
        TF,

        [Description("Togo"), DefaultValue("228")]
        TG,

        [Description("Thailand"), DefaultValue("66")]
        TH,

        [Description("Tajikistan"), DefaultValue("992")]
        TJ,

        [Description("Tokelau"), DefaultValue("690")]
        TK,

        [Description("East Timor"), DefaultValue("670")]
        TL,

        [Description("Turkmenistan"), DefaultValue("993")]
        TM,

        [Description("Tunisia"), DefaultValue("216")]
        TN,

        [Description("Tonga"), DefaultValue("676")]
        TO,

        [Description("Turkey"), DefaultValue("90")]
        TR,

        [Description("Trinidad and Tobago"), DefaultValue("1-868")]
        TT,

        [Description("Tuvalu"), DefaultValue("688")]
        TV,

        [Description("Taiwan"), DefaultValue("886")]
        TW,

        [Description("Tanzania"), DefaultValue("255")]
        TZ,

        [Description("Ukraine"), DefaultValue("380")]
        UA,

        [Description("Uganda"), DefaultValue("256")]
        UG,

        [Description("United States Minor Outlying Islands"), DefaultValue("")]
        UM,

        [Description("United States of America"), DefaultValue("1")]
        US,

        [Description("Uruguay"), DefaultValue("598")]
        UY,

        [Description("Uzbekistan"), DefaultValue("998")]
        UZ,

        [Description("Holy See"), DefaultValue("")]
        VA,

        [Description("Saint Vincent and the Grenadines"), DefaultValue("1-784")]
        VC,

        [Description("Venezuela"), DefaultValue("58")]
        VE,

        [Description("British Virgin Islands"), DefaultValue("1-284")]
        VG,

        [Description("U.S. Virgin Islands"), DefaultValue("1-340")]
        VI,

        [Description("Vietnam"), DefaultValue("84")]
        VN,

        [Description("Vanuatu"), DefaultValue("678")]
        VU,

        [Description("Wallis and Futuna"), DefaultValue("681")]
        WF,

        [Description("Samoa"), DefaultValue("685")]
        WS,

        [Description("Yemen"), DefaultValue("967")]
        YE,

        [Description("Mayotte"), DefaultValue("262")]
        YT,

        [Description("Uganda"), DefaultValue("27")]
        ZA,

        [Description("Zambia"), DefaultValue("260")]
        ZM,

        [Description("Zimbabwe"), DefaultValue("263")]
        ZW
    }
}