//Apartment ipls
const preloadIPLs = [
    "apa_v_mp_h_01_a",
    "apa_v_mp_h_01_c",
    "apa_v_mp_h_01_b",
    "apa_v_mp_h_02_a",
    "apa_v_mp_h_02_c",
    "apa_v_mp_h_02_b",
    "apa_v_mp_h_03_a",
    "apa_v_mp_h_03_c",
    "apa_v_mp_h_03_b",
    "apa_v_mp_h_04_a",
    "apa_v_mp_h_04_c",
    "apa_v_mp_h_04_b",
    "apa_v_mp_h_05_a",
    "apa_v_mp_h_05_c",
    "apa_v_mp_h_05_b",
    "apa_v_mp_h_06_a",
    "apa_v_mp_h_06_c",
    "apa_v_mp_h_06_b",
    "apa_v_mp_h_07_a",
    "apa_v_mp_h_07_c",
    "apa_v_mp_h_07_b",
    "apa_v_mp_h_08_a",
    "apa_v_mp_h_08_c",
    "apa_v_mp_h_08_b"

];

mp.events.add("Client:LoadIPL", (ipl) => {
    if (ipl) {
        if (ipl.includes("apa_v_")) {
            for (var i = 0; i < preloadIPLs.length; i++) {
                if (mp.game.streaming.isIplActive(preloadIPLs[i])) mp.game.streaming.removeIpl(preloadIPLs[i]);
            }
        }
        setTimeout(function () {
            if (!mp.game.streaming.isIplActive(ipl)) mp.game.streaming.requestIpl(ipl);
        }, 150);
        mp.events.call('Client:RefreshTuningInterior', ipl);
    }
})

//Tuning Garage
let tuningId = mp.game.interior.getInteriorAtCoords(-1350.0, 160.0, -100.0);
let tuningDlcInteriorsPre = [
    "entity_set_style_1",
    "entity_set_style_2",
    "entity_set_style_3",
    "entity_set_style_4",
    "entity_set_style_5",
    "entity_set_style_6",
    "entity_set_style_7",
    "entity_set_style_8",
    "entity_set_style_9",
];
for (const prop of tuningDlcInteriorsPre) {
    mp.game.interior.disableInteriorProp(tuningId, prop);
}

let phPropList = [
    "entity_set_bedroom",
    "entity_set_bombs",
    "entity_set_cabinets",
    "entity_set_car_lift_cutscene",
    "entity_set_car_lift_default",
    "entity_set_car_lift_purchase",
    "entity_set_chalkboard",
    "entity_set_container",
    "entity_set_cut_seats",
    "entity_set_def_table",
    "entity_set_drive",
    "entity_set_ecu",
    "entity_set_IAA",
    "entity_set_jammers",
    "entity_set_laptop",
    "entity_set_lightbox",
    "entity_set_plate",
    "entity_set_scope",
    "entity_set_style_9",
    "entity_set_thermal",
    "entity_set_tints",
    "entity_set_train",
    "entity_set_virus",
];
for (const propName of phPropList) {
    mp.game.interior.enableInteriorProp(tuningId, propName);
    mp.game.invoke("0xC1F1920BAF281317", tuningId, propName, 1);
}
mp.game.interior.refreshInterior(tuningId);

mp.events.add("Client:RefreshTuningInterior", (ipl) => {
    if (ipl.includes('tuningdlc_interior')) {
        let tuningDlcInteriors = [
            "entity_set_style_1",
            "entity_set_style_2",
            "entity_set_style_3",
            "entity_set_style_4",
            "entity_set_style_5",
            "entity_set_style_6",
            "entity_set_style_7",
            "entity_set_style_8",
            "entity_set_style_9",
        ];
        for (const prop of tuningDlcInteriors) {
            mp.game.interior.disableInteriorProp(tuningId, prop);
        }
        mp.game.interior.enableInteriorProp(tuningId, "entity_set_style_" + ipl[ipl.length - 1]);
        mp.game.invoke("0xC1F1920BAF281317", tuningId, "entity_set_style_" + ipl[ipl.length - 1], 1);
        mp.game.interior.refreshInterior(tuningId);
    }
})