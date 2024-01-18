-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 01. Mai 2023 um 13:08
-- Server-Version: 10.4.27-MariaDB
-- PHP-Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `nworld`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `adminlogs`
--

CREATE TABLE `adminlogs` (
  `id` int(11) NOT NULL,
  `loglabel` varchar(35) NOT NULL,
  `text` varchar(256) NOT NULL,
  `ip` varchar(64) DEFAULT NULL,
  `miscellaneous` int(11) DEFAULT 10,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `adminlogs`
--

INSERT INTO `adminlogs` (`id`, `loglabel`, `text`, `ip`, `miscellaneous`, `timestamp`) VALUES
(1, 'loginlog', 'Testuser hat sich erfolgreich in den Account (ID: 1) eingeloggt!', '127.0.0.1', 18021891, 1682942633);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `adminlogsnames`
--

CREATE TABLE `adminlogsnames` (
  `id` int(11) NOT NULL,
  `loglabel` varchar(35) NOT NULL,
  `name` varchar(35) NOT NULL,
  `rang` int(2) NOT NULL DEFAULT 1,
  `checkip` int(1) NOT NULL DEFAULT 0,
  `miscellaneous` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `adminsettings`
--

CREATE TABLE `adminsettings` (
  `id` int(11) NOT NULL,
  `adminpassword` varchar(255) NOT NULL,
  `server_created` int(11) NOT NULL DEFAULT 1609462861,
  `punishments` int(7) NOT NULL DEFAULT 0,
  `voicerp` int(1) NOT NULL DEFAULT 1,
  `govvalue` int(11) NOT NULL DEFAULT 0,
  `changelogcd` int(11) NOT NULL DEFAULT 1609462861,
  `towedcash` int(4) NOT NULL DEFAULT 250,
  `lsteuer` int(3) NOT NULL DEFAULT 7,
  `gsteuer` int(3) NOT NULL DEFAULT 5,
  `ksteuer` float NOT NULL DEFAULT 0.25,
  `groupsettings` varchar(150) NOT NULL DEFAULT '7,5,0.12,250,500,500,450,2500,500,35000,55000,60000,65000,60000',
  `adcount` int(5) NOT NULL DEFAULT 0,
  `adprice` int(5) NOT NULL DEFAULT 1500,
  `admoney` int(11) NOT NULL DEFAULT 0,
  `dailyguesslimit` int(6) NOT NULL DEFAULT 0,
  `nametag` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `adminsettings`
--

INSERT INTO `adminsettings` (`id`, `adminpassword`, `server_created`, `punishments`, `voicerp`, `govvalue`, `changelogcd`, `towedcash`, `lsteuer`, `gsteuer`, `ksteuer`, `groupsettings`, `adcount`, `adprice`, `admoney`, `dailyguesslimit`, `nametag`) VALUES
(1, '$2y$10$6n4eP023KIEGINK1j6XE5Op3EDlh.t0BLMm4fXjEyh5CIbA6ra4t6', 1683446400, 0, 1, 100000, 1683446400, 250, 1, 1, 1, '1,1,250,250,30,30,12,250,950,25000,5000,3500,65000,65000', 0, 1500, 1500, 10000, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `animations`
--

CREATE TABLE `animations` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `animation` varchar(128) NOT NULL,
  `category` varchar(64) NOT NULL,
  `duration` int(5) NOT NULL DEFAULT -1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `animations`
--

INSERT INTO `animations` (`id`, `name`, `animation`, `category`, `duration`) VALUES
(1, 'salute', 'anim@mp_player_intuppersalute%idle_a%1', 'interaktion', -1),
(2, 'smokecigarette', 'WORLD_HUMAN_AA_SMOKE', 'interaktion', -1),
(3, 'smoke', 'amb@world_human_aa_smoke@male@idle_a%idle_a%1', 'interaktion', -1),
(4, 'handshake', 'mp_ped_interaction%handshake_guy_a%0', 'interaktion', 3000),
(5, 'handshake2', 'mp_ped_interaction%handshake_guy_b%0', 'interaktion', 3000),
(6, 'kiss', 'mp_ped_interaction%kisses_guy_a%0', 'interaktion', 3000),
(7, 'kiss2', 'mp_ped_interaction%kisses_guy_b%0', 'interaktion', 3000),
(8, 'hug', 'mp_ped_interaction%hugs_guy_a%0', 'interaktion', 4000),
(9, 'hug2', 'mp_ped_interaction%hugs_guy_b%0', 'interaktion', 4000),
(10, 'give', 'mp_common%givetake1_a%0', 'interaktion', 2850),
(11, 'give2', 'mp_common%givetake1_b%0', 'interaktion', 3000),
(12, 'baseball', 'anim@arena@celeb@flat@paired@no_props@%baseball_a_player_a%0', 'sport', 6000),
(13, 'punch', 'melee@unarmed@streamed_variations%plyr_takedown_rear_lefthook%0', 'kampfanimation', 2000),
(14, 'punched', 'melee@unarmed@streamed_variations%victim_takedown_front_cross_r%0', 'kampfanimation', 2000),
(15, 'headbutt', 'melee@unarmed@streamed_variations%plyr_takedown_front_headbutt%0', 'kampfanimation', 2000),
(16, 'headbutted', 'melee@unarmed@streamed_variations%victim_takedown_front_headbutt%0', 'kampfanimation', 2000),
(17, 'slap2', 'melee@unarmed@streamed_variations%plyr_takedown_front_backslap%0', 'kampfanimation', 2000),
(18, 'slap', 'melee@unarmed@streamed_variations%plyr_takedown_front_slap%0', 'kampfanimation', 2000),
(19, 'slapped', 'melee@unarmed@streamed_variations%victim_takedown_front_slap%0', 'kampfanimation', 2000),
(20, 'slapped2', 'melee@unarmed@streamed_variations%victim_takedown_front_backslap%0', 'kampfanimation', 2000),
(21, 'dancef', 'anim@amb@nightclub@dancers@solomun_entourage@%mi_dance_facedj_17_v1_female^1%47', 'tanzen', 32),
(22, 'dancef2', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_a@%high_center%47', 'tanzen', 32),
(23, 'dancef3', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_a@%high_center_up%47', 'tanzen', 32),
(24, 'dancef4', 'anim@amb@nightclub@dancers@crowddance_facedj@hi_intensity%hi_dance_facedj_09_v2_female^1%47', 'tanzen', 32),
(25, 'dancef5', 'anim@amb@nightclub@dancers@crowddance_facedj@hi_intensity%hi_dance_facedj_09_v2_female^3%47', 'tanzen', 32),
(26, 'danceslow', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_a@%low_center%47', 'tanzen', 32),
(27, 'danceslow2', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_a@%low_center_down%47', 'tanzen', 32),
(28, 'dance', 'anim@amb@nightclub@dancers@podium_dancers@%hi_dance_facedj_17_v2_male^5%47', 'tanzen', -1),
(29, 'dance2', 'anim@amb@nightclub@mini@dance@dance_solo@male@var_b@%high_center_down%47', 'tanzen', -1),
(30, 'dance3', 'anim@amb@nightclub@mini@dance@dance_solo@male@var_b@%high_center%47', 'tanzen', -1),
(31, 'dance4', 'anim@amb@nightclub@mini@dance@dance_solo@male@var_b@%high_center_up%47', 'tanzen', -1),
(32, 'dance5', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_b@%high_center%1', 'tanzen', -1),
(33, 'dance6', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_b@%high_center_up%1', 'tanzen', -1),
(34, 'danceshy', 'anim@amb@nightclub@mini@dance@dance_solo@male@var_a@%low_center%47', 'tanzen', -1),
(35, 'danceshy2', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_b@%low_center_down%47', 'tanzen', -1),
(36, 'danceslow3', 'anim@amb@nightclub@mini@dance@dance_solo@male@var_b@%low_center%47', 'tanzen', -1),
(37, 'dancesilly9', 'rcmnigel1bnmt_1b%dance_loop_tyler%47', 'tanzen', -1),
(38, 'dance7', 'misschinese2_crystalmazemcs1_cs%dance_loop_tao%47', 'tanzen', -1),
(39, 'dance8', 'misschinese2_crystalmazemcs1_ig%dance_loop_tao%47', 'tanzen', -1),
(40, 'dance9', 'missfbi3_sniping%dance_m_default%47', 'tanzen', -1),
(41, 'dancesilly', 'special_ped@mountain_dancer@monologue_3@monologue_3a%mnt_dnc_buttwag%47', 'tanzen', -1),
(42, 'dancesilly2', 'move_clown@p_m_zero_idles@%fidget_short_dance%47', 'tanzen', -1),
(43, 'dancesilly3', 'move_clown@p_m_two_idles@\"%fidget_short_dance%47', 'tanzen', -1),
(44, 'dancesilly4', 'anim@amb@nightclub@lazlow@hi_podium@%danceidle_hi_11_buttwiggle_b_laz%47', 'tanzen', -1),
(45, 'dancesilly5', 'timetable@tracy@ig_5@idle_a%idle_a%47', 'tanzen', -1),
(46, 'dancesilly6', 'timetable@tracy@ig_8@idle_b%idle_d%47', 'tanzen', -1),
(47, 'dance10', 'anim@amb@nightclub@mini@dance@dance_solo@female@var_a@%med_center_up%47', 'tanzen', -1),
(48, 'dancesilly8', 'anim@mp_player_intcelebrationfemale@the_woogie%the_woogie%47', 'tanzen', -1),
(49, 'dancesilly7', 'anim@amb@casino@mini@dance@dance_solo@female@var_b@%high_center%47', 'tanzen', -1),
(50, 'dance11', 'anim@amb@casino@mini@dance@dance_solo@female@var_a@%med_center%47', 'tanzen', -1),
(51, 'danceglowstick', 'anim@amb@nightclub@lazlow@hi_railing@%ambclub_13_mi_hi_sexualgriding_laz%47', 'tanzen', -1),
(52, 'danceglowstick2', 'anim@amb@nightclub@lazlow@hi_railing@%ambclub_12_mi_hi_bootyshake_laz%47', 'tanzen', -1),
(53, 'danceglowstick3', 'anim@amb@nightclub@lazlow@hi_railing@%ambclub_09_mi_hi_bellydancer_laz%47', 'tanzen', -1),
(54, 'dancehorse', 'anim@amb@nightclub@lazlow@hi_dancefloor@%dancecrowd_li_15_handup_laz%47', 'tanzen', -1),
(55, 'dancehorse2', 'anim@amb@nightclub@lazlow@hi_dancefloor@%crowddance_hi_11_handup_laz%47', 'tanzen', -1),
(56, 'dancehorse3', 'anim@amb@nightclub@lazlow@hi_dancefloor@%dancecrowd_li_11_hu_shimmy_laz%47', 'tanzen', -1),
(57, 'drink', 'mp_player_inteat@pnq%loop%0', 'emotes', 2000),
(58, 'beast', 'anim@mp_fm_event@intro%beast_transform%0', 'emotes', 5000),
(59, 'chill', 'switch@trevor@scares_tramp%trev_scares_tramp_idle_tramp%1', 'liegend', -1),
(60, 'cloudgaze', 'switch@trevor@annoys_sunbathers%trev_annoys_sunbathers_loop_girl%1', 'liegend', -1),
(61, 'cloudgaze2', 'switch@trevor@annoys_sunbathers%trev_annoys_sunbathers_loop_guy%1', 'liegend', -1),
(62, 'prone', 'missfbi3_sniping%prone_dave%1', 'liegend', -1),
(63, 'pullover', 'misscarsteal3pullover%pull_over_right%0', 'emotes', 1300),
(64, 'idle', 'anim@heists@heist_corona@team_idles@male_a%idle%1', 'stehend', -1),
(65, 'idle8', 'amb@world_human_hang_out_street@male_b@idle_a%idle_b%1', 'stehend', -1),
(66, 'idle9', 'friends@fra@ig_1%base_idle%1', 'stehend', -1),
(67, 'idle10', 'mp_move@prostitute@m@french%idle%1', 'stehend', -1),
(68, 'idle11', 'random@countrysiderobbery%idle_a%1', 'stehend', -1),
(69, 'idle2', 'anim@heists@heist_corona@team_idles@female_a%idle%1', 'stehend', -1),
(70, 'idle3', 'anim@heists@humane_labs@finale@strip_club%ped_b_celebrate_loop%1', 'stehend', -1),
(71, 'idle4', 'anim@mp_celebration@idles@female%celebration_idle_f_a%1', 'stehend', -1),
(72, 'idle5', 'anim@mp_corona_idles@female_b@idle_a%idle_a%1', 'stehend', -1),
(73, 'idle6', 'anim@mp_corona_idles@male_c@idle_a%idle_a%1', 'stehend', -1),
(74, 'idle7', 'anim@mp_corona_idles@male_d@idle_a%idle_a%1', 'stehend', -1),
(75, 'wait3', 'amb@world_human_hang_out_street@female_hold_arm@idle_a%idle_a%1', 'stehend', -1),
(76, 'idledrunk', 'random@drunk_driver_1%drunk_driver_stand_loop_dd1%1', 'stehend', -1),
(77, 'idledrunk2', 'random@drunk_driver_1%drunk_driver_stand_loop_dd2%1', 'stehend', -1),
(78, 'idledrunk3', 'missarmenian2%standing_idle_loop_drunk%1', 'stehend', -1),
(79, 'airguitar', 'anim@mp_player_intcelebrationfemale@air_guitar%air_guitar%1', 'tanzen', -1),
(80, 'airsynth', 'anim@mp_player_intcelebrationfemale@air_synth%air_synth%1', 'tanzen', -1),
(81, 'argue', 'misscarsteal4@actor%actor_berating_loop%1', 'emotes', -1),
(82, 'bartender', 'anim@amb@clubhouse@bar@drink@idle_a%idle_a_bartender%1', 'stehend', -1),
(83, 'blowkiss', 'anim@mp_player_intcelebrationfemale@blow_kiss%blow_kiss%0', 'emotes', 5000),
(84, 'curtsy', 'anim@mp_player_intcelebrationpaired@f_f_sarcastic%sarcastic_left%0', 'emotes', 3000),
(85, 'bringiton', 'misscommon@response%bring_it_on%0', 'emotes', 3000),
(86, 'comeatmebro', 'mini@triathlon%want_some_of_this%0', 'emotes', 2000),
(87, 'cop2', 'anim@amb@nightclub@peds@%rcmme_amanda1_stand_loop_cop%0', 'stehend', 32),
(88, 'cop3', 'amb@code_human_police_investigate@idle_a%idle_b%0', 'emotes', 32),
(89, 'crossarms', 'amb@world_human_hang_out_street@female_arms_crossed@idle_a%idle_a%1', 'stehend', 32),
(90, 'crossarms2', 'amb@world_human_hang_out_street@male_c@idle_a%idle_b%1', 'stehend', 32),
(94, 'crossarms3', 'anim@heists@heist_corona@single_team%single_team_loop_boss%1', 'stehend', 32),
(95, 'crossarms4', 'random@street_race%_car_b_lookout%1', 'stehend', 32),
(96, 'crossarms5', 'anim@amb@nightclub@peds@%rcmme_amanda1_stand_loop_cop%1', 'stehend', 32),
(97, 'crossarms6', 'random@shop_gunstore%_idle%1', 'stehend', 32),
(98, 'foldarms', 'anim@amb@business@bgen@bgen_no_work@%stand_phone_phoneputdown_idle_nowork%0', 'stehend', 32),
(99, 'crossarmsside', 'rcmnigel1a_band_groupies%base_m2%1', 'stehend', 32),
(100, 'damn', 'gestures@m@standing@casual%gesture_damn%0', 'emotes', 1000),
(101, 'damn2', 'anim@am_hold_up@male%shoplift_mid%0', 'emotes', 1000),
(102, 'pointdown', 'gestures@f@standing@casual%gesture_hand_down%0', 'emotes', 1000),
(103, 'surrender', 'random@arrests@busted%idle_a%1', 'emotes', -1),
(104, 'facepalm2', 'anim@mp_player_intcelebrationfemale@face_palm%face_palm%0', 'emotes', 8000),
(105, 'facepalm', 'random@car_thief@agitated@idle_a%agitated_idle_a%0', 'emotes', 8000),
(106, 'facepalm3', 'missminuteman_1ig_2%tasered_2%0', 'emotes', 8000),
(107, 'facepalm4', 'anim@mp_player_intupperface_palm%idle_a%0', 'emotes', 8000),
(108, 'fallover', 'random@drunk_driver_1%drunk_fall_over%1', 'umfallen', -1),
(109, 'fallover2', 'mp_suicide%pistol%1', 'umfallen', -1),
(110, 'fallover3', 'mp_suicide%pill%1', 'umfallen', -1),
(111, 'fallover4', 'friends@frf@ig_2%knockout_plyr%1', 'umfallen', -1),
(112, 'fallover5', 'anim@gangops@hostage@%victim_fail%1', 'umfallen', -1),
(113, 'fallasleep', 'mp_sleep%sleep_loop%1', 'stehend', -1),
(114, 'fightme', 'anim@deathmatch_intros@unarmed%intro_male_unarmed_c%1', 'emotes', -1),
(115, 'fightme2', 'anim@deathmatch_intros@unarmed%intro_male_unarmed_e%1', 'emotes', -1),
(116, 'middlefinger', 'anim@mp_player_intselfiethe_bird%idle_a%0', 'emotes', 32),
(117, 'middlefinger2', 'anim@mp_player_intupperfinger%idle_a_fp%0', 'emotes', 32),
(118, 'wait4', 'amb@world_human_hang_out_street@female_arm_side@idle_a%idle_a%1', 'stehend', -1),
(119, 'wait5', 'missclothing%idle_storeclerk%1', 'stehend', -1),
(120, 'wait6', 'timetable@amanda@ig_2%ig_2_base_amanda%1', 'stehend', -1),
(121, 'wait7', 'rcmnigel1cnmt_1c%base%1', 'stehend', -1),
(122, 'wait8', 'rcmjosh1%idle%1', 'stehend', -1),
(123, 'wait9', 'rcmjosh2%josh_2_intp1_base%1', 'stehend', -1),
(124, 'wait10', 'timetable@amanda@ig_3%ig_3_base_tracy%1', 'stehend', -1),
(125, 'wait11', 'misshair_shop@hair_dressers%keeper_base%1', 'stehend', -1),
(126, 'hiking', 'move_m@hiking%idle%0', 'laufend', 4500),
(127, 'hug3', 'mp_ped_interaction%hugs_guy_a%0', 'interaktion', 6000),
(128, 'inspect', 'random@train_tracks%idle_e%0', 'interaktion', 12000),
(129, 'jazzhands', 'anim@mp_player_intcelebrationfemale@jazz_hands%jazz_hands%0', 'interaktion', 6000),
(130, 'jog2', 'amb@world_human_jog_standing@male@idle_a%idle_a%0', 'laufend', 4500),
(131, 'jog3', 'amb@world_human_jog_standing@female@idle_a%idle_a%0', 'laufend', 4500),
(132, 'jog4', 'amb@world_human_power_walker@female@idle_a%idle_a%0', 'laufend', 4500),
(133, 'jog5', 'move_m@joy@a%walk%0', 'laufend', 4500),
(134, 'jumpingjacks', 'timetable@reunited@ig_2%jimmy_getknocked%1', 'sport', -1),
(135, 'knee12', 'rcmextreme3%idle%1', 'kniend', -1),
(136, 'knee13', 'amb@world_human_bum_wash@male@low@idle_a%idle_a%1', 'kniend', -1),
(137, 'knock', 'timetable@jimmy@doorknock@%knockdoor_idle%1', 'interaktion', -1),
(138, 'knock2', 'missheistfbi3b_ig7%lift_fibagent_loop%1', 'interaktion', -1),
(139, 'knucklecrunch', 'anim@mp_player_intcelebrationfemale@knuckle_crunch%knuckle_crunch%1', 'emotes', -1),
(140, 'lapdance', 'mp_safehouse%lap_dance_girl%47', 'tanzen', -1),
(141, 'lean2', 'amb@world_human_leaning@female@wall@back@hand_up@idle_a%idle_a%1', 'stehend', -1),
(142, 'lean3', 'amb@world_human_leaning@female@wall@back@holding_elbow@idle_a%idle_a%1', 'stehend', -1),
(143, 'lean4', 'amb@world_human_leaning@male@wall@back@foot_up@idle_a%idle_a%1', 'stehend', -1),
(144, 'lean5', 'amb@world_human_leaning@male@wall@back@hands_together@idle_b%idle_b%1', 'stehend', -1),
(145, 'leanflirt', 'random@street_race%_car_a_flirt_girl%1', 'stehend', -1),
(146, 'leanbar2', 'amb@prop_human_bum_shopping_cart@male@idle_a%idle_c%1', 'stehend', -1),
(147, 'leanbar3', 'anim@amb@nightclub@lazlow@ig1_vip@%clubvip_base_laz%1', 'stehend', -1),
(148, 'leanbar4', 'anim@heists@prison_heist%ped_b_loop_a%1', 'stehend', -1),
(149, 'leanhigh', 'anim@mp_ferris_wheel%idle_a_player_one%1', 'stehend', -1),
(150, 'leanhigh2', 'anim@mp_ferris_wheel%idle_a_player_two%1', 'stehend', -1),
(151, 'leanside', 'timetable@mime@01_gc%idle_a%1', 'stehend', -1),
(152, 'leanside2', 'misscarstealfinale%packer_idle_1_trevor%1', 'stehend', -1),
(153, 'leanside3', 'misscarstealfinalecar_5_ig_1%waitloop_lamar%1', 'stehend', -1),
(154, 'leanside4', 'rcmjosh2%josh_2_intp1_base%1', 'stehend', -1),
(155, 'mechanic', 'mini@repair%fixing_a_ped%1', 'interaktion', -1),
(156, 'mechanic2', 'anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%1', 'interaktion', -1),
(157, 'medic2', 'amb@medic@standing@tendtodead@base%base%1', 'kniend', -1),
(158, 'meditate', 'rcmcollect_paperleadinout@%meditiate_idle%1', 'sport', -1),
(159, 'meditate2', 'rcmepsilonism3%ep_3_rcm_marnie_meditating%1', 'sport', -1),
(160, 'meditate3', 'rcmepsilonism3%base_loop%1', 'sport', -1),
(161, 'metal', 'anim@mp_player_intincarrockstd@ps@%idle_a%0', 'emotes', 32),
(162, 'no1', 'anim@heists@ornate_bank@chat_manager%fail%0', 'emotes', 32),
(163, 'no2', 'mp_player_int_upper_nod%mp_player_int_nod_no%0', 'emotes', 32),
(164, 'nosepick', 'anim@mp_player_intcelebrationfemale@nose_pick%nose_pick%0', 'emotes', 32),
(165, 'noway', 'gestures@m@standing@casual%gesture_no_way%0', 'emotes', 32),
(166, 'okay', 'anim@mp_player_intselfiedock%idle_a%0', 'emotes', 32),
(167, 'outofbreath', 're@construction%out_of_breath%1', 'sport', 32),
(168, 'push', 'missfinale_c2ig_11%pushcar_offcliff_f%0', 'interaktion', 32),
(169, 'pickup', 'random@domestic%pickup_low%0', 'interaktion', 2250),
(170, 'push2', 'missfinale_c2ig_11%pushcar_offcliff_m%0', 'interaktion', 32),
(171, 'point', 'gestures@f@standing@casual%gesture_point%0', 'emotes', 32),
(172, 'pushup', 'amb@world_human_push_ups@male@idle_a%idle_d%1', 'sport', -1),
(173, 'countdown', 'random@street_race%grid_girl_race_start%0', 'sport', 32),
(174, 'pointright', 'mp_gun_shop_tut%indicate_right%0', 'emotes', 32),
(178, 'salute2', 'anim@mp_player_intincarsalutestd@ds@%idle_a%0', 'emotes', 32),
(179, 'salute4', 'anim@mp_player_intincarsalutestd@ps@%idle_a%0', 'emotes', 32),
(180, 'salute3', 'anim@mp_player_intuppersalute%idle_a%0', 'emotes', 32),
(181, 'scared', 'random@domestic%f_distressed_loop%0', 'emotes', 32),
(182, 'scared2', 'random@homelandsecurity%knees_loop_girl%0', 'emotes', 32),
(183, 'screwyou', 'misscommon@response%screw_you%0', 'emotes', 32),
(184, 'shakeoff', 'move_m@_idles@shake_off%shakeoff_1%0', 'emotes', 3500),
(185, 'shot', 'random@dealgonewrong%idle_a%1', 'liegend', -1),
(186, 'sleep', 'timetable@tracy@sleep@%idle_c%1', 'liegend', -1),
(187, 'shrug', 'gestures@f@standing@casual%gesture_shrug_hard%0', 'emotes', 1000),
(188, 'shrug2', 'gestures@m@standing@casual%gesture_shrug_hard%0', 'emotes', 1000),
(189, 'sit', 'anim@amb@business@bgen@bgen_no_work@%sit_phone_phoneputdown_idle_nowork%1', 'sitzend', -1),
(190, 'sit2', 'rcm_barry3%barry_3_sit_loop%1', 'sitzend', -1),
(191, 'sit3', 'amb@world_human_picnic@male@idle_a%idle_a%1', 'sitzend', -1),
(192, 'sit4', 'amb@world_human_picnic@female@idle_a%idle_a%1', 'sitzend', -1),
(193, 'sit5', 'anim@heists@fleeca_bank@ig_7_jetski_owner%owner_idle%1', 'sitzend', -1),
(194, 'sit6', 'timetable@jimmy@mics3_ig_15@%idle_a_jimmy%1', 'sitzend', -1),
(195, 'sit7', 'anim@amb@nightclub@lazlow@lo_alone@%lowalone_base_laz%1', 'sitzend', -1),
(196, 'sit8', 'timetable@jimmy@mics3_ig_15@%mics3_15_base_jimmy%1', 'sitzend', -1),
(197, 'sit9', 'amb@world_human_stupor@male@idle_a%idle_a%1', 'sitzend', -1),
(198, 'sitlean', 'timetable@tracy@ig_14@%ig_14_base_tracy%1', 'sitzend', -1),
(199, 'sitsad', 'anim@amb@business@bgen@bgen_no_work@%sit_phone_phoneputdown_sleeping-noworkfemale%1', 'sitzend', -1),
(200, 'sitscared', 'anim@heists@ornate_bank@hostages@hit%hit_loop_ped_b%1', 'sitzend', -1),
(201, 'sitscared2', 'anim@heists@ornate_bank@hostages@ped_c@%flinch_loop%1', 'sitzend', -1),
(202, 'sitscared3', 'anim@heists@ornate_bank@hostages@ped_e@%flinch_loop%1', 'sitzend', -1),
(203, 'sitdrunk', 'timetable@amanda@drunk@base%base%1', 'sitzend', -1),
(204, 'sitchair2', 'timetable@ron@ig_5_p3%ig_5_p3_base%1', 'sitzend', -1),
(205, 'sitchair3', 'timetable@reunited@ig_10%base_amanda%1', 'sitzend', -1),
(206, 'sitchair4', 'timetable@ron@ig_3_couch%base%1', 'sitzend', -1),
(207, 'sitchair5', 'timetable@jimmy@mics3_ig_15@%mics3_15_base_tracy%1', 'sitzend', -1),
(208, 'sitchair6', 'timetable@maid@couch@%base%1', 'sitzend', -1),
(209, 'sitchairside', 'timetable@ron@ron_ig_2_alt1%ig_2_alt1_base%1', 'sitzend', -1),
(210, 'situp', 'amb@world_human_sit_ups@male@idle_a%idle_a%1', 'sport', -1),
(211, 'clapangry', 'anim@arena@celeb@flat@solo@no_props@%angry_clap_a_player_a%1', 'emotes', 2500),
(212, 'slowclap3', 'anim@mp_player_intupperslow_clap%idle_a%0', 'emotes', 2500),
(213, 'clap', 'amb@world_human_cheering@male_a%base%0', 'emotes', 2500),
(214, 'slowclap', 'anim@mp_player_intcelebrationfemale@slow_clap%slow_clap%0', 'emotes', 2500),
(215, 'slowclap2', 'anim@mp_player_intcelebrationmale@slow_clap%slow_clap%0', 'emotes', 2500),
(216, 'smell', 'move_p_m_two_idles@generic%fidget_sniff_fingers%0', 'emotes', 3500),
(217, 'stickup', 'random@countryside_gang_fight%biker_02_stickup_loop%0', 'kampfanimation', 3000),
(218, 'stumble', 'misscarsteal4@actor%stumble%1', 'emotes', -1),
(219, 'stunned', 'stungun@standing%damage%1', 'emotes', -1),
(220, 'sunbathe', 'amb@world_human_sunbathe@male@back@base%base%1', 'liegend', -1),
(221, 'sunbathe2', 'amb@world_human_sunbathe@female@back@base%base%1', 'liegend', -1),
(222, 'yogat', 'missfam5_yoga%a2_pose%1', 'sport', -1),
(223, 'yogat2', 'mp_sleep%bind_pose_180%1', 'sport', -1),
(224, 'think5', 'mp_cp_welcome_tutthink%b_think%0', 'emotes', 2000),
(225, 'think', 'misscarsteal4@aliens%rehearsal_base_idle_director%0', 'emotes', 32),
(226, 'think3', 'timetable@tracy@ig_8@base%base%0', 'emotes', 32),
(227, 'think2', 'missheist_jewelleadinout%jh_int_outro_loop_a%0', 'emotes', 32),
(228, 'thumbsup3', 'anim@mp_player_intincarthumbs_uplow@ds@%enter%0', 'emotes', 1000),
(229, 'thumbsup2', 'anim@mp_player_intselfiethumbs_up%idle_a%0', 'emotes', 32),
(230, 'thumbsup', 'anim@mp_player_intupperthumbs_up%idle_a%0', 'emotes', 32),
(231, 'type', 'anim@heists@prison_heiststation@cop_reactions%cop_b_idle%1', 'interaktion', -1),
(232, 'type2', 'anim@heists@prison_heistig1_p1_guard_checks_bus%loop%1', 'interaktion', -1),
(233, 'type3', 'mp_prison_break%hack_loop%1', 'interaktion', -1),
(234, 'type4', 'mp_fbi_heist%loop%1', 'interaktion', -1),
(235, 'warmth', 'amb@world_human_stand_fire@male@idle_a%idle_a%1', 'interaktion', -1),
(236, 'wave4', 'random@mugging5%001445_01_gangintimidation_1_female_idle_b%1', 'emotes', 3000),
(237, 'wave2', 'anim@mp_player_intcelebrationfemale@wave%wave%1', 'emotes', 2500),
(238, 'wave3', 'friends@fra@ig_1%over_here_idle_a%1', 'emotes', 2500),
(239, 'wave', 'friends@frj@ig_1%wave_a%1', 'emotes', 2500),
(240, 'wave5', 'friends@frj@ig_1%wave_b%1', 'emotes', 2500),
(241, 'wave6', 'friends@frj@ig_1%wave_c%1', 'emotes', 2500),
(242, 'wave7', 'friends@frj@ig_1%wave_d%1', 'emotes', 2500),
(243, 'wave8', 'friends@frj@ig_1%wave_e%1', 'emotes', 2500),
(244, 'wave9', 'gestures@m@standing@casual%gesture_hello%1', 'emotes', 2500),
(245, 'whistle', 'taxi_hail%hail_taxi%0', 'interaktion', 1300),
(246, 'whistle2', 'rcmnigel1c%hailing_whistle_waive_a%0', 'interaktion', 2000),
(247, 'yeah', 'anim@mp_player_intupperair_shagging%idle_a%0', 'emotes', 3000),
(248, 'lift', 'random@hitch_lift%idle_f%0', 'emotes', 32),
(249, 'lol', 'anim@arena@celeb@flat@paired@no_props@%laugh_a_player_b%0', 'emotes', 3000),
(250, 'lol2', 'anim@arena@celeb@flat@solo@no_props@%giggle_a_player_b%0', 'emotes', 3000),
(251, 'statue2', 'fra_0_int-1%cs_lamardavis_dual-1%0', 'emotes', 32),
(252, 'statue3', 'club_intro2-0%csb_englishdave_dual-0%0', 'emotes', 32),
(253, 'gangsign', 'mp_player_int_uppergang_sign_a%mp_player_int_gang_sign_a%1', 'emotes', -1),
(254, 'gangsign2', 'mp_player_int_uppergang_sign_b%mp_player_int_gang_sign_b%0', 'emotes', 32),
(255, 'passout', 'missarmenian2%drunk_loop%1', 'liegend', -1),
(256, 'passout2', 'missarmenian2%corpse_search_exit_ped%1', 'liegend', -1),
(257, 'passout3', 'anim@gangops@morgue@table@%body_search%1', 'liegend', -1),
(258, 'passout4', 'mini@cpr@char_b@cpr_def%cpr_pumpchest_idle%1', 'liegend', -1),
(259, 'passout5', 'random@mugging4%flee_backward_loop_shopkeeper%1', 'liegend', -1),
(260, 'petting', 'creatures@rottweiler@tricks@%petting_franklin%1', 'interaktion', -1),
(261, 'crawl', 'move_injured_ground%front_loop%1', 'liegend', -1),
(262, 'flip2', 'anim@arena@celeb@flat@solo@no_props@%cap_a_player_a%1', 'stunt', -1),
(263, 'flip', 'anim@arena@celeb@flat@solo@no_props@%flip_a_player_a%1', 'stunt', -1),
(264, 'slide', 'anim@arena@celeb@flat@solo@no_props@%slide_a_player_a%1', 'stunt', -1),
(265, 'slide2', 'anim@arena@celeb@flat@solo@no_props@%slide_b_player_a%1', 'stunt', -1),
(266, 'slide3', 'anim@arena@celeb@flat@solo@no_props@%slide_c_player_a%1', 'stunt', -1),
(267, 'slugger', 'anim@arena@celeb@flat@solo@no_props@%slugger_a_player_a%1', 'stunt', -1),
(268, 'flipoff', 'anim@arena@celeb@podium@no_prop@%flip_off_a_1st%1', 'emotes', -1),
(269, 'flipoff2', 'anim@arena@celeb@podium@no_prop@%flip_off_c_1st%1', 'emotes', -1),
(270, 'bow', 'anim@arena@celeb@podium@no_prop@%regal_c_1st%0', 'emotes', 2750),
(271, 'bow2', 'anim@arena@celeb@podium@no_prop@%regal_a_1st%0', 'emotes', 2750),
(272, 'keyfob', 'anim@mp_player_intmenu@key_fob@%fob_click%0', 'interaktion', 1000),
(273, 'golfswing', 'rcmnigel1d%swing_a_mark%0', 'sport', 3000),
(274, 'eat', 'mp_player_inteat@burger%mp_player_int_eat_burger%0', 'emotes', 3000),
(275, 'reaching', 'move_m@intimidation@cop@unarmed%idle%0', 'kampfanimation', 32),
(276, 'wait', 'random@shop_tattoo%_idle_a%0', 'stehend', 32),
(277, 'wait2', 'missbigscore2aig_3%wait_for_van_c%0', 'stehend', 32),
(278, 'wait12', 'rcmjosh1%idle%0', 'stehend', 32),
(279, 'wait13', 'rcmnigel1a%base%0', 'stehend', 32),
(280, 'lapdance2', 'mini@strip_club@private_dance@idle%priv_dance_idle%1', 'tanzen', -1),
(281, 'lapdance3', 'mini@strip_club@private_dance@part2%priv_dance_p2%1', 'tanzen', -1),
(282, 'twerk', 'switch@trevor@mocks_lapdance%001443_01_trvs_28_idle_stripper%1', 'tanzen', -1),
(284, 'fishdance', 'anim@mp_player_intupperfind_the_fish%idle_a%1', 'tanzen', -1),
(285, 'peace', 'mp_player_int_upperpeace_sign%mp_player_int_peace_sign%0', 'emotes', 32),
(286, 'peace2', 'anim@mp_player_intupperpeace%idle_a%0', 'emotes', 32),
(287, 'cpr', 'mini@cpr@char_a@cpr_str%cpr_pumpchest%1', 'interaktion', -1),
(289, 'ledge', 'missfbi1%ledge_loop%1', 'interaktion', -1),
(290, 'airplane', 'missfbi1%ledge_loop%0', 'laufend', 4500),
(291, 'peek', 'random@paparazzi@peek%left_peek_a%1', 'interaktion', -1),
(292, 'cough', 'timetable@gardener@smoking_joint%idle_cough%0', 'emotes', 4500),
(293, 'stretch', 'mini@triathlon%idle_e%1', 'sport', -1),
(294, 'stretch2', 'mini@triathlon%idle_f%1', 'sport', -1),
(295, 'stretch3', 'mini@triathlon%idle_d%1', 'sport', -1),
(296, 'stretch4', 'rcmfanatic1maryann_stretchidle_b%idle_e%1', 'sport', -1),
(297, 'celebrate', 'rcmfanatic1celebrate%celebrate%1', 'emotes', -1),
(298, 'punching', 'rcmextreme2%loop_punching%0', 'kampfanimation', 3500),
(299, 'superhero', 'rcmbarry%base%1', 'stehend', -1),
(300, 'superhero2', 'rcmbarry%base%0', 'laufend', 4500),
(301, 'mindcontrol', 'rcmbarry%mind_control_b_loop%1', 'emotes', -1),
(302, 'mindcontrol2', 'rcmbarry%bar_1_attack_idle_aln%1', 'emotes', -1),
(303, 'clown', 'rcm_barry2%clown_idle_0%1', 'tanzen', -1),
(304, 'clown2', 'rcm_barry2%clown_idle_1%1', 'tanzen', -1),
(305, 'clown3', 'rcm_barry2%clown_idle_2%1', 'tanzen', -1),
(306, 'clown4', 'rcm_barry2%clown_idle_3%0', 'emotes', 32),
(307, 'clown5', 'rcm_barry2%clown_idle_6%1', 'tanzen', -1),
(308, 'tryclothes', 'mp_clothing@female@trousers%try_trousers_neutral_a%1', 'emotes', -1),
(309, 'tryclothes2', 'mp_clothing@female@shirt%try_shirt_positive_a%1', 'emotes', -1),
(310, 'tryclothes3', 'mp_clothing@female@shoes%try_shoes_positive_a%1', 'emotes', -1),
(311, 'nervous2', 'mp_missheist_countrybank@nervous%nervous_idle%0', 'laufend', 4500),
(312, 'nervous', 'amb@world_human_bum_standing@twitchy@idle_a%idle_c%0', 'laufend', 4500),
(313, 'nervous3', 'rcmme_tracey1%nervous_loop%0', 'laufend', 4500),
(314, 'uncuff', 'mp_arresting%a_uncuff%1', 'interaktion', 2000),
(315, 'namaste', 'timetable@amanda@ig_4%ig_4_base%0', 'emotes', 32),
(316, 'dj1', 'anim@amb@nightclub@djs@dixon@%dixn_dance_cntr_open_dix%1', 'interaktion', -1),
(317, 'threaten', 'random@atmrobberygen%b_atm_mugging%1', 'kampfanimation', -1),
(318, 'radio', 'random@arrests%generic_radio_chatter%0', 'interaktion', 1500),
(319, 'pull', 'random@mugging4%struggle_loop_b_thief%1', 'interaktion', 32),
(320, 'bird', 'random@peyote@bird%wakeup%0', 'laufend', 4500),
(321, 'chicken', 'random@peyote@chicken%wakeup%0', 'laufend', 4500),
(322, 'bark', 'random@peyote@dog%wakeup%1', 'kniend', -1),
(323, 'rabbit', 'random@peyote@rabbit%wakeup%1', 'kniend', -1),
(324, 'spiderman', 'missexile3%ex03_train_roof_idle%1', 'kniend', -1),
(325, 'boi', 'special_ped@jane@monologue_5@monologue_5c%brotheradrianhasshown_2%0', 'emotes', 3000),
(326, 'adjust', 'missmic4%michael_tux_fidget%0', 'emotes', 4000),
(327, 'pee', 'misscarsteal2peeing%peeing_loop%0', 'interaktion', 3000),
(328, 'karate', 'anim@mp_player_intcelebrationfemale@karate_chops%karate_chops%1', 'kampfanimation', -1),
(329, 'karate2', 'anim@mp_player_intcelebrationmale@karate_chops%karate_chops%1', 'kampfanimation', -1),
(330, 'cutthroat', 'anim@mp_player_intcelebrationmale@cut_throat%cut_throat%1', 'emotes', -1),
(331, 'cutthroat2', 'anim@mp_player_intcelebrationfemale@cut_throat%cut_throat%1', 'emotes', -1),
(332, 'mindblow', 'anim@mp_player_intcelebrationmale@mind_blown%mind_blown%0', 'emotes', 32),
(333, 'stink', 'anim@mp_player_intcelebrationfemale@stinker%stinker%0', 'emotes', 32),
(334, 'joint', 'amb@world_human_aa_smoke@male@idle_a%idle_a%49', 'interaktion', -1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bank`
--

CREATE TABLE `bank` (
  `id` int(11) NOT NULL,
  `banknumber` varchar(20) NOT NULL DEFAULT 'n/A',
  `bankvalue` int(11) NOT NULL DEFAULT 0,
  `pincode` varchar(4) NOT NULL DEFAULT '0000',
  `ownercharid` int(11) NOT NULL DEFAULT 0,
  `groupid` int(11) NOT NULL DEFAULT 0,
  `banktype` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `animations`
--

INSERT INTO `bank` (`id`, `banknumber`, `bankvalue`, `pincode`, `ownercharid`, `groupid`, `banktype`) VALUES
(1, 'SA3701-100000', 100000, '4301', -1, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bankfile`
--

CREATE TABLE `bankfile` (
  `id` int(11) NOT NULL,
  `bankid` int(11) NOT NULL,
  `bankfrom` varchar(25) NOT NULL,
  `bankto` varchar(25) NOT NULL,
  `banktext` varchar(64) NOT NULL,
  `bankvalue` int(11) NOT NULL DEFAULT 0,
  `banktime` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `banksettings`
--

CREATE TABLE `banksettings` (
  `id` int(11) NOT NULL,
  `banknumber` varchar(13) NOT NULL DEFAULT 'n/A',
  `setting` varchar(25) NOT NULL DEFAULT 'n/A',
  `value` varchar(25) NOT NULL DEFAULT 'n/A',
  `name` varchar(35) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bans`
--

CREATE TABLE `bans` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `banname` varchar(35) NOT NULL,
  `banfrom` varchar(35) NOT NULL,
  `ip` varchar(128) NOT NULL,
  `text` varchar(35) NOT NULL,
  `identifier` varchar(128) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `business`
--

CREATE TABLE `business` (
  `id` int(11) NOT NULL,
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `posz` float NOT NULL,
  `price` int(11) NOT NULL DEFAULT 0,
  `name` varchar(64) NOT NULL DEFAULT 'Business',
  `name2` varchar(64) NOT NULL DEFAULT 'Business',
  `owner` varchar(35) NOT NULL DEFAULT 'n/A',
  `funds` int(11) NOT NULL DEFAULT 0,
  `products` int(4) NOT NULL DEFAULT 2000,
  `multiplier` float NOT NULL DEFAULT 1,
  `cash` int(11) NOT NULL DEFAULT 0,
  `govcash` int(11) NOT NULL DEFAULT 0,
  `prodprice` int(4) NOT NULL DEFAULT 0,
  `blipid` int(4) NOT NULL DEFAULT 1,
  `buyproducts` int(1) NOT NULL DEFAULT 0,
  `selled` int(2) NOT NULL DEFAULT 0,
  `getmoney` int(11) NOT NULL DEFAULT 25000,
  `elec` int(3) NOT NULL DEFAULT 50
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `business`
--

INSERT INTO `business` (`id`, `posx`, `posy`, `posz`, `price`, `name`, `name2`, `owner`, `funds`, `products`, `multiplier`, `cash`, `govcash`, `prodprice`, `blipid`, `buyproducts`, `selled`, `getmoney`, `elec`) VALUES
(1, 83.6053, -1389, 29.4159, 125000, 'Strawberry Kleidungsladen', 'Strawberry Kleidungsladen', 'n/A', 0, 2000, 1, 0, 0, 30, 73, 0, 0, 10000, 51),
(2, -1203.16, -782.073, 17.329, 120000, 'Del Perro Kleidungsladen', 'Del Perro Kleidungsladen', 'n/A', 0, 2000, 1, 0, 0, 30, 73, 0, 0, 10000, 50),
(3, -716.517, -160.725, 36.9918, 130000, 'Rockford Hills Kleidungsladen', 'Rockford Hills Kleidungsladen', 'n/A', 0, 2000, 1, 0, 0, 30, 73, 0, 0, 10000, 50),
(4, -3169.29, 1060.53, 20.8581, 100000, 'Chumash Kleidungsladen', 'Chumash Kleidungsladen', 'n/A', 0, 2000, 1, 0, 0, 30, 73, 0, 0, 10000, 50),
(5, 1168.22, -323.421, 69.2941, 215000, 'Mirror Park 24/7 & Tankstelle', 'Mirror Park 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 51),
(6, -55.3299, -1755.64, 29.4396, 200000, 'Davis 24/7 & Tankstelle', 'Davis 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 51),
(7, 2561.55, 390.335, 108.618, 195000, 'Tataviam 24/7 & Tankstelle', 'Tataviam 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 50),
(8, 373.122, 322.655, 103.498, 135000, 'Vinewood 24/7', 'Vinewood 24/7', 'n/A', 0, 2000, 1, 0, 0, 30, 52, 0, 0, 10000, 51),
(9, -1224.53, -899.787, 12.3846, 125000, 'Vespucci 24/7', 'Vespucci 24/7', 'n/A', 0, 2000, 1, 0, 0, 30, 52, 0, 0, 10000, 50),
(10, -3239.26, 1011.89, 12.2954, 105000, 'Chumash 24/7', 'Chumash 24/7', 'n/A', 0, 2000, 1, 0, 0, 30, 52, 0, 0, 10000, 51),
(11, 538.84, 2673.45, 42.1592, 105000, 'Harmony 24/7', 'Harmony 24/7', 'n/A', 0, 2000, 1, 0, 0, 30, 52, 0, 0, 10000, 50),
(12, 1741.55, 6407.64, 35.0971, 115000, 'Mount Chiliad 24/7 & Tankstelle', 'Mount Chiliad 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 51),
(13, 1142.37, -977.982, 46.3447, 105000, 'Murrieta 24/7', 'Murrieta 24/7', 'n/A', 0, 2000, 1, 0, 0, 30, 52, 0, 0, 10000, 50),
(14, 1971.74, 3744.46, 32.2202, 115000, 'Sandy 24/7 & Tankstelle', 'Sandy 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 50),
(15, -705.047, -917.243, 19.2143, 215000, 'Little Seoul 24/7 & Tankstelle', 'Little Seoul 24/7 & Tankstelle', 'n/A', 0, 2000, 1, 0, 0, 30, 361, 0, 0, 10000, 51),
(16, 233.458, -1873.32, 26.5578, 205000, 'Sprunklager', 'Sprunklager', 'n/A', 0, 2000, 1, 0, 0, 30, 569, 0, 0, 10000, 51),
(17, 815.727, -2147.22, 29.4063, 275000, 'Ammunation Cypress', 'Ammunation Cypress', 'n/A', 0, 2000, 1, 0, 0, 30, 110, 0, 0, 10000, 50),
(18, 840.548, -1021.29, 27.5332, 280000, 'Ammunation La Mesa', 'Ammunation La Mesa', 'n/A', 0, 2000, 1, 0, 0, 30, 110, 0, 0, 10000, 50),
(19, 24.3831, -1113.85, 29.2705, 315000, 'Ammunation Pillbox Hill', 'Ammunation Pillbox Hill', 'n/A', 0, 2000, 1, 0, 0, 30, 110, 0, 0, 10000, 51),
(20, -1316.46, -392.577, 36.5062, 255000, 'Ammunation Morningwood', 'Ammunation Morningwood', 'n/A', 0, 2000, 1, 0, 0, 30, 110, 0, 0, 10000, 50),
(21, -326.172, 6073.26, 31.2305, 215000, 'Ammunation Paleto Bay', 'Ammunation Paleto Bay', 'n/A', 0, 2000, 1, 0, 0, 30, 110, 0, 0, 10000, 50),
(22, 118.694, -161.215, 54.739, 1, 'Luxus Autohaus', 'Luxus Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 51),
(23, 307.209, -1166.84, 29.2922, 3500000, 'Motorrad Autohaus', 'Motorrad Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 522, 0, 0, 10000, 50),
(24, -43.2307, -1108.68, 26.4376, 5850000, 'Sportwagen Autohaus', 'Sportwagen Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 51),
(25, -66.127, 61.2234, 71.9451, 4750000, 'SUV/Sedans Autohaus', 'SUV/Sedans Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(26, -38.7297, -1677.76, 29.4908, 2750000, 'Gebrauchtwagen Autohaus', 'Gebrauchtwagen Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(27, 1254.78, 2700.1, 38.0058, 2250000, 'Musclecar Autohaus', 'Musclecar Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(28, -215.449, 6219.15, 31.4916, 3250000, 'Offroad Autohaus', 'Offroad Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(29, 671.526, -2667.6, 6.08119, 5850000, 'Truck Autohaus', 'Truck Autohaus', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(30, -1139.59, -199.939, 37.96, 275000, 'Fahrradhändler', 'Fahrradhändler', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(31, -761.69, -1314.78, 5.00038, 4550000, 'Bootshändler', 'Bootshändler', 'n/A', 0, 2000, 1, 0, 0, 30, 427, 0, 0, 10000, 50),
(32, -948.595, -2946.54, 13.9451, 8750000, 'Flugzeughändler', 'Flugzeughändler', 'n/A', 0, 2000, 1, 0, 0, 30, 584, 0, 0, 10000, 51),
(33, 201.994, -808.826, 31.0107, 165000, 'Pillbox Garage', 'Pillbox Garage', 'n/A', 0, 2000, 1, 0, 0, 30, 524, 0, 0, 10000, 50),
(34, -910.07, -1383.95, 5.16854, 115000, 'Bootsgarage La Purta', 'Bootsgarage La Purta', 'n/A', 0, 2000, 1, 0, 0, 30, 524, 0, 0, 10000, 50),
(35, -953.483, -3057.67, 13.9451, 135000, 'Fluggarage', 'Fluggarage', 'n/A', 0, 2000, 1, 0, 0, 30, 524, 0, 0, 10000, 51),
(36, 77.3893, 5.79824, 68.6731, 125000, 'Garage Vinewood Mitte', 'Garage Vinewood Mitte', 'n/A', 0, 2000, 1, 0, 0, 30, 524, 0, 0, 10000, 50),
(37, 130.151, -1712.4, 29.2694, 105000, 'David Barber Shop', 'David Barber Shop', 'n/A', 0, 2000, 1, 0, 0, 30, 71, 0, 0, 10000, 50),
(38, -826.337, -184.868, 37.6534, 135000, 'Rockford Hills Kosmetik & Friseurladen', 'Rockford Hills Kosmetik & Friseurladen', 'n/A', 0, 2000, 1, 0, 0, 30, 71, 0, 0, 10000, 50),
(39, 1323.11, -1647.73, 52.1456, 145000, 'Tattooladen El Burro', 'Tattooladen El Burro', 'n/A', 0, 2000, 1, 0, 0, 30, 437, 0, 0, 10000, 50),
(40, 318.43, 178.343, 103.621, 155000, 'Tattooladen Vinewood', 'Tattooladen Vinewood', 'n/A', 0, 2000, 1, 0, 0, 30, 437, 0, 0, 10000, 51),
(41, -630.466, -240.888, 38.161, 295000, 'Juwelier Rockford Hills', 'Juwelier Rockford Hills', 'n/A', 0, 2000, 1, 0, 0, 30, 617, 0, 0, 10000, 50),
(42, -515.013, -2202.1, 6.39402, 825000, 'Möbellager', 'Möbellager', 'n/A', 0, 2000, 1, 0, 0, 30, 557, 0, 0, 10000, 50),
(43, -702.741, -1288.17, 5.10206, 515000, 'Fahrschule La Puerta', 'Fahrschule La Puerta', 'n/A', 0, 2000, 1, 0, 0, 30, 523, 0, 0, 10000, 50),
(44, -562.145, 273.855, 83.0197, 97500, 'Tequi - LA - LA Bar', 'Tequi - LA - LA Bar', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 51),
(45, 131.775, -1298.36, 29.2327, 100000, 'Vanilla Unicorn', 'Vanilla Unicorn', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 50),
(46, 848.913, -114.958, 79.4116, 92500, 'Irish Pub', 'Irish Pub', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 50),
(47, -427.229, 260.499, 83.0381, 95000, 'Split Sides Bar', 'Split Sides Bar', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 50),
(48, 1989.69, 3055.46, 47.2151, 85000, 'Yellow Jack Bar', 'Yellow Jack Bar', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 50),
(49, -1392.54, -588.187, 30.257, 98500, 'Bahama Mamas', 'Bahama Mamas', 'n/A', 0, 2000, 1, 0, 0, 30, 93, 0, 0, 10000, 50),
(50, 2854.04, 1502.15, 24.7244, 500000, 'Kraftwerk Palmer-Taylor', 'Kraftwerk Palmer-Taylor', 'n/A', 0, 2000, 1, 0, 0, 30, 767, 0, 0, 10000, 50),
(51, 528.381, -1603.14, 29.2993, 500000, 'Kraftwerk Rancho', 'Kraftwerk Rancho', 'n/A', 0, 2000, 1, 0, 0, 30, 767, 0, 0, 10000, 51);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `busroutes`
--

CREATE TABLE `busroutes` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `routes` longtext NOT NULL,
  `advert` longtext NOT NULL,
  `skill` int(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `busroutes`
--

INSERT INTO `busroutes` (`id`, `name`, `routes`, `advert`, `skill`) VALUES
(3, 'Vinewood', '315.84985,-86.30158,68.62251,Hawick|-115.34068,79.0754,70.43149,Vinewood West|-492.19443,135.18025,62.89999,Vinewood West 2|-966.04486,89.22061,51.219296,Rockford Hills|-1058.407,186.87552,59.45451,Rockford Hills 2|-914.3757,229.5445,69.43296,Rockford Hills 3|-455.4553,236.44347,82.2741,Vinewood West 3|-138.24649,244.29785,94.659454,Vinewood West 4|103.273224,221.02217,107.17998,Vinewood Mitte|414.59836,107.34887,100.20601,Vinewood Mitte 2|646.3812,21.113667,85.41307,Vinewood Mitte 3|500.92328,-259.53342,46.813118,Alta|426.50302,-660.629,28.050413,Busdepot', '317.28168,-83.70929,69.38953,Hawick|-113.93099,81.284134,71.22948,Vinewood West|-491.50137,137.49324,63.702812,Vinewood West 2|-966.25366,92.04445,51.982296,Rockford Hills|-1055.7675,187.88255,60.20622,Rockford Hills 2|-916.0047,226.8794,70.071625,Rockford Hills 3|-455.94208,234.27948,83.024376,Vinewood West 3|-138.36066,241.79338,95.38104,Vinewood West 4|101.972786,219.00407,107.881256,Vinewood Mitte|412.8673,105.50263,100.970474,Vinewood Mitte 2|645.3049,18.5634,86.18371,Vinewood Mitte 3|499.21664,-257.27628,47.609978,Alta', 1),
(4, 'South Los-Santos', '-29.842764,-1355.3297,28.492867,Strawberry|-211.58348,-1468.2617,30.64306,Chamberlain Hills|-0.25512818,-1650.3003,28.50414,Mosleys-Autohaus|148.06549,-1770.5306,28.237793,Davis|366.0409,-1776.6487,28.453648,Rancho|301.68546,-1684.9795,28.607183,Rancho 2|280.8432,-1551.1064,28.38645,Davis 2|399.38287,-1595.7639,28.554081,Rancho 3|263.33844,-1449.9543,28.554886,Davis 3|83.9718,-1458.9045,28.557999,Strawberry 2|-14.839349,-1576.0632,28.531982,Strawberry 3|426.6106,-661.3814,28.110125,Busdepot', '-29.659899,-1352.9633,29.320189,Strawberry|-213.3782,-1470.2684,31.397663,Chamberlain Hills|-1.8729258,-1651.6809,29.321909,Mosleys-Autohaus|146.55241,-1772.2012,29.033379,Davis|367.84607,-1778.5961,29.162386,Rancho|303.17932,-1683.092,29.337646,Rancho 2|281.71936,-1553.2361,29.157007,Davis 2|397.7246,-1597.8104,29.291584,Rancho 3|265.19904,-1448.4166,29.349018,Davis 3|82.06677,-1456.948,29.291134,Strawberry 2|-16.562075,-1574.6852,29.291996,Strawberry 3', 1),
(5, 'Mirror Park', '1248.7096,-754.3618,60.42775,Utopia Gardens|1357.1473,-585.0237,73.5903,Nikola Place|1264.1206,-392.558,68.34031,East Mirror Drive|1044.839,-414.9569,65.67345,East Mirror Drive 2|903.1505,-592.55804,56.620274,West Mirror Drive|1024.7933,-603.19196,58.077026,Mirror Place|1081.6587,-521.4635,62.094902,Mirror Park|1203.3153,-442.17444,66.234985,Mirror Park Boulevard|1112.775,-230.32727,68.42343,East Vinewood|426.23053,-660.5483,28.047749,Busdepot', '1250.3451,-756.5323,61.21021,Utopia Gardens|1355.1694,-586.6798,74.36782,Nikola Place|1265.9585,-390.69608,69.09253,East Mirror Drive|1043.8182,-413.26495,66.44057,East Mirror Drive 2|901.7409,-594.4324,57.401546,West Mirror Drive|1027.1683,-604.33044,58.833454,Mirror Place|1081.8308,-524.6574,62.755234,Mirror Park|1205.6709,-443.197,66.97667,Mirror Park Boulevard|1114.2366,-227.69807,69.1365,East Vinewood', 1),
(6, 'Little Seoul', '-523.3482,-267.24518,34.64972,Rathaus|-721.9571,-165.57133,36.312492,Portola Drive|-931.4281,-126.51489,36.92893,Boulevard Del Perro|-1239.9368,-300.25693,36.75251,Boulevard Del Perro 2|-1270.1141,-422.74997,33.342354,Richards Majestic|-1244.8971,-588.6499,26.589226,North Rockford Drive|-1118.031,-737.2644,19.369532,Vespucci KanÃ¤le|-1166.7002,-823.55164,13.536093,San Andreas Avenue|-1134.5913,-909.6664,3.0674493,Vespucci Boulevard|-1017.4965,-1108.1221,1.4343128,Prosperity Street|-890.0827,-1200.4913,4.1951437,Palomino Avenue|-842.28406,-1288.9717,4.344233,La Puerta', '-523.01184,-263.63962,35.471695,Rathaus|-719.81287,-164.67957,37.03168,Portola Drive|-931.1807,-120.332954,37.77012,Boulevard Del Perro|-1240.6146,-298.04947,37.510754,Boulevard Del Perro 2|-1272.1528,-424.2338,34.088905,Richards Majestic|-1246.6344,-590.11383,27.321682,North Rockford Drive|-1119.6683,-739.2005,20.100403,Vespucci KanÃ¤le|-1167.6892,-821.45825,14.329721,San Andreas Avenue|-1137.0411,-910.34595,3.9147837,Vespucci Boulevard|-1019.97546,-1109.2808,2.160689,Prosperity Street|-888.08136,-1203.0919,4.968436,Palomino Avenue|-843.841,-1291.3118,5.0001783,La Puerta', 1),
(7, 'Airport', '-1028.5171,-2725.4998,13.045939,Airport|-754.9747,-2446.2456,13.74434,Exceptionalists Way|-947.2154,-2159.618,8.1846,Greenwich Parkway|-704.8315,-1527.1587,12.054735,La Puerta|-567.86206,-1233.1036,14.442877,Little Seoul|-528.0332,-1025.8,22.10145,Calais Avenue|-250.0703,-882.8092,29.937841,Pillbox Hill|168.46277,-1033.9202,28.592745,WÃ¼rfelpark|331.50305,-767.98236,28.538435,Textilbezirk|412.48352,-581.57806,27.979258,Busdepot|948.4069,133.58237,80.19846,Vinewood Casino|426.4814,-660.81024,28.066088,Busdepot', '-1030.0076,-2728.1099,13.756636,Airport|-752.91425,-2447.8398,14.483702,Exceptionalists Way|-945.8208,-2158.2283,8.934807,Greenwich Parkway|-702.7884,-1528.1587,12.781429,La Puerta|-565.9455,-1234.7506,15.171802,Little Seoul|-525.22705,-1025.6819,22.847897,Calais Avenue|-248.52702,-887.70135,30.564716,Pillbox Hill|167.27419,-1036.3967,29.321611,WÃ¼rfelpark|334.08813,-768.54736,29.273413,Textilbezirk|414.28757,-583.43555,28.725954,Busdepot|946.1643,134.60951,80.830635,Vinewood Casino', 2),
(8, 'Vinewood Hills', '818.1506,559.6778,125.12616,Vinewood Bowl|390.84705,438.95807,142.56409,North Conker Avenue|140.82756,491.48364,144.69118,Wild Oats Drive|-95.09646,512.2782,143.22852,Wild Oats Drive 2|-373.92786,513.6456,119.10956,Didion Drive|-496.47995,582.2045,120.82642,Didion Drive 2|-191.87062,601.1136,195.01689,Kimbie Hill Drive|21.509264,629.2001,206.66023,Lake Vinewood Drive|257.66342,810.458,194.50406,Lake Vinewood Drive 2|103.14227,1003.745,213.55516,Vinewood Hills|-84.40156,905.16455,234.94304,Vinewood Hills Kreisel|-496.5585,905.3349,240.75154,Marlowe Drive|-829.17535,820.27264,198.29265,North Sheldon Avenue|-1207.8978,693.968,146.22195,North Sheldon Avenue 2|-1427.9325,490.12863,111.507744,Mad Wayne Thunder Drive|-1255.3611,462.8022,93.20637,Mad Wayne Thunder Drive 2|-1056.9696,450.36288,74.16189,Cockingend Drive|426.4814,-660.81024,28.066088,Busdepot', '816.4708,557.714,125.91516,Vinewood Bowl|391.03983,441.59055,143.40582,North Conker Avenue|141.8362,493.5076,145.16283,Wild Oats Drive|-93.63249,514.1869,143.80591,Wild Oats Drive 2|-372.72183,515.82007,119.94306,Didion Drive|-494.6346,583.82324,121.90813,Didion Drive 2|-192.02295,599.3472,195.5484,Kimbie Hill Drive|21.566444,626.77844,207.3891,Lake Vinewood Drive|258.00146,807.89795,195.43117,Lake Vinewood Drive 2|104.773544,1005.3754,214.15114,Vinewood Hills|-82.63461,906.26025,235.68013,Vinewood Hills Kreisel|-496.19333,907.4091,241.37161,Marlowe Drive|-830.0512,822.87646,198.9818,North Sheldon Avenue|-1209.495,695.40454,146.8616,North Sheldon Avenue 2|-1431.0725,489.51614,112.339066,Mad Wayne Thunder Drive|-1255.6434,460.47772,93.981346,Mad Wayne Thunder Drive 2|-1055.7245,448.3623,74.80438,Cockingend Drive', 3),
(9, 'Sandy Shores', '1427.6534,-1543.6304,58.98169,El Burro Heights|1959.0564,-932.5627,78.48443,Palomino Hochland|2431.6067,-688.3978,62.11601,Sustancia Road|2594.866,343.5731,107.76186,Tataviam Bergkette|2547.108,1614.1895,29.19596,Senora Way|2555.3171,2688.8308,40.50692,Davis Quartz|2116.8962,3048.6562,44.86763,Grand Senora Wüste|1840.4825,3650.3374,33.61267,Alhambra Drive|1904.6462,3817.1667,31.714483,Niland Avenue|1597.3489,3735.2412,34.001476,Cholla Springs Avenue|1778.7189,3355.96,39.711468,Sandy Shores Airport|1216.9465,2687.583,36.999664,Route 68|688.68036,2704.7761,39.86872,Harmony|426.45773,-661.0147,28.079906,Busdepot', '1429.2277,-1545.124,59.59424,El Burro Heights|1961.422,-933.13196,79.07077,Palomino Hochland|2432.6643,-690.1491,62.71632,Sustancia Road|2596.7185,343.21402,108.37432,Tataviam Bergkette|2549.12,1614.3813,29.800467,Senora Way|2557.3528,2689.6287,41.06906,Davis Quartz|2117.197,3050.5103,45.497322,Grand Senora Wüste|1841.6306,3648.9255,34.248833,Alhambra Drive|1906.4662,3817.6436,32.338165,Niland Avenue|1595.2849,3737.0923,34.626087,Cholla Springs Avenue|1777.0465,3354.5244,40.36242,Sandy Shores Airport|1217.245,2689.3213,37.546535,Route 68|688.4585,2706.7458,40.481354,Harmony', 4),
(10, 'Paleto Bay', '1162.4943,401.61755,90.449196,Vinewood Hills|1552.7007,873.9853,76.802086,Route 13 LS Freeway|2674.2969,3136.0203,50.32208,Grand Senora Wüste|2912.032,4161.897,50.093544,San Chianski Bergkette|2596.9841,5316.1445,43.93607,Mount Gordo|1645.6534,6417.9834,27.895975,Mount Chiliad Tankstelle|1420.9644,6592.3975,12.208105,Procopio Beach|91.86037,6480.2153,30.750664,Paleto Bay|-214.85167,6173.473,30.573872,Paleto Bay European Auto|-611.3118,5641.7227,38.026722,Paleto Forest|-960.1088,5422.263,38.28263,Paleto Bay Route 1|-1527.154,4995.122,61.847412,Chiliad Mountain|-2283.271,4239.937,42.253773,North Chumash|-2508.7363,3592.4985,13.875284,Great Ocean Highway|-2741.993,2283.9644,19.088875,Lago Zancudo|-3059.3535,1725.0208,35.589394,Tongva Hills|-3093.4639,1361.9017,19.442932,Chumash|-3135.2974,1049.4149,19.3251,Chumash Route 1|-3032.4468,596.6837,7.0810285,Ineseno Road|-3065.9368,225.13493,14.915039,Banham Canyon|-2166.9658,-362.37064,12.336723,Pacific Bluffs|-1785.3695,-540.4508,32.57853,Bay City Incline|-1537.9825,-683.6997,28.132158,Bay City Avenue|426.61984,-661.4244,28.113585,Busdepot', '1164.2976,399.85364,91.190765,Vinewood Hills|1554.6968,873.0255,77.455666,Route 13 LS Freeway|2676.4526,3134.974,50.969997,Grand Senora Wüste|2914.8188,4162.401,50.90055,San Chianski Bergkette|2598.963,5317.979,44.64325,Mount Gordo|1646.3848,6420.1704,28.445206,Mount Chiliad Tankstelle|1421.5059,6594.8345,12.617334,Procopio Beach|90.54971,6482.0933,31.414103,Paleto Bay|-215.6076,6177.017,31.180134,Paleto Bay European Auto|-613.13873,5643.4653,38.554783,Paleto Forest|-961.34955,5425.05,38.99832,Paleto Bay Route 1|-1529.794,4996.533,62.240646,Chiliad Mountain|-2285.7307,4241.147,42.683933,North Chumash|-2511.2207,3593.0378,14.513128,Great Ocean Highway|-2744.4617,2285.2322,19.773415,Lago Zancudo|-3062.2292,1724.292,36.105988,Tongva Hills|-3095.8105,1362.4857,19.990288,Chumash|-3137.2065,1050.4454,20.12982,Chumash Route 1|-3034.3457,595.7958,7.809395,Ineseno Road|-3066.1426,222.5303,15.636562,Banham Canyon|-2167.5002,-364.44025,13.093839,Pacific Bluffs|-1786.0199,-542.4878,33.423664,Bay City Incline|-1539.4708,-685.5324,28.87988,Bay City Avenue', 5);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `cctvs`
--

CREATE TABLE `cctvs` (
  `id` int(11) NOT NULL,
  `who` varchar(35) NOT NULL,
  `from` varchar(35) NOT NULL,
  `position` varchar(64) NOT NULL,
  `heading` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `characters`
--

CREATE TABLE `characters` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `name` varchar(35) NOT NULL,
  `json` longtext NOT NULL,
  `cash` int(11) NOT NULL DEFAULT 5000,
  `birth` varchar(10) NOT NULL DEFAULT '01.01.2000',
  `bank` int(11) NOT NULL DEFAULT 0,
  `size` varchar(10) NOT NULL DEFAULT 'n/A',
  `eyecolor` varchar(10) NOT NULL DEFAULT 'n/A',
  `job` int(2) NOT NULL DEFAULT 0,
  `minijob` int(2) NOT NULL DEFAULT 0,
  `lastonline` int(11) NOT NULL DEFAULT 1609462861,
  `licenses` varchar(125) NOT NULL DEFAULT '0|0|0|0|0|0|0',
  `education` varchar(64) NOT NULL DEFAULT 'n/A',
  `origin` varchar(64) NOT NULL DEFAULT 'n/A',
  `skills` varchar(64) NOT NULL DEFAULT 'n/A',
  `appearance` varchar(64) NOT NULL DEFAULT 'n/A',
  `gender` int(1) NOT NULL DEFAULT 1,
  `faction` int(2) NOT NULL DEFAULT 0,
  `rang` int(2) NOT NULL DEFAULT 0,
  `faction_dutytime` int(4) NOT NULL DEFAULT 0,
  `faction_since` int(11) NOT NULL DEFAULT 1609462861,
  `last_spawn` varchar(35) NOT NULL DEFAULT 'Los-Santos',
  `ucp_privat` int(1) NOT NULL DEFAULT 0,
  `ck` int(11) NOT NULL DEFAULT 0,
  `mygroup` int(11) NOT NULL DEFAULT -1,
  `closed` int(1) NOT NULL DEFAULT 0,
  `tutorialstep` int(1) NOT NULL DEFAULT 0,
  `health` int(3) NOT NULL DEFAULT 100,
  `armor` int(2) NOT NULL DEFAULT 1,
  `armorcolor` int(2) NOT NULL DEFAULT 0,
  `thirst` int(3) NOT NULL DEFAULT 100,
  `hunger` int(3) NOT NULL DEFAULT 100,
  `screen` varchar(128) NOT NULL DEFAULT 'https://i.imgur.com/JjpH0qO.jpg',
  `lastpos` varchar(64) NOT NULL DEFAULT '0|0|0|0|0',
  `items` longtext NOT NULL,
  `inhouse` int(11) NOT NULL DEFAULT -1,
  `defaultbank` varchar(20) NOT NULL DEFAULT 'n/A',
  `online` int(1) NOT NULL DEFAULT 0,
  `truckerskill` int(4) NOT NULL DEFAULT 45,
  `nextpayday` int(6) NOT NULL DEFAULT 0,
  `payday_points` int(2) NOT NULL DEFAULT 0,
  `sellprods` int(4) NOT NULL DEFAULT 0,
  `abusemoney` int(11) NOT NULL DEFAULT 0,
  `lastsmartphone` varchar(10) NOT NULL DEFAULT '',
  `thiefskill` int(4) NOT NULL DEFAULT 25,
  `fishingskill` int(4) NOT NULL DEFAULT 35,
  `busskill` int(4) NOT NULL DEFAULT 35,
  `farmingskill` int(4) NOT NULL DEFAULT 25,
  `animations` longtext NOT NULL,
  `walkingstyle` varchar(35) NOT NULL DEFAULT '',
  `clothing` varchar(20) NOT NULL DEFAULT '1,1,1,1,1,1,1,1',
  `factionduty` tinyint(1) NOT NULL,
  `sapoints` int(2) NOT NULL DEFAULT 0,
  `swat` int(1) NOT NULL DEFAULT 0,
  `arrested` int(4) NOT NULL DEFAULT 0,
  `cell` int(1) NOT NULL DEFAULT 0,
  `dutyjson` longtext NOT NULL,
  `death` tinyint(1) NOT NULL DEFAULT 0,
  `disease` int(1) NOT NULL,
  `craftingskill` int(2) NOT NULL DEFAULT 25,
  `robcooldown` int(11) NOT NULL DEFAULT 0,
  `adcount` int(5) NOT NULL DEFAULT 0,
  `guessvalue` int(5) NOT NULL DEFAULT 0,
  `jobless` int(1) NOT NULL DEFAULT 0,
  `friends` varchar(500) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `characters`
--

INSERT INTO `characters` (`id`, `userid`, `name`, `json`, `cash`, `birth`, `bank`, `size`, `eyecolor`, `job`, `minijob`, `lastonline`, `licenses`, `education`, `origin`, `skills`, `appearance`, `gender`, `faction`, `rang`, `faction_dutytime`, `faction_since`, `last_spawn`, `ucp_privat`, `ck`, `mygroup`, `closed`, `tutorialstep`, `health`, `armor`, `thirst`, `hunger`, `screen`, `lastpos`, `items`, `inhouse`, `defaultbank`, `online`, `truckerskill`, `nextpayday`, `payday_points`, `sellprods`, `abusemoney`, `lastsmartphone`, `thiefskill`, `fishingskill`, `busskill`, `farmingskill`, `animations`, `walkingstyle`, `clothing`, `factionduty`, `sapoints`, `swat`, `arrested`, `cell`, `dutyjson`, `death`, `disease`, `craftingskill`, `robcooldown`, `adcount`, `guessvalue`, `jobless`, `friends`) VALUES
(1, 1, 'Test Char', '{\"birth\":\"22.10.1991\",\"origin\":\"Los Santos\",\"hair\":[1,16,0],\"beard\":[255,0],\"blendData\":[12,0,0,0,0,0],\"faceFeatures\":[0.9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothing\":[15,15,14,34,15,0,255,255,0,255,255,255,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"headOverlays\":[23,14,255,-1,255,-1,-1,-1,255,-1,-1,-1],\"headOverlaysColors\":[0,0,0,0,0,0,0,0,0,0,0,0],\"eyeColor\":0,\"gender\":true}', 1104983, '12.12.1980', 0, '01m - 20cm', 'Blau', 1, 0, 1682942061, '0|0|1|0|0|0|0', 'Test', 'Los Santos', 'High School Abschluss', 'Test', 1, 0, 0, 0, 1682115269, 'Los-Santos', 1, 1634304377, 19, 0, 4, 100, 7, 0, 0, 'https://i.imgur.com/RdI0oWK.jpg', '-541,6512|-210,75754|37,649796|-153,134|0', '[]', -1, 'SA3701-459250', 1, 225, 0, 20, 144, 0, '0189771044', 32, 47, 36, 150, '[\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\"]', 'move_m@multiplayer', '1,1,1,1,1,1,1,1', 1, 2, 0, 0, 0, '{\"clothing\":[452,0,121,0,179,0,56,149,0,0,0,0,155,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}', 0, 2, 27, 1672071991, 0, 375, 0, '');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `coupons`
--

CREATE TABLE `coupons` (
  `id` int(11) NOT NULL,
  `coupon` varchar(8) NOT NULL,
  `coupontext` varchar(35) NOT NULL,
  `usages` int(2) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `documents`
--

CREATE TABLE `documents` (
  `id` int(11) NOT NULL,
  `title` varchar(35) NOT NULL,
  `link` varchar(250) NOT NULL,
  `owner` varchar(35) NOT NULL,
  `creator` varchar(35) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `doors`
--

CREATE TABLE `doors` (
  `doorsid` int(11) NOT NULL,
  `id` int(11) NOT NULL DEFAULT 0,
  `hash` varchar(64) NOT NULL DEFAULT 'n/A',
  `posx` float NOT NULL DEFAULT 0,
  `posy` float NOT NULL DEFAULT 0,
  `posz` float NOT NULL DEFAULT 0,
  `dimension` int(11) NOT NULL DEFAULT 0,
  `toogle` tinyint(1) NOT NULL DEFAULT 0,
  `props` varchar(35) NOT NULL,
  `save` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `doors`
--

INSERT INTO `doors` (`doorsid`, `id`, `hash`, `posx`, `posy`, `posz`, `dimension`, `toogle`, `props`, `save`) VALUES
(1, 1, '2306413732', 461.613, -985.254, 34.1874, 0, 1, 'faction-1', 0),
(2, 2, '2306413732', 461.444, -988.688, 34.1878, 0, 1, 'faction-1', 0),
(3, 3, '2306413732', 455.66, -985.18, 34.1877, 0, 1, 'faction-1', 0),
(4, 4, '2306413732', 455.46, -988.671, 34.1873, 0, 1, 'faction-1', 0),
(5, 5, '2306413732', 449.577, -988.608, 34.1872, 0, 1, 'faction-1', 0),
(6, 6, '2306413732', 449.758, -985.203, 34.1876, 0, 1, 'faction-1', 0),
(7, 7, '165994623', 441.031, -994.775, 30.6893, 0, 1, 'faction-1', 0),
(8, 8, '165994623', 465.409, -988.593, 25.729, 0, 1, 'faction-1', 0),
(9, 9, '2751108264', 486.824, -996.792, 25.729, 0, 1, 'faction-1', 0),
(10, 10, '165994623', 467.576, -992.37, 25.736, 0, 1, 'faction-1', 0),
(11, 11, '2751108264', 438.514, -995.837, 30.6893, 0, 1, 'faction-1', 0),
(12, 12, '165994623', 467.542, -991.321, 30.689, 0, 1, 'faction-1', 0),
(13, 13, '165994623', 467.341, -991.494, 34.2171, 0, 1, 'faction-1', 0),
(14, 14, '165994623', 478.86, -996.644, 34.217, 0, 1, 'faction-1', 0),
(15, 15, '165994623', 479.321, -1000.65, 34.2171, 0, 1, 'faction-1', 0),
(16, 16, '165994623', 479.217, -992.627, 34.2171, 0, 1, 'faction-1', 0),
(17, 17, '165994623', 481.9, -1007.29, 34.2224, 0, 1, 'faction-1', 0),
(18, 18, '2751108264', 471.532, -1002.06, 34.217, 0, 1, 'faction-1', 0),
(19, 19, '2751108264', 435.706, -985.156, 34.1871, 0, 1, 'faction-1', 0),
(20, 20, '2751108264', 441.524, -994.724, 34.1871, 0, 1, 'faction-1', 0),
(21, 21, '165994623', 445.865, -986.892, 34.1872, 0, 1, 'faction-1', 0),
(22, 22, '1356380196', 436.8, -1001.55, 25.7198, 0, 1, 'faction-1', 0),
(23, 23, '1356380196', 430.628, -1000.86, 25.6429, 0, 1, 'faction-1', 0),
(24, 24, '1356380196', 452.887, -1001.48, 25.709, 0, 1, 'faction-1', 0),
(25, 25, '1356380196', 447.22, -998.55, 25.676, 0, 1, 'faction-1', 0),
(26, 26, '4104186511', 458.469, -1015.62, 28.2295, 0, 1, 'faction-1', 0),
(27, 27, '4104186511', 459.148, -1018.62, 28.1369, 0, 1, 'faction-1', 0),
(28, 28, '725274945', 410.336, -1021.42, 29.3733, 0, 1, 'faction-1', 0),
(29, 29, '2691149580', 488.432, -1020.1, 28.0431, 0, 1, 'faction-1', 0),
(30, 30, '2271212864', 467.884, -1014.87, 26.386, 0, 1, 'faction-1', 0),
(31, 31, '2271212864', 469.497, -1014.85, 26.386, 0, 1, 'faction-1', 0),
(32, 32, '4129362982', 434.207, -981.333, 30.7094, 0, 1, 'faction-1', 0),
(33, 33, '1388858739', 434.213, -982.486, 30.7094, 0, 1, 'faction-1', 0),
(34, 34, '4129362982', 440.713, -999.048, 30.7255, 0, 1, 'faction-1', 0),
(35, 35, '4129362982', 443.216, -999.167, 30.7243, 0, 1, 'faction-1', 0),
(36, 36, '1388858739', 442.277, -999.197, 30.7245, 0, 1, 'faction-1', 0),
(37, 37, '1388858739', 439.602, -999.174, 30.7255, 0, 1, 'faction-1', 0),
(38, 38, '3954737168', 463.739, -984.044, 43.692, 0, 1, 'faction-1', 0),
(39, 39, '4104186511', 537.953, -31.469, 70.646, 0, 1, 'faction-1', 0),
(40, 40, '4104186511', 532.038, -34.2741, 70.6302, 0, 1, 'faction-1', 0),
(41, 41, '270965283', 604.961, 5.73707, 76.6282, 0, 1, 'faction-1', 0),
(42, 42, '270965283', 604.888, 5.79286, 82.6281, 0, 1, 'faction-1', 0),
(43, 43, '270965283', 604.834, 5.51146, 90.2504, 0, 1, 'faction-1', 0),
(44, 44, '3751469904', 606.431, 0.285559, 76.6281, 0, 1, 'faction-1', 0),
(45, 45, '3751469904', 605.816, -12.0915, 76.628, 0, 1, 'faction-1', 0),
(46, 46, '3751469904', 610.457, -10.7467, 76.6281, 0, 1, 'faction-1', 0),
(47, 47, '2868942576', 592.377, -25.6077, 76.6281, 0, 1, 'faction-1', 0),
(48, 48, '2868942576', 587.549, -24.7519, 76.6281, 0, 1, 'faction-1', 0),
(49, 49, '2868942576', 581.398, -19.4345, 76.6281, 0, 1, 'faction-1', 0),
(50, 50, '2868942576', 583.667, -19.9344, 76.6281, 0, 1, 'faction-1', 0),
(51, 51, '2868942576', 582.027, -15.9208, 76.6281, 0, 1, 'faction-1', 0),
(52, 52, '2868942576', 582.616, -12.6035, 76.6281, 0, 1, 'faction-1', 0),
(53, 53, '2868942576', 583.186, -9.14448, 76.6281, 0, 1, 'faction-1', 0),
(54, 54, '2868942576', 584.158, -16.4451, 76.6281, 0, 1, 'faction-1', 0),
(55, 55, '2868942576', 583.843, -5.78842, 76.6281, 0, 1, 'faction-1', 0),
(56, 56, '2868942576', 584.903, -13.1371, 76.6281, 0, 1, 'faction-1', 0),
(57, 57, '2868942576', 585.394, -9.75757, 76.6281, 0, 1, 'faction-1', 0),
(58, 58, '2868942576', 585.396, -4.90072, 76.6281, 0, 1, 'faction-1', 0),
(59, 59, '2868942576', 586.167, -1.54025, 76.6281, 0, 1, 'faction-1', 0),
(60, 60, '3751469904', 581.713, 9.54358, 76.628, 0, 1, 'faction-1', 0),
(61, 61, '3751469904', 619.346, -16.4141, 82.6281, 0, 1, 'faction-1', 0),
(62, 62, '2473190209', 625.31, 6.62837, 82.6282, 0, 1, 'faction-1', 0),
(63, 63, '2473190209', 625.695, 7.87277, 82.6282, 0, 1, 'faction-1', 0),
(64, 64, '3079744621', 638.279, 1.29676, 82.7866, 0, 0, 'faction-1', 0),
(65, 65, '3079744621', 638.614, 2.4843, 82.7867, 0, 0, 'faction-1', 0),
(66, 66, '3524227011', -670.687, 328.769, 83.0831, 0, 0, 'faction-1', 0),
(67, 67, '3524227011', -671.806, 328.987, 83.0832, 0, 0, 'faction-2', 0),
(68, 68, '475418095', -683.382, 331.48, 83.0833, 0, 1, 'faction-2', 0),
(69, 69, '475418095', -683.335, 330.026, 78.1225, 0, 1, 'faction-2', 0),
(70, 70, '475418095', -683.394, 329.943, 88.0166, 0, 1, 'faction-2', 0),
(71, 71, '475418095', -686.991, 330.334, 92.7423, 0, 1, 'faction-2', 0),
(72, 72, '1653893025', -671.839, 335.956, 83.0832, 0, 1, 'faction-2', 0),
(73, 73, '1653893025', -678.806, 329.58, 83.0832, 0, 1, 'faction-2', 0),
(74, 74, '3931332547', -654.393, 312.108, 83.0384, 0, 1, 'faction-2', 0),
(75, 75, '3931332547', -699.134, 315.534, 83.0251, 0, 1, 'faction-2', 0),
(76, 76, '270965283', 604.828, 5.32703, 70.628, 0, 1, 'faction-1', 0),
(77, 77, '2330716764', 847.294, -112.412, 79.6793, 0, 1, 'bizz-46', 0),
(78, 78, '90939006', 822.491, -120.037, 80.4258, 0, 1, 'bizz-46', 0),
(79, 79, '4082365898', 830.574, -112.34, 79.7747, 0, 1, 'bizz-46', 0),
(80, 80, '3178925983', 129.155, -1299.11, 29.2327, 0, 1, 'bizz-45', 0),
(81, 81, '3799246327', 114.378, -1296.36, 29.2689, 0, 1, 'bizz-45', 0),
(82, 82, '3668283177', 99.1613, -1292.76, 29.2688, 0, 1, 'bizz-45', 0),
(83, 83, '668467214', 95.0643, -1284.51, 29.2716, 0, 1, 'bizz-45', 0),
(84, 84, '1734378958', -429.395, 261.668, 83.0046, 0, 1, 'bizz-47', 0),
(85, 85, '1734378958', -430.646, 261.857, 83.0046, 0, 1, 'bizz-47', 0),
(86, 86, '1734378958', -419.191, 267.433, 83.1959, 0, 1, 'bizz-47', 0),
(87, 87, '1734378958', -420.405, 267.492, 83.1946, 0, 1, 'bizz-47', 0),
(88, 88, '1734378958', -452.802, 278.522, 83.6237, 0, 1, 'bizz-47', 0),
(89, 89, '993120320', -564.479, 275.71, 83.1107, 0, 1, 'bizz-44', 0),
(90, 90, '3668283177', -559.461, 292.439, 82.1762, 0, 1, 'bizz-44', 0),
(91, 91, '1289778077', -568.167, 281.859, 82.9762, 0, 1, 'bizz-44', 0),
(92, 92, '993120320', -561.868, 294.535, 87.4945, 0, 1, 'bizz-44', 0),
(93, 93, '4007304890', 1990.81, 3053.88, 47.2149, 0, 1, 'bizz-48', 0),
(94, 94, '4189801027', -1388.98, -587.35, 30.2224, 0, 1, 'bizz-49', 0),
(95, 95, '702589573', -1387.91, -586.568, 30.2153, 0, 1, 'bizz-49', 0),
(96, 96, '1542392972', -357.954, -134.182, 38.7962, 0, 1, 'faction-3', 0),
(97, 97, '1157738230', -340.615, -166.827, 39.0154, 0, 1, 'faction-3', 0),
(98, 98, '1142444161', -334.157, -161.433, 44.5877, 0, 1, 'faction-3', 0),
(99, 99, '1542392972', -352.82, -116.526, 38.6978, 0, 1, 'faction-3', 0),
(100, 100, '260701421', -366.107, -106.02, 38.6807, 0, 1, 'faction-3', 0),
(101, 101, '260701421', -372.825, -103.384, 38.6835, 0, 1, 'faction-3', 0),
(102, 102, '260701421', -362.152, -151.93, 38.3206, 0, 1, 'faction-3', 0),
(103, 103, '9006550', -574.805, -200.698, 42.8366, 0, 1, 'faction-4', 0),
(104, 104, '2264746914', -559.678, -187.891, 38.2211, 0, 1, 'faction-4', 0),
(105, 105, '2264746914', -551.886, -183.243, 38.2212, 0, 1, 'faction-4', 0),
(106, 106, '2264746914', -569.577, -217.423, 38.1827, 0, 1, 'faction-4', 0),
(107, 107, '2264746914', -570.621, -218.075, 38.183, 0, 1, 'faction-4', 0),
(108, 108, '2447623261', -557.015, -228.509, 38.3385, 0, 1, 'faction-4', 0),
(109, 109, '2447623261', -556.463, -229.574, 38.33, 0, 1, 'faction-4', 0),
(117, 0, '1289409051', 261.583, 221.962, 106.283, 0, 1, 'faction-4', 0),
(118, 0, '-1246222793', 256.764, 219.992, 106.286, 0, 1, 'faction-4', 0),
(119, 0, '-1148826190', 84.0214, -1390.27, 29.4154, 0, 1, 'bizz-1', 0),
(120, 0, '868499217', 84.7424, -1391.81, 29.3017, 0, 1, 'bizz-1', 0),
(121, 0, '-8873588', 843.214, -1021.83, 27.5701, 0, 1, 'bizz-18', 0),
(122, 0, '97297972', 844.795, -1022.28, 27.6788, 0, 1, 'bizz-18', 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `drugs`
--

CREATE TABLE `drugs` (
  `id` int(11) NOT NULL,
  `owner` int(11) NOT NULL,
  `value` int(5) NOT NULL,
  `time` int(2) NOT NULL,
  `water` int(3) NOT NULL,
  `posx` varchar(25) NOT NULL,
  `posy` varchar(25) NOT NULL,
  `posz` varchar(25) NOT NULL,
  `posa` varchar(25) NOT NULL,
  `vw` int(6) NOT NULL,
  `drugname` varchar(35) NOT NULL DEFAULT 'Marihuana'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `factionbudgets`
--

CREATE TABLE `factionbudgets` (
  `id` int(11) NOT NULL,
  `faction` int(3) NOT NULL,
  `budget` int(7) NOT NULL DEFAULT 100000
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `factionbudgets`
--

INSERT INTO `factionbudgets` (`id`, `faction`, `budget`) VALUES
(1, 1, 100000),
(2, 2, 100000),
(3, 3, 100000),
(4, 4, 100000),
(5, 5, 100000);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `factions`
--

CREATE TABLE `factions` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `tag` varchar(10) NOT NULL,
  `leader` int(11) NOT NULL DEFAULT -1,
  `created` int(11) NOT NULL DEFAULT 1609462861,
  `bankvalue` int(11) NOT NULL DEFAULT 0,
  `members` int(5) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `factions`
--

INSERT INTO `factions` (`id`, `name`, `tag`, `leader`, `created`, `bankvalue`, `members`) VALUES
(1, 'Los Santos Police Department', 'LSPD', -1, 1633690373, 0, 0),
(2, 'Los Santos Rescue Center', 'LSRC', -1, 1633690373, 0, 0),
(3, 'Los Santos Abschleppdienst', 'LSAA', -1, 1633690373, 0, 0),
(4, 'Los Santos Regierung', 'LSR', -1, 1633690373, 0, 0),
(5, 'Los Santos Nachrichtendienst', 'LSN', -1, 1633690373, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `factionsalary`
--

CREATE TABLE `factionsalary` (
  `id` int(11) NOT NULL,
  `factionid` int(3) NOT NULL,
  `rang1` int(5) NOT NULL DEFAULT 0,
  `rang2` int(5) NOT NULL DEFAULT 0,
  `rang3` int(5) NOT NULL DEFAULT 0,
  `rang4` int(5) NOT NULL DEFAULT 0,
  `rang5` int(5) NOT NULL DEFAULT 0,
  `rang6` int(5) NOT NULL DEFAULT 0,
  `rang7` int(5) NOT NULL DEFAULT 0,
  `rang8` int(5) NOT NULL DEFAULT 0,
  `rang9` int(5) NOT NULL DEFAULT 0,
  `rang10` int(5) NOT NULL DEFAULT 0,
  `rang11` int(5) NOT NULL DEFAULT 0,
  `rang12` int(5) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


--
-- Daten für Tabelle `factionsalary`
--

INSERT INTO `factionsalary` (`id`, `factionid`, `rang1`, `rang2`, `rang3`, `rang4`, `rang5`, `rang6`, `rang7`, `rang8`, `rang9`, `rang10`, `rang11`, `rang12`) VALUES
(1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
(5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `factionsrangs`
--

CREATE TABLE `factionsrangs` (
  `id` int(11) NOT NULL,
  `factionid` int(3) NOT NULL,
  `rang1` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang2` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang3` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang4` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang5` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang6` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang7` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang8` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang9` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang10` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang11` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang12` varchar(50) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `factionsrangs`
--

INSERT INTO `factionsrangs` (`id`, `factionid`, `rang1`, `rang2`, `rang3`, `rang4`, `rang5`, `rang6`, `rang7`, `rang8`, `rang9`, `rang10`, `rang11`, `rang12`) VALUES
(1, 1, 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A'),
(2, 2, 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A'),
(3, 3, 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A'),
(4, 4, 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A'),
(5, 5, 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fahndungen`
--

CREATE TABLE `fahndungen` (
  `id` int(11) NOT NULL,
  `text` varchar(575) NOT NULL,
  `status` int(1) NOT NULL DEFAULT 0,
  `creator` varchar(35) NOT NULL DEFAULT 'n/A',
  `editor` varchar(35) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fahndungskommentare`
--

CREATE TABLE `fahndungskommentare` (
  `id` int(11) NOT NULL,
  `fahndungsid` int(11) NOT NULL DEFAULT 0,
  `text` varchar(225) NOT NULL,
  `creator` varchar(35) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `failed_jobs`
--

CREATE TABLE `failed_jobs` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `uuid` varchar(255) NOT NULL,
  `connection` text NOT NULL,
  `queue` text NOT NULL,
  `payload` longtext NOT NULL,
  `exception` longtext NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fire`
--

CREATE TABLE `fire` (
  `id` int(11) NOT NULL,
  `name` varchar(35) NOT NULL,
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `posz` float NOT NULL,
  `posa` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `fire`
--

INSERT INTO `fire` (`id`, `name`, `posx`, `posy`, `posz`, `posa`) VALUES
(1, '24/7 Davis', -48.7636, -1761.83, 29.4317, 0),
(2, '24/7 Davis', -49.8466, -1760.66, 29.4397, 0),
(3, '24/7 Davis', -50.969, -1759.6, 29.4397, 0),
(4, '24/7 Davis', -51.9892, -1758.78, 29.4397, 0),
(5, '24/7 Davis', -53.5306, -1757.4, 29.4397, 0),
(6, '24/7 Davis', -54.6433, -1756.45, 29.4397, 0),
(7, '24/7 Davis', -56.2765, -1755.22, 29.4397, 0),
(8, '24/7 Davis', -58.2033, -1753.73, 29.4397, 0),
(9, '24/7 Davis', -60.1893, -1752.42, 29.2603, 0),
(10, '24/7 Davis', -60.4966, -1750.61, 29.3208, 0),
(11, '24/7 Davis', -59.9671, -1749.33, 29.3184, 0),
(12, '24/7 Davis', -58.7537, -1748.16, 29.3172, 0),
(13, '24/7 Davis', -57.6641, -1747.17, 29.3128, 0),
(14, '24/7 Davis', -56.6448, -1745.7, 29.3156, 0),
(15, '24/7 Davis', -55.2699, -1744.36, 29.3111, 0),
(16, '24/7 Davis', -59.7645, -1753.49, 29.2115, 0),
(17, '24/7 Davis', -52.372, -1759.85, 29.1607, 0),
(18, '24/7 Davis', -50.3681, -1760.17, 29.4391, 0),
(19, '24/7 Davis', -46.7771, -1762.36, 29.4309, 0),
(20, '24/7 Davis', -45.8905, -1761.13, 29.4664, 0),
(21, '24/7 Davis', -44.3943, -1760.64, 29.492, 0),
(22, '24/7 Davis', -42.8314, -1759, 29.4917, 0),
(23, '24/7 Davis', -41.2243, -1755.66, 29.4917, 0),
(24, '24/7 Davis', -57.0266, -1756.45, 29.1062, 0),
(30, 'Fahrzeugunfall La Mesa', 707.797, -582.798, 36.0061, 0),
(31, 'Fahrzeugunfall La Mesa', 708.511, -583.587, 36.7151, 0),
(34, 'Fahrzeugunfall La Mesa', 711.609, -587.77, 35.9451, 0),
(35, 'Fahrzeugunfall La Mesa', 710.517, -588.047, 36.0017, 0),
(36, 'Fahrzeugunfall La Mesa', 709.658, -587.117, 36.0006, 0),
(37, 'Fahrzeugunfall La Mesa', 709.19, -586.259, 36.0012, 0),
(38, 'Fahrzeugunfall La Mesa', 708.5, -582.734, 36.0096, 0),
(39, 'Fahrzeugunfall La Mesa', 709.743, -582.129, 36.0145, 0),
(40, 'Fahrzeugunfall La Mesa', 710.371, -583.506, 36.0157, 0),
(41, 'Fahrzeugunfall La Mesa', 711.576, -584.693, 36.0183, 0),
(42, 'Fahrzeugunfall La Mesa', 712.16, -586.47, 36.015, 0),
(43, 'Fahrzeugunfall La Mesa', 711.608, -587.613, 36.0085, 0),
(44, 'Fahrzeugunfall La Mesa', 710.316, -587.615, 36.0021, 0),
(45, 'Fahrzeugunfall La Mesa', 709.233, -586.325, 36.0013, 0),
(46, 'Fahrzeugunfall La Mesa', 707.384, -585.11, 35.9964, 0),
(47, 'Fahrzeugunfall La Mesa', 707.664, -582.211, 36.007, 0),
(48, 'Fahrzeugunfall La Mesa', 706.022, -584.028, 35.9933, 0),
(49, 'Fahrzeugunfall La Mesa', 705.781, -583.196, 35.9952, 0),
(50, 'Fahrzeugunfall La Mesa', 704.928, -582.175, 35.9941, 0),
(51, 'Fahrzeugunfall La Mesa', 705.558, -580.989, 35.9998, 0),
(52, 'Fahrzeugunfall La Mesa', 706.162, -580.346, 36.0023, 0),
(53, 'Fahrzeugunfall La Mesa', 707.353, -579.677, 35.9867, 0),
(54, 'Fahrzeugunfall La Mesa', 708.059, -580.518, 36.0061, 0),
(55, 'Fahrzeugunfall La Mesa', 709.334, -580.562, 36.0023, 0),
(56, 'Fahrzeugunfall Rockford Hills', -883.422, -389.147, 40.2955, 0),
(57, 'Fahrzeugunfall Rockford Hills', -884.615, -388.551, 39.9083, 0),
(58, 'Fahrzeugunfall Rockford Hills', -885.484, -389.65, 38.7984, 0),
(59, 'Fahrzeugunfall Rockford Hills', -885.111, -389.77, 38.8241, 0),
(60, 'Fahrzeugunfall Rockford Hills', -883.707, -390.372, 38.9046, 0),
(61, 'Fahrzeugunfall Rockford Hills', -882.855, -391.004, 38.9552, 0),
(62, 'Fahrzeugunfall Rockford Hills', -881.299, -390.847, 39.0119, 0),
(63, 'Fahrzeugunfall Rockford Hills', -881.256, -390.263, 39.0163, 0),
(64, 'Fahrzeugunfall Rockford Hills', -880.986, -389.195, 39.007, 0),
(65, 'Fahrzeugunfall Rockford Hills', -882.252, -388.193, 38.9241, 0),
(66, 'Fahrzeugunfall Rockford Hills', -882.454, -387.926, 38.9072, 0),
(67, 'Fahrzeugunfall Rockford Hills', -883.609, -387.127, 38.999, 0),
(68, 'Fahrzeugunfall Rockford Hills', -885.199, -386.928, 38.9815, 0),
(69, 'Fahrzeugunfall Rockford Hills', -886.432, -387.729, 38.9347, 0),
(70, 'Fahrzeugunfall Rockford Hills', -886.721, -388.457, 38.9196, 0),
(71, 'Fahrzeugunfall Rockford Hills', -886.462, -390.261, 38.7708, 0),
(72, 'Fahrzeugunfall Rockford Hills', -885.502, -391.337, 38.8388, 0),
(73, 'Fahrzeugunfall Rockford Hills', -883.119, -392.012, 38.9408, 0),
(74, 'Fahrzeugunfall Rockford Hills', -880.823, -391.252, 39.0279, 0),
(75, 'Fahrzeugunfall Rockford Hills', -879.635, -389.147, 39.0701, 0),
(76, 'Fahrzeugunfall Rockford Hills', -880.974, -387.218, 38.9519, 0),
(77, 'Kleidungsladen - Del Perro', -1207.96, -775.785, 17.3377, 0),
(78, 'Kleidungsladen - Del Perro', -1206.91, -776.914, 17.3349, 0),
(79, 'Kleidungsladen - Del Perro', -1206.69, -777.336, 17.3379, 0),
(80, 'Kleidungsladen - Del Perro', -1205.24, -777.788, 17.3256, 0),
(81, 'Kleidungsladen - Del Perro', -1204.6, -777.699, 17.3323, 0),
(82, 'Kleidungsladen - Del Perro', -1202.7, -777.161, 17.3369, 0),
(83, 'Kleidungsladen - Del Perro', -1202.04, -777.725, 17.3374, 0),
(84, 'Kleidungsladen - Del Perro', -1201.1, -778.902, 17.3377, 0),
(85, 'Kleidungsladen - Del Perro', -1201.99, -779.597, 17.335, 0),
(86, 'Kleidungsladen - Del Perro', -1202.04, -780.685, 17.3333, 0),
(87, 'Kleidungsladen - Del Perro', -1202.52, -782.102, 17.3329, 0),
(88, 'Kleidungsladen - Del Perro', -1202.41, -783.122, 17.3289, 0),
(89, 'Kleidungsladen - Del Perro', -1201.52, -784.534, 17.3286, 0),
(90, 'Kleidungsladen - Del Perro', -1197.97, -778.282, 17.3281, 0),
(91, 'Kleidungsladen - Del Perro', -1198.98, -776.161, 17.324, 0),
(92, 'Kleidungsladen - Del Perro', -1199.7, -771.338, 17.3162, 0),
(93, 'Kleidungsladen - Del Perro', -1197.31, -771.648, 17.3192, 0),
(94, 'Kleidungsladen - Del Perro', -1196.08, -773.062, 17.3226, 0),
(95, 'Kleidungsladen - Del Perro', -1193.9, -774.329, 17.3268, 0),
(96, 'Kleidungsladen - Del Perro', -1191.2, -773.478, 17.3284, 0),
(97, 'Kleidungsladen - Del Perro', -1186.88, -771.512, 17.3306, 0),
(98, 'Kleidungsladen - Del Perro', -1186.42, -768.198, 17.3263, 0),
(99, 'Kleidungsladen - Del Perro', -1189.92, -768.175, 17.3225, 0),
(100, 'Kleidungsladen - Del Perro', -1195.6, -769.503, 17.3181, 0),
(101, 'Kleidungsladen - Del Perro', -1199.37, -770.468, 17.3151, 0),
(102, 'Kleidungsladen - Del Perro', -1200.85, -774.574, 17.3195, 0),
(103, 'Fahrzeugunfall - Davis', 149.694, -1987.4, 18.3075, 0),
(104, 'Fahrzeugunfall - Davis', 151.244, -1987.8, 18.3499, 0),
(105, 'Fahrzeugunfall - Davis', 151.421, -1988.76, 18.3667, 0),
(106, 'Fahrzeugunfall - Davis', 149.822, -1989.77, 18.3457, 0),
(107, 'Fahrzeugunfall - Davis', 147.825, -1989.94, 18.3036, 0),
(108, 'Fahrzeugunfall - Davis', 146.942, -1987.81, 18.2051, 0),
(109, 'Fahrzeugunfall - Davis', 148.565, -1986.71, 18.4266, 0),
(110, 'Fahrzeugunfall - Davis', 151.087, -1987.17, 18.3361, 0),
(111, 'Fahrzeugunfall - Davis', 152.871, -1988.67, 18.3637, 0),
(112, 'Fahrzeugunfall - Davis', 153.414, -1991.34, 18.308, 0),
(113, 'Fahrzeugunfall - Davis', 151.403, -1994.39, 18.298, 0),
(114, 'Fahrzeugunfall - Davis', 149.181, -1996.31, 18.3075, 0),
(115, 'Fahrzeugunfall - Davis', 147.014, -1994.82, 18.3644, 0),
(116, 'Fahrzeugunfall - Davis', 144.563, -1992.19, 18.2324, 0),
(117, 'Fahrzeugunfall - Davis', 143.367, -1989.34, 18.2333, 0),
(118, 'Fahrzeugunfall - Davis', 144.863, -1986.78, 18.3075, 0),
(119, 'Fahrzeugunfall - Davis', 146.544, -1984.52, 18.3407, 0),
(120, 'Barber-Shop Davis', 134.523, -1715.51, 29.2791, 0),
(121, 'Barber-Shop Davis', 134.077, -1715.15, 29.2807, 0),
(122, 'Barber-Shop Davis', 133.08, -1714.22, 29.2872, 0),
(123, 'Barber-Shop Davis', 132.794, -1712.78, 29.3035, 0),
(124, 'Barber-Shop Davis', 132.272, -1712.35, 29.2918, 0),
(125, 'Barber-Shop Davis', 130.214, -1711.59, 29.2821, 0),
(126, 'Barber-Shop Davis', 130.941, -1713.39, 29.2645, 0),
(127, 'Barber-Shop Davis', 133.434, -1711.1, 29.2917, 0),
(128, 'Barber-Shop Davis', 134.625, -1710, 29.2916, 0),
(129, 'Barber-Shop Davis', 136.254, -1708.09, 29.2916, 0),
(130, 'Barber-Shop Davis', 137.331, -1706, 29.2916, 0),
(131, 'Barber-Shop Davis', 138.887, -1704.89, 29.2916, 0),
(132, 'Barber-Shop Davis', 140.026, -1705.61, 29.2916, 0),
(133, 'Barber-Shop Davis', 139.518, -1706.48, 29.2916, 0),
(134, 'Barber-Shop Davis', 138.361, -1708.04, 29.3016, 0),
(135, 'Barber-Shop Davis', 137.362, -1709.38, 29.2991, 0),
(136, 'Barber-Shop Davis', 135.886, -1711.47, 29.2933, 0),
(137, 'Juwelier', -633.384, -235.919, 38.0087, 0),
(138, 'Juwelier', -633.058, -237.133, 38.039, 0),
(139, 'Juwelier', -632.246, -237.795, 38.0659, 0),
(140, 'Juwelier', -631.73, -238.903, 38.0972, 0),
(141, 'Juwelier', -630.481, -239.963, 38.1406, 0),
(142, 'Juwelier', -630.262, -237.519, 38.0948, 0),
(143, 'Juwelier', -629.82, -235.658, 38.0571, 0),
(144, 'Juwelier', -630.681, -234.279, 38.0571, 0),
(145, 'Juwelier', -630.095, -233.376, 38.0571, 0),
(146, 'Juwelier', -628.358, -232.83, 38.0571, 0),
(147, 'Juwelier', -626.994, -236.145, 38.0571, 0),
(148, 'Juwelier', -625.37, -236.551, 38.0571, 0),
(149, 'Juwelier', -622.724, -234.603, 38.0571, 0),
(150, 'Juwelier', -619.016, -233.356, 38.0571, 0),
(151, 'Juwelier', -618.189, -229.794, 38.057, 0),
(152, 'Juwelier', -619.908, -227.683, 38.0569, 0),
(153, 'Juwelier', -626.297, -227.245, 38.068, 0),
(154, 'Juwelier', -628.575, -231.1, 38.0581, 0),
(155, 'Taxizentrale', 899.609, -173.171, 74.0069, 0),
(156, 'Taxizentrale', 900.101, -171.582, 74.0771, 0),
(157, 'Taxizentrale', 901.168, -170.09, 74.0756, 0),
(158, 'Taxizentrale', 902.334, -168.382, 74.0817, 0),
(159, 'Taxizentrale', 905.205, -165.3, 74.1015, 0),
(160, 'Taxizentrale', 906.024, -164.497, 74.1081, 0),
(161, 'Taxizentrale', 907.923, -161.067, 74.1311, 0),
(162, 'Taxizentrale', 910.911, -156.45, 74.7172, 0),
(163, 'Taxizentrale', 911.544, -155.334, 75.5629, 0),
(164, 'Taxizentrale', 913.345, -153.019, 75.8385, 0),
(165, 'Taxizentrale', 905.016, -165.848, 74.1, 0),
(166, 'Taxizentrale', 901.441, -169.782, 74.0757, 0),
(167, 'Taxizentrale', 899.713, -172.564, 74.0633, 0),
(168, 'Taxizentrale', 899.321, -176.089, 73.865, 0),
(169, 'Taxizentrale', 898.184, -178.154, 73.7666, 0),
(170, 'Taxizentrale', 897.175, -180.222, 73.7365, 0),
(171, 'Taxizentrale', 895.926, -182.02, 73.7212, 0),
(172, 'Taxizentrale', 898.918, -179.997, 73.8105, 0),
(173, 'Taxizentrale', 906.574, -169.052, 74.1158, 0),
(174, 'Fahrzeugunfall - Richman', -1761.45, 48.9755, 68.0193, 0),
(175, 'Fahrzeugunfall - Richman', -1761.19, 48.3089, 68.005, 0),
(176, 'Fahrzeugunfall - Richman', -1759.67, 47.1659, 67.9771, 0),
(177, 'Fahrzeugunfall - Richman', -1760.12, 49.0868, 68.0125, 0),
(178, 'Fahrzeugunfall - Richman', -1761.93, 49.3849, 68.0286, 0),
(179, 'Fahrzeugunfall - Richman', -1762.91, 48.4805, 67.9924, 0),
(180, 'Fahrzeugunfall - Richman', -1762.49, 46.736, 67.9422, 0),
(181, 'Fahrzeugunfall - Richman', -1761.63, 45.2639, 67.9022, 0),
(182, 'Fahrzeugunfall - Richman', -1760.48, 44.6058, 67.8948, 0),
(183, 'Fahrzeugunfall - Richman', -1759.33, 45.2499, 67.9237, 0),
(184, 'Fahrzeugunfall - Richman', -1758.58, 49.9295, 68.0211, 0),
(185, 'Fahrzeugunfall - Richman', -1763.39, 50.7941, 68.0487, 0),
(186, 'Tattoo-Laden El Burro', 1316.87, -1653.04, 52.1455, 0),
(187, 'Tattoo-Laden El Burro', 1315.64, -1653.82, 52.1455, 0),
(188, 'Tattoo-Laden El Burro', 1319, -1651.61, 52.1456, 0),
(189, 'Tattoo-Laden El Burro', 1320.56, -1650.32, 52.1456, 0),
(190, 'Tattoo-Laden El Burro', 1321.73, -1649.26, 52.1456, 0),
(191, 'Tattoo-Laden El Burro', 1323.3, -1647.86, 52.1456, 0),
(192, 'Tattoo-Laden El Burro', 1325.08, -1646.64, 52.1456, 0),
(193, 'Tattoo-Laden El Burro', 1327.88, -1644.56, 52.1456, 0),
(194, 'Tattoo-Laden El Burro', 1329.65, -1643.18, 52.1456, 0),
(195, 'Tattoo-Laden El Burro', 1322.22, -1650.75, 52.2689, 0),
(196, 'Tattoo-Laden El Burro', 1322.07, -1652.44, 52.2751, 0),
(197, 'Tattoo-Laden El Burro', 1321.53, -1653.9, 52.2753, 0),
(198, 'Tattoo-Laden El Burro', 1322.72, -1652.48, 52.2752, 0),
(199, 'Tattoo-Laden El Burro', 1322.8, -1651.65, 52.2751, 0),
(200, 'Tattoo-Laden El Burro', 1325.58, -1651.94, 52.2756, 0),
(201, 'Tattoo-Laden El Burro', 1326.21, -1652.41, 52.2758, 0),
(202, 'Tattoo-Laden El Burro', 1324.65, -1652.9, 52.2756, 0),
(203, 'Fahrschule', -709.891, -1295.48, 5.10206, 0),
(204, 'Fahrschule', -709.527, -1295.63, 5.10206, 0),
(205, 'Fahrschule', -710.365, -1295.36, 5.10206, 0),
(206, 'Fahrschule', -711.755, -1297.03, 5.10206, 0),
(207, 'Fahrschule', -712.819, -1298.9, 5.10206, 0),
(208, 'Fahrschule', -714.627, -1298.1, 5.10206, 0),
(209, 'Fahrschule', -716.763, -1297.6, 5.10206, 0),
(210, 'Fahrschule', -719.151, -1300.41, 5.10206, 0),
(211, 'Fahrschule', -714.069, -1294.74, 5.10203, 0),
(212, 'Fahrschule', -711.886, -1292.82, 5.10206, 0),
(213, 'Fahrschule', -710.379, -1291.4, 5.10206, 0),
(214, 'Fahrschule', -708.409, -1289, 5.10206, 0),
(215, 'Fahrschule', -709.087, -1300.23, 5.12243, 0),
(216, 'Fahrschule', -706.824, -1301.48, 5.11263, 0),
(217, 'Fahrschule', -706.62, -1304.14, 5.11263, 0),
(218, 'Fahrschule', -709.717, -1304.26, 5.11263, 0),
(219, 'Fahrschule', -713.758, -1304.59, 5.11263, 0),
(220, 'Fahrschule', -714.562, -1303.35, 5.11263, 0),
(221, 'Fahrschule', -707.314, -1297.35, 5.11295, 0),
(222, 'Fahrschule', -706.171, -1295.69, 5.11263, 0),
(223, 'Fahrschule', -704.683, -1294.56, 5.11263, 0),
(224, 'Fahrschule', -702.098, -1297.38, 5.11264, 0),
(225, 'Fahrschule', -702.729, -1300.29, 5.11263, 0),
(226, 'Fahrschule', -697.61, -1300.28, 5.11262, 0),
(227, 'Fahrschule', -704.33, -1306.39, 5.11271, 0),
(228, 'Fahrschule', -705.774, -1307.5, 5.11262, 0),
(229, 'Fahrschule', -704.244, -1309.37, 5.11263, 0),
(230, 'Fahrschule', -701.058, -1309.3, 5.11263, 0),
(231, 'Fahrschule', -698.516, -1310.48, 5.11262, 0),
(232, 'Fahrschule', -699.744, -1312.82, 5.11264, 0),
(233, 'Fleeca Bank - Pillbox', 152.226, -1036.79, 29.3381, 0),
(234, 'Fleeca Bank - Pillbox', 153.025, -1037.77, 29.3342, 0),
(235, 'Fleeca Bank - Pillbox', 154.219, -1037.78, 29.3049, 0),
(236, 'Fleeca Bank - Pillbox', 150.43, -1037.01, 29.3406, 0),
(237, 'Fleeca Bank - Pillbox', 149.629, -1035.99, 29.341, 0),
(238, 'Fleeca Bank - Pillbox', 147.705, -1035.3, 29.3431, 0),
(239, 'Fleeca Bank - Pillbox', 149.1, -1034.96, 29.3419, 0),
(240, 'Fleeca Bank - Pillbox', 152.617, -1039.92, 29.368, 0),
(241, 'Fleeca Bank - Pillbox', 152.998, -1040.44, 29.368, 0),
(242, 'Fleeca Bank - Pillbox', 152.215, -1041.42, 29.372, 0),
(243, 'Fleeca Bank - Pillbox', 150.331, -1040.31, 29.3735, 0),
(244, 'Fleeca Bank - Pillbox', 149.036, -1039.96, 29.374, 0),
(245, 'Fleeca Bank - Pillbox', 146.564, -1038.13, 29.3697, 0),
(246, 'Fleeca Bank - Pillbox', 144.873, -1038.79, 29.3679, 0),
(247, 'Fleeca Bank - Pillbox', 143.803, -1041.81, 29.3679, 0),
(248, 'Fleeca Bank - Pillbox', 144.283, -1043.61, 29.3679, 0),
(249, 'Fleeca Bank - Pillbox', 145.946, -1044.82, 29.3767, 0),
(250, 'Fleeca Bank - Pillbox', 147.55, -1039.45, 29.3725, 0),
(251, 'Fleeca Bank - Pillbox', 150.432, -1040.82, 29.3746, 0),
(252, 'Fleeca Bank - Pillbox', 152.202, -1039.68, 29.368, 0),
(255, 'Möbellager', -510.231, -2206.27, 6.39402, 0),
(256, 'Möbellager', -511.326, -2205.21, 6.45973, 0),
(257, 'Möbellager', -512.829, -2203.81, 6.39405, 0),
(258, 'Möbellager', -515.742, -2202.01, 6.39403, 0),
(259, 'Möbellager', -518.01, -2199.8, 6.39403, 0),
(260, 'Möbellager', -519.962, -2197.7, 6.39403, 0),
(261, 'Möbellager', -522.903, -2195.36, 6.39403, 0),
(262, 'Möbellager', -525.592, -2193.4, 6.39403, 0),
(263, 'Möbellager', -519.105, -2198.45, 6.39403, 0),
(264, 'Möbellager', -511.167, -2204.22, 6.39402, 0),
(265, 'Möbellager', -507.19, -2205.79, 6.39402, 0),
(266, 'Möbellager', -504.69, -2207.74, 6.39402, 0),
(267, 'Möbellager', -503.195, -2209.24, 6.39402, 0),
(268, 'Möbellager', -502.684, -2211.17, 6.39402, 0),
(269, 'Möbellager', -502.432, -2212.44, 6.39402, 0),
(270, 'Möbellager', -501.984, -2208.87, 6.39402, 0),
(271, 'Möbellager', -509.478, -2203.79, 6.39402, 0),
(272, 'Ammunation Cypress', 813.406, -2147.53, 29.5041, 0),
(273, 'Ammunation Cypress', 812.041, -2147.57, 29.5058, 0),
(274, 'Ammunation Cypress', 810.622, -2147.2, 29.4443, 0),
(275, 'Ammunation Cypress', 812.046, -2149.16, 29.6311, 0),
(276, 'Ammunation Cypress', 810.886, -2150.09, 29.6241, 0),
(277, 'Ammunation Cypress', 809.596, -2152.12, 29.619, 0),
(278, 'Ammunation Cypress', 809.37, -2155.61, 29.619, 0),
(279, 'Ammunation Cypress', 811.927, -2157.09, 29.619, 0),
(280, 'Ammunation Cypress', 815.099, -2158.15, 29.619, 0),
(281, 'Ammunation Cypress', 816.999, -2156.56, 29.619, 0),
(282, 'Ammunation Cypress', 818.763, -2155.4, 29.619, 0),
(283, 'Ammunation Cypress', 820.914, -2156.81, 29.619, 0),
(284, 'Ammunation Cypress', 825.084, -2158.94, 29.619, 0),
(285, 'Ammunation Cypress', 824.027, -2156.12, 29.619, 0),
(286, 'Ammunation Cypress', 827.191, -2152.29, 29.619, 0),
(287, 'Ammunation Cypress', 824.686, -2149.32, 29.619, 0),
(288, 'Ammunation Cypress', 822.863, -2150.06, 29.619, 0),
(289, 'Ammunation Cypress', 822.892, -2151.95, 29.6253, 0),
(290, 'Ammunation Cypress', 822.816, -2155.23, 29.6191, 0),
(291, 'Fleeca Bank - Banham', -2968.18, 478.694, 15.4687, 0),
(292, 'Fleeca Bank - Banham', -2967.93, 476.568, 15.4687, 0),
(293, 'Fleeca Bank - Banham', -2967.9, 473.808, 15.4687, 0),
(294, 'Fleeca Bank - Banham', -2968.09, 479.588, 15.4687, 0),
(295, 'Fleeca Bank - Banham', -2967.16, 481.413, 15.4845, 0),
(296, 'Fleeca Bank - Banham', -2966.34, 484.357, 15.6927, 0),
(297, 'Fleeca Bank - Banham', -2966.47, 486.273, 15.5595, 0),
(298, 'Fleeca Bank - Banham', -2964.18, 485.093, 15.6971, 0),
(299, 'Fleeca Bank - Banham', -2963.45, 485.951, 15.697, 0),
(300, 'Fleeca Bank - Banham', -2962.95, 484.191, 15.7031, 0),
(301, 'Fleeca Bank - Banham', -2963.49, 481.061, 15.7052, 0),
(302, 'Fleeca Bank - Banham', -2963.97, 478.662, 15.6974, 0),
(303, 'Fleeca Bank - Banham', -2961.73, 477.807, 15.6969, 0),
(304, 'Fleeca Bank - Banham', -2959.4, 477.418, 15.6969, 0),
(305, 'Fleeca Bank - Banham', -2957.44, 479.888, 15.6981, 0),
(306, 'Fleeca Bank - Banham', -2957.95, 481.599, 15.697, 0),
(307, '24/7 Vespucci', -1223.19, -897.845, 12.4583, 0),
(308, '24/7 Vespucci', -1222.88, -899.152, 12.4426, 0),
(309, '24/7 Vespucci', -1224.03, -899.446, 12.4047, 0),
(310, '24/7 Vespucci', -1225.73, -899.828, 12.3498, 0),
(311, '24/7 Vespucci', -1227.25, -901.293, 12.2817, 0),
(312, '24/7 Vespucci', -1228.96, -902.734, 12.2083, 0),
(313, '24/7 Vespucci', -1231.28, -904.388, 12.1135, 0),
(314, '24/7 Vespucci', -1223.65, -903.39, 12.3264, 0),
(315, '24/7 Vespucci', -1226.3, -905.505, 12.3264, 0),
(316, '24/7 Vespucci', -1225.83, -907.212, 12.3264, 0),
(317, '24/7 Vespucci', -1223.25, -906.668, 12.3264, 0),
(318, '24/7 Vespucci', -1223.99, -911.609, 12.3264, 0),
(319, '24/7 Vespucci', -1221.37, -911.828, 12.3264, 0),
(320, '24/7 Vespucci', -1222.06, -911.942, 12.3264, 0),
(321, 'Fahrzeugunfall - Vinewood Hills', 704.698, 28.9755, 84.2051, 0),
(322, 'Fahrzeugunfall - Vinewood Hills', 703.417, 29.7743, 84.1977, 0),
(323, 'Fahrzeugunfall - Vinewood Hills', 702.01, 30.239, 84.1971, 0),
(324, 'Fahrzeugunfall - Vinewood Hills', 702.486, 28.1128, 84.1941, 0),
(325, 'Fahrzeugunfall - Vinewood Hills', 704.191, 28.669, 84.1966, 0),
(326, 'Fahrzeugunfall - Vinewood Hills', 704.675, 30.8056, 84.2027, 0),
(327, 'Fahrzeugunfall - Vinewood Hills', 703.009, 32.3988, 84.205, 0),
(328, 'Fahrzeugunfall - Vinewood Hills', 701.52, 32.7715, 84.2041, 0),
(329, 'Fahrzeugunfall - Vinewood Hills', 698.935, 31.8183, 84.1752, 0),
(330, 'Fahrzeugunfall - Vinewood Hills', 697.961, 29.7204, 84.1843, 0),
(331, 'Fahrzeugunfall - Vinewood Hills', 699.722, 27.6063, 84.1936, 0),
(332, 'Fahrzeugunfall - Vinewood Hills', 702.989, 27.6313, 84.1936, 0),
(333, 'Fahrzeugunfall - Vinewood Hills', 703.299, 31.3986, 84.2016, 0),
(334, 'Umgefallener Baum - Cypress', 790.421, -2082.63, 29.1803, 47.6476),
(336, 'Umgefallener Baum - Hawick', 675.473, -31.8241, 82.7905, 22.277),
(337, '24/7 Davis', -52.1213, -1754.48, 29.421, 0),
(338, '24/7 Davis', -54.309, -1754, 29.421, 0),
(339, '24/7 Davis', -55.5656, -1750.92, 29.421, 0),
(340, '24/7 Davis', -52.4214, -1749.07, 29.421, 0),
(341, '24/7 Davis', -49.5025, -1750.71, 29.421, 0),
(342, '24/7 Davis', -48.3926, -1752.93, 29.421, 0),
(343, '24/7 Davis', -46.301, -1754.08, 29.421, 0),
(344, '24/7 Davis', -42.8139, -1753.76, 29.4422, 0),
(345, '24/7 Davis', -48.0776, -1756.28, 29.4211, 0),
(346, 'Umgefallener Baum - Rockford Hills', -1003.58, -371.908, 37.7407, -96.8466),
(347, 'Fahrzeugunfall - Little Seoul', -556.834, -1201.08, 17.8878, 0),
(348, 'Fahrzeugunfall - Little Seoul', -558.448, -1200.86, 17.8942, 0),
(349, 'Fahrzeugunfall - Little Seoul', -558.284, -1201.73, 17.8433, 0),
(350, 'Fahrzeugunfall - Little Seoul', -557.317, -1202.96, 17.7751, 0),
(351, 'Fahrzeugunfall - Little Seoul', -555.804, -1203.01, 17.7782, 0),
(352, 'Fahrzeugunfall - Little Seoul', -555.564, -1201.59, 17.8633, 0),
(353, 'Fahrzeugunfall - Little Seoul', -556.132, -1200.1, 17.9457, 0),
(354, 'Fahrzeugunfall - Little Seoul', -556.847, -1197.88, 18.0811, 0),
(355, 'Fahrzeugunfall - Little Seoul', -559.147, -1198.34, 18.0589, 0),
(356, 'Fahrzeugunfall - Little Seoul', -560.496, -1200.7, 17.9264, 0),
(357, 'Fahrzeugunfall - Little Seoul', -559.476, -1202.6, 17.7982, 0),
(358, 'Fahrzeugunfall - Little Seoul', -557.149, -1203.52, 17.7469, 0),
(359, 'Fahrzeugunfall - Little Seoul', -555.684, -1201.54, 17.8653, 0),
(360, 'Fahrzeugunfall - Little Seoul', -554.804, -1199.21, 18.0048, 0),
(361, 'Maze Bank', 228.962, 217.842, 105.55, 0),
(362, 'Maze Bank', 230.146, 216.537, 105.84, 0),
(363, 'Maze Bank', 230.808, 215.467, 106.093, 0),
(364, 'Maze Bank', 231.709, 212.177, 105.568, 0),
(365, 'Maze Bank', 231.799, 213.226, 105.876, 0),
(366, 'Maze Bank', 234.582, 215.318, 106.287, 0),
(367, 'Maze Bank', 236.272, 214.598, 106.287, 0),
(368, 'Maze Bank', 238.446, 212.946, 106.287, 0),
(369, 'Maze Bank', 242.403, 212.237, 106.287, 0),
(370, 'Maze Bank', 243.952, 214.769, 106.287, 0),
(371, 'Maze Bank', 245.846, 217.714, 106.287, 0),
(372, 'Maze Bank', 246.991, 219.502, 106.287, 0),
(373, 'Maze Bank', 250.547, 220.404, 106.289, 0),
(374, 'Maze Bank', 253.548, 218.945, 106.287, 0),
(375, 'Maze Bank', 256.608, 217.461, 106.287, 0),
(376, 'Maze Bank', 257.648, 214.292, 106.287, 0),
(377, 'Maze Bank', 255.976, 213.148, 106.287, 0),
(378, 'Maze Bank', 252.879, 210.179, 106.287, 0),
(379, 'Maze Bank', 245.86, 210.511, 106.278, 0),
(380, 'Maze Bank', 243.446, 211.184, 106.279, 0),
(381, 'Maze Bank', 238.594, 224.572, 106.287, 0),
(382, 'Maze Bank', 235.417, 224.854, 106.287, 0),
(383, 'Maze Bank', 234.837, 221.713, 106.287, 0),
(384, 'Maze Bank', 235.373, 219.266, 106.287, 0),
(385, 'Maze Bank', 262.913, 214.989, 106.283, 0),
(386, 'Maze Bank', 261.883, 213.652, 106.283, 0),
(387, 'Maze Bank', 261.299, 210.19, 106.283, 0),
(388, 'Maze Bank', 261.604, 206.856, 106.283, 0),
(389, 'Maze Bank', 260.665, 206.176, 106.283, 0),
(390, 'Maze Bank', 258.674, 206.21, 106.283, 0),
(391, 'Umgefallener Baum - Chamber. Hills', -184.559, -1492.5, 32.1367, -109.916),
(392, 'Spandex Spedition', 156.23, -3296.96, 7.03039, 0),
(393, 'Spandex Spedition', 155.923, -3295.89, 7.03037, 0),
(394, 'Spandex Spedition', 155.845, -3294.01, 7.03039, 0),
(395, 'Spandex Spedition', 155.438, -3290.97, 7.03039, 0),
(396, 'Spandex Spedition', 155.725, -3290.01, 7.03043, 0),
(397, 'Spandex Spedition', 158.379, -3289.86, 7.03077, 0),
(398, 'Spandex Spedition', 157.392, -3286.64, 7.03069, 0),
(399, 'Spandex Spedition', 159.552, -3287.77, 5.78705, 0),
(400, 'Spandex Spedition', 159.775, -3290.79, 5.99878, 0),
(401, 'Spandex Spedition', 159.534, -3294.82, 6.00535, 0),
(402, 'Spandex Spedition', 162.065, -3296.08, 5.95752, 0),
(403, 'Spandex Spedition', 163.781, -3293.05, 5.92793, 0),
(404, 'Spandex Spedition', 165.751, -3290.31, 5.88964, 0),
(405, 'Spandex Spedition', 163.723, -3287.38, 5.92678, 0),
(406, 'Spandex Spedition', 159.238, -3286.66, 6.00848, 0),
(407, 'Spandex Spedition', 159.086, -3293.07, 6.01376, 0),
(408, 'Spandex Spedition', 162.782, -3296.67, 5.94549, 0),
(409, 'Umgefallener Baum - Richman', -1445.72, 133.778, 52.8052, -152.194),
(410, 'Autounfall - Vinewood Mitte', 679.01, 207.323, 93.2095, 0),
(411, 'Autounfall - Vinewood Mitte', 679.609, 208.199, 93.1779, 0),
(412, 'Autounfall - Vinewood Mitte', 679.677, 209.096, 93.2056, 0),
(413, 'Autounfall - Vinewood Mitte', 678.068, 208.767, 93.3748, 0),
(414, 'Autounfall - Vinewood Mitte', 681.029, 206.859, 92.9437, 0),
(415, 'Autounfall - Vinewood Mitte', 681.511, 205.482, 92.8392, 0),
(416, 'Autounfall - Vinewood Mitte', 684.56, 205.136, 92.4878, 0),
(417, 'Autounfall - Vinewood Mitte', 685.625, 208.018, 92.4678, 0),
(418, 'Autounfall - Vinewood Mitte', 682.891, 210.509, 92.8137, 0),
(419, 'Autounfall - Vinewood Mitte', 680.393, 212.439, 93.111, 0),
(420, 'Autounfall - Vinewood Mitte', 676.603, 210.378, 93.6147, 0),
(421, 'Autounfall - Vinewood Mitte', 674.56, 207.642, 93.727, 0),
(422, 'Autounfall - Vinewood Mitte', 677.351, 204.6, 93.1893, 0),
(423, 'Ammunation Morningwood', -1314.62, -389.193, 36.5871, 0),
(424, 'Ammunation Morningwood', -1314.65, -389.594, 36.5871, 0),
(425, 'Ammunation Morningwood', -1315.43, -391.47, 36.5646, 0),
(426, 'Ammunation Morningwood', -1312.92, -391.458, 36.6863, 0),
(427, 'Ammunation Morningwood', -1310.39, -391.023, 36.6957, 0),
(428, 'Ammunation Morningwood', -1308.22, -391.94, 36.6958, 0),
(429, 'Ammunation Morningwood', -1306.55, -392.886, 36.6958, 0),
(430, 'Ammunation Morningwood', -1308.29, -394.359, 36.6958, 0),
(431, 'Ammunation Morningwood', -1310.96, -395.284, 36.6958, 0),
(432, 'Ammunation Morningwood', -1312.56, -394.399, 36.6958, 0),
(433, 'Ammunation Morningwood', -1306.3, -391.047, 36.6958, 0),
(434, 'Ammunation Morningwood', -1304.13, -390.363, 36.6958, 0),
(435, 'Ammunation Morningwood', -1302.78, -391.516, 36.6958, 0),
(436, 'Ammunation Morningwood', -1303.5, -393.421, 36.6958, 0),
(437, 'Ammunation Morningwood', -1312.06, -392.674, 36.6958, 0),
(438, '24/7 Harmony', 546.297, 2673.4, 42.1617, 0),
(439, '24/7 Harmony', 545.467, 2673.32, 42.1585, 0),
(440, '24/7 Harmony', 544.574, 2673.33, 42.1533, 0),
(441, '24/7 Harmony', 543.595, 2673.41, 42.1518, 0),
(442, '24/7 Harmony', 543.598, 2672.56, 42.1556, 0),
(443, '24/7 Harmony', 543.184, 2671.09, 42.1565, 0),
(444, '24/7 Harmony', 541.135, 2670.95, 42.1565, 0),
(445, '24/7 Harmony', 541.002, 2668.95, 42.1565, 0),
(446, '24/7 Harmony', 542.488, 2666.8, 42.1565, 0),
(447, '24/7 Harmony', 543.969, 2666.99, 42.1565, 0),
(448, '24/7 Harmony', 544.39, 2668.43, 42.1565, 0),
(449, '24/7 Harmony', 545.796, 2671.41, 42.1565, 0),
(450, '24/7 Harmony', 547.081, 2670.98, 42.1565, 0),
(451, '24/7 Harmony', 548.212, 2668.07, 42.1565, 0),
(452, '24/7 Harmony', 549.575, 2664.68, 42.1565, 0),
(453, '24/7 Harmony', 548.548, 2663.75, 42.1565, 0),
(454, '24/7 Harmony', 545.792, 2663.89, 42.1565, 0),
(455, 'Umgefallener Baum - Vinewood Hills', 1390.3, 661.699, 79.6028, 93.5988),
(456, 'Umgefallener Baum - Pacific Bluffs', -3005.88, 126.764, 14.8816, -106.638),
(457, 'Autounfall - Richman Glen', -1628.17, 1083.18, 152.661, 0),
(458, 'Autounfall - Richman Glen', -1627.45, 1082.79, 152.669, 0),
(459, 'Autounfall - Richman Glen', -1626.45, 1082.79, 152.673, 0),
(460, 'Autounfall - Richman Glen', -1627.05, 1084.85, 152.63, 0),
(461, 'Autounfall - Richman Glen', -1629.84, 1085.77, 152.599, 0),
(462, 'Autounfall - Richman Glen', -1631, 1084.14, 152.633, 0),
(463, 'Autounfall - Richman Glen', -1632.01, 1080.94, 152.699, 0),
(464, 'Autounfall - Richman Glen', -1631.2, 1076.99, 152.792, 0),
(465, 'Autounfall - Richman Glen', -1628.03, 1079.46, 152.738, 0),
(466, 'Autounfall - Richman Glen', -1625.24, 1081.88, 152.694, 0),
(467, 'Autounfall - Richman Glen', -1626.49, 1085.69, 152.612, 0),
(468, 'Autounfall - Richman Glen', -1627.48, 1088.55, 152.536, 0),
(469, 'Test', -3075.94, 230.253, 14.7516, 0),
(470, 'Richman', -1766.21, 794.266, 139.106, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `furniture`
--

CREATE TABLE `furniture` (
  `id` int(11) NOT NULL,
  `hash` varchar(64) NOT NULL DEFAULT '0',
  `name` varchar(64) NOT NULL DEFAULT 'n/A',
  `categorie` int(11) NOT NULL DEFAULT 0,
  `extra` int(2) NOT NULL DEFAULT 0,
  `price` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `furniture`
--

INSERT INTO `furniture` (`id`, `hash`, `name`, `categorie`, `extra`, `price`) VALUES
(6, '-457079481', 'Kleiderschrank-A', 2, 4, 125),
(7, '-388033105', 'Pflanze-A', 3, 0, 45),
(8, '110411286', 'Tür-A', 4, 5, 155),
(9, '1042741067', 'Tür-B', 4, 5, 135),
(10, '-1267889684', 'Tür-C', 4, 5, 125),
(11, '377646791', 'Holzbox-A', 14, 1, 100),
(12, '170995043', 'Werkbank-A', 6, 2, 300),
(13, '-126973474', 'Werkbank-B', 6, 2, 300),
(14, '1246927682', 'Werkbank-C', 6, 2, 300),
(15, '1726113796', 'Werkbank-D', 6, 2, 275),
(16, '890176606', 'Werkbank-E', 6, 2, 275),
(17, '-1555905221', 'Hebebühne-A', 6, 0, 500),
(18, '120509734', 'Hebebühne-B', 6, 0, 500),
(19, '-989597509', 'Hebebühne-C', 6, 0, 475),
(20, '-2137991637', 'Zielatrappe', 5, 0, 85),
(22, '1896964866', 'Bild-B', 22, 0, 125),
(23, '-1428269413', 'Bild-C', 22, 0, 125),
(24, '-1652016145', 'Bild-D', 22, 0, 125),
(25, '-1707196534', 'Bild-E', 22, 0, 125),
(26, '-1012100506', 'Bild-A', 22, 0, 125),
(27, '60046420', 'TV-A', 15, 0, 415),
(28, '-218195159', 'TV-B', 15, 0, 415),
(29, '-1528588652', 'TV-C', 15, 0, 415),
(30, '2095169631', 'TV-Tisch', 15, 1, 350),
(31, '-1350614016', 'Tannenbaum', 5, 0, 315),
(32, '-898971071', 'Werkzeugschrank', 6, 2, 315),
(33, '913522275', 'Tablet', 15, 0, 185),
(34, '1760825203', 'Klingel', 5, 0, 200),
(35, '-1113053091', 'Laptop-A', 15, 0, 450),
(36, '-1078906855', 'Laptop-B', 15, 0, 450),
(37, '69729596', 'TV-D', 15, 0, 500),
(38, '688554878', 'Kaffee', 7, 0, 20),
(39, '396006926', 'PC-A', 15, 0, 500),
(54, '1047183037', 'Lampe-A', 11, 0, 100),
(55, '-977875353', 'Lampe-B', 11, 0, 100),
(58, '-667536719', 'Küchenrolle-A', 7, 0, 20),
(59, '1971657777', 'Küchenrolle-B', 7, 0, 20),
(60, '14751329', 'Neonsign-A', 11, 0, 400),
(61, '514028339', 'Neonsign-B', 11, 0, 400),
(62, '-1925068611', 'Neonsign-C', 11, 0, 400),
(64, '1430014549', 'Pflanze-B', 3, 0, 50),
(65, '-1468640009', 'Teppich-A', 7, 0, 60),
(66, '1445081168', 'Teppich-B', 7, 0, 60),
(67, '-1210236344', 'Teppich-C', 7, 0, 60),
(68, '1598361869', 'Teppich-D', 7, 0, 60),
(69, '1978613345', 'Teppich-E', 7, 0, 60),
(70, '-1424152376', 'Teppich-F', 7, 0, 60),
(71, '-630768823', 'Teppich-G', 7, 0, 60),
(72, '-621527969', 'Teppich-H', 7, 0, 60),
(73, '-1963422616', 'Deko-A', 8, 0, 35),
(74, '732793894', 'Deko-B', 8, 1, 35),
(75, '-1114464763', 'Deko-C', 8, 1, 35),
(76, '-338658688', 'Deko-D', 8, 1, 40),
(77, '143143919', 'Deko-E', 8, 1, 38),
(78, '36546358', 'Deko-F', 8, 1, 42),
(79, '-1287349674', 'Pflanze-C', 3, 0, 40),
(80, '-503056428', 'Pflanze-D', 3, 0, 40),
(81, '-798075735', 'Pflanze-E', 3, 0, 40),
(82, '-900708383', 'Pflanze-F', 3, 0, 40),
(83, '-776790914', 'Badewanne-A', 9, 0, 180),
(84, '-1729561268', 'Bett-A', 2, 2, 210),
(85, '466551578', 'Bett-B', 2, 2, 213),
(86, '1111119442', 'Bett-C', 2, 2, 215),
(87, '-866701635', 'Bett-D', 2, 3, 310),
(88, '1688567163', 'Nachttischbett', 2, 2, 80),
(89, '-606800174', 'Stuhl-A', 10, 0, 60),
(90, '6963200', 'Stuhl-B', 10, 0, 60),
(91, '987641063', 'Stuhl-C', 10, 0, 60),
(92, '-1333092650', 'Stuhl-D', 10, 0, 60),
(93, '652816835', 'Stuhl-E', 10, 0, 60),
(94, '-864414942', 'Stehlampe-A', 11, 0, 43),
(95, '558728618', 'Stehlampe-B', 11, 0, 42),
(96, '1401940526', 'Stehlampe-C', 11, 0, 42),
(97, '2004890126', 'Stehlampe-D', 11, 0, 41),
(98, '-1970053448', 'Stehlampe-E', 11, 0, 41),
(99, '349041455', 'Stehlampe-F', 11, 0, 46),
(100, '-1348949822', 'Stehlampe-G', 11, 0, 46),
(101, '737845640', 'Stehlampe-H', 11, 0, 40),
(102, '424672659', 'Stehlampe-I', 11, 0, 50),
(103, '-1565454253', 'Stehlampe-J', 11, 0, 50),
(104, '493421917', 'Stehlampe-K', 11, 0, 50),
(105, '809398600', 'Stehlampe-L', 11, 0, 47),
(106, '1556139508', 'Stehlampe-M', 11, 0, 47),
(107, '-69177262', 'Nachttischlampe-A', 11, 0, 40),
(108, '635914467', 'Nachttischlampe-B', 11, 0, 35),
(109, '-1766511999', 'Nachttischlampe-C', 11, 0, 35),
(110, '-1578811167', 'Nachttischlampe-D', 11, 0, 35),
(111, '1070431691', 'Nachttischlampe-E', 11, 0, 36),
(112, '-153490459', 'Nachttischlampe-F', 11, 0, 36),
(113, '-1434080050', 'Nachttischlampe-G', 11, 0, 33),
(114, '-772466644', 'Nachttischlampe-H', 11, 0, 35),
(115, '2002674160', 'Nachttischlampe-I', 11, 0, 35),
(116, '284048046', 'Deckenlampe-A', 11, 0, 90),
(117, '1234485355', 'Deckenlampe-B', 11, 0, 80),
(118, '678755888', 'Deckenlampe-C', 11, 0, 80),
(119, '-1538737514', 'Deckenlampe-D', 11, 0, 80),
(120, '1738496974', 'Sessel-A', 13, 1, 140),
(121, '2019753297', 'Stuhl-F', 10, 0, 65),
(122, '1722309084', 'Stuhl-G', 10, 0, 66),
(123, '-234688365', 'Stuhl-H', 10, 0, 63),
(124, '-868509571', 'Stuhl-I', 10, 0, 63),
(125, '-1482600631', 'Stuhl-J', 10, 0, 63),
(126, '49088219', 'Stuhl-K', 10, 0, 63),
(127, '-1168018231', 'Sessel-B', 13, 0, 120),
(128, '338307413', 'Sessel-C', 13, 1, 160),
(129, '-1210781524', 'Sessel-D', 13, 0, 140),
(130, '-1785811936', 'Sessel-E', 13, 0, 130),
(131, '-67059393', 'Sessel-F', 13, 1, 135),
(132, '1488091809', 'Sessel-G', 13, 1, 135),
(133, '-393569709', 'Sessel-H', 13, 1, 138),
(134, '-999534061', 'Sessel-I', 13, 1, 137),
(135, '-689899780', 'Sessel-J', 13, 1, 137),
(136, '-1608185467', 'Sessel-K', 13, 1, 135),
(137, '1740052654', 'Sessel-L', 13, 1, 135),
(138, '-168596674', 'Sessel-M', 13, 0, 130),
(139, '-1515435323', 'Sessel-N', 13, 0, 130),
(140, '-671738639', 'Liege-A', 16, 0, 100),
(141, '94826578', 'Liege-B', 16, 0, 100),
(142, '39200959', 'Sofa-A', 12, 1, 200),
(143, '-401852480', 'Sofa-B', 12, 1, 250),
(144, '-361055067', 'Sofa-C', 12, 1, 250),
(145, '-726363879', 'Sofa-D', 12, 1, 250),
(146, '1443107762', 'Sofa-E', 12, 1, 250),
(147, '-968395721', 'Sofa-F', 12, 1, 250),
(148, '-1309717625', 'Sofa-G', 12, 1, 250),
(149, '-1118217677', 'Sofa-H', 12, 1, 240),
(150, '-1223496606', 'Tvwand-A', 15, 1, 350),
(151, '-1820646534', 'Tvwand-B', 15, 1, 350),
(152, '-1546399138', 'TV-E', 15, 1, 390),
(153, '1653710254', 'TV-F', 15, 1, 330),
(154, '170618079', 'TV-G', 15, 1, 335),
(155, '-1600421347', 'Schrank-A', 17, 2, 180),
(156, '-743637179', 'Regal-A', 18, 1, 140),
(157, '658311972', 'Regal-B', 18, 1, 100),
(158, '-1810368652', 'Sideboard-A', 19, 1, 170),
(159, '1706990326', 'Sideboard-B', 19, 1, 175),
(160, '2084096798', 'Sideboard-C', 19, 1, 175),
(161, '-1742568715', 'Sideboard-D', 19, 1, 175),
(162, '-909613504', 'Sideboard-E', 19, 1, 171),
(163, '-254030581', 'Sideboard-F', 19, 1, 165),
(164, '288263604', 'Sideboard-G', 19, 1, 155),
(165, '-1128082619', 'Sideboard-H', 19, 1, 155),
(166, '1769221281', 'Sideboard-I', 19, 0, 120),
(167, '-1189382354', 'Tisch-A', 20, 0, 100),
(168, '-235935530', 'Tisch-B', 20, 0, 100),
(169, '-799726175', 'Tisch-C', 20, 0, 100),
(170, '-1106542610', 'Tisch-D', 20, 0, 110),
(171, '1064015289', 'Tisch-E', 20, 0, 100),
(172, '1352775717', 'Tisch-F', 20, 1, 130),
(173, '-949790410', 'Tisch-G', 20, 1, 125),
(174, '543918917', 'Tisch-H', 20, 0, 125),
(175, '-2127868733', 'Tisch-I', 20, 0, 100),
(176, '-48085841', 'Tisch-J', 20, 0, 100),
(177, '-9376854', 'Tisch-K', 20, 0, 100),
(178, '-1802365458', 'Tisch-L', 20, 0, 100),
(179, '-1085948709', 'Nachttischlampe-J', 11, 0, 35),
(180, '298623376', 'Sessel-O', 13, 1, 135),
(181, '954232759', 'Sessel-P', 13, 1, 135),
(182, '654887944', 'Sessel-Q', 13, 1, 135),
(183, '682082323', 'Stuhl-L', 10, 0, 60),
(184, '-864163686', 'Bett-E', 2, 1, 300),
(185, '-1103279079', 'Bett-F', 2, 1, 314),
(186, '-957463636', 'Sofa-I', 12, 1, 260),
(187, '1769203125', 'Nachttischlampe-K', 11, 0, 40),
(188, '-1811563816', 'Nachttischlampe-L', 11, 0, 40),
(189, '1803549050', 'Crosstrainer-A', 21, 0, 210),
(190, '1732037892', 'Laufband', 21, 0, 250),
(191, '2052672560', 'Bild-F', 22, 0, 125),
(192, '-1379028208', 'Bild-G', 22, 0, 120),
(193, '-1071622219', 'Bild-H', 22, 0, 120),
(194, '-832900054', 'Bild-I', 22, 0, 120),
(195, '2129114391', 'Wandkunst-A', 22, 0, 120),
(196, '630784631', 'Bild-J', 22, 0, 120),
(197, '913482794', 'Bild-K', 22, 0, 120),
(198, '1061810055', 'Trikot-A', 22, 0, 210),
(199, '70121804', 'Trikot-B', 22, 0, 210),
(200, '-1516927114', 'Tür-D', 4, 5, 120),
(201, '-1278729253', 'Tür-E', 4, 5, 120),
(202, '-738253', 'Tür-F', 4, 5, 120),
(203, '-833759002', 'Tür-G', 4, 5, 120),
(204, '-270590968', 'Tür-H', 4, 5, 120),
(205, '956182085', 'Tür-I', 4, 5, 120),
(206, '-264728216', 'Tür-J', 4, 5, 120),
(207, '-1687047623', 'Tür-K', 4, 5, 120),
(208, '-658026477', 'Tür-L', 4, 5, 120),
(209, '-711771128', 'Tür-M', 4, 5, 120),
(210, '1927676967', 'Tür-N', 4, 5, 120),
(211, '103339342', 'Tür-O', 4, 5, 120),
(212, '-1186396713', 'Tür-P', 4, 5, 120),
(213, '1398355146', 'Tür-Q', 4, 5, 120),
(214, '-1633987155', 'Tür-R', 4, 5, 120),
(215, '563392945', 'Tür-S', 4, 0, 120),
(216, '1901183774', 'Tür-T', 4, 5, 120),
(217, '-2037125726', 'Tür-U', 4, 5, 120),
(218, '-739665083', 'Tür-V', 4, 5, 120),
(219, '-2003105485', 'Tür-W', 4, 5, 120),
(220, '-1573772550', 'Tor-A', 23, 0, 300),
(221, '-1720238398', 'Tür-X', 4, 5, 120),
(222, '-1563799200', 'Tür-Y', 4, 5, 120),
(223, '-1574447115', 'Flaschen-A', 24, 0, 20),
(224, '-715967502', 'Flaschen-B', 24, 0, 20),
(225, '1096585990', 'Barstuhl-A', 25, 0, 100),
(226, '-1126331894', 'Barstuhl-B', 25, 0, 100),
(227, '-1348472945', 'Barstuhl-C', 25, 0, 100),
(228, '-1635234464', 'Barstuhl-D', 25, 0, 100),
(229, '-1938226077', 'Geld-A', 5, 0, 100),
(230, '-470815620', 'Stuhl-M', 10, 0, 60),
(231, '1151045333', 'Stuhl-N', 10, 0, 60),
(232, '-1673752417', 'Campingbett', 5, 0, 107),
(233, '388143302', 'Koffer-A', 5, 1, 45),
(234, '-656978571', 'Deckenvent-A', 5, 0, 95),
(235, '1448093015', 'Sessel-R', 13, 1, 130),
(236, '1896897239', 'Sessel-S', 13, 1, 130),
(237, '-1910586096', 'Tür-Z', 4, 5, 120),
(238, '2065196134', 'Spint-A', 5, 2, 60),
(239, '1439105467', 'Waffenkoffer-A', 14, 1, 80),
(240, '-1774325522', 'Safe-A', 14, 2, 200),
(241, '-1346995970', 'Safetür-A', 4, 5, 100),
(242, '275795038', 'Wischer', 26, 0, 15),
(243, '-1397488862', 'Geld-B', 5, 0, 50),
(244, '-1824753883', 'Geld-C', 5, 0, 90),
(245, '-1698761910', 'Geld-D', 5, 0, 500),
(246, '1762103260', 'Geld-E', 5, 0, 500),
(247, '-197122485', 'Geld-F', 5, 0, 400),
(248, '816815913', 'Geld-G', 5, 0, 1000),
(249, '1360148151', 'Cashtrolley', 5, 0, 98),
(250, '-1780563953', 'Stuhl-O', 10, 0, 65),
(251, '598033448', 'Jukebox-A', 27, 0, 150),
(252, '836690075', 'Jukebox-B', 27, 0, 180),
(253, '1722009340', 'Jukebox-C', 27, 0, 170),
(254, '384965730', 'Laptop-C', 15, 0, 400),
(255, '82999335', 'Laptop-D', 15, 0, 400),
(256, '637672069', 'Stuhl-P', 10, 0, 64),
(257, '-1969563019', 'Sofa-J', 12, 1, 250),
(258, '852117134', 'Backpulver-A', 24, 0, 20),
(259, '-1266741560', 'Backpulver-B', 24, 0, 20),
(260, '-1892518878', 'Kokainblock', 5, 0, 1300),
(261, '-2086291657', 'Kokainflasche-A', 24, 5, 17),
(262, '728434663', 'Kokainflasche-B', 24, 5, 17),
(263, '-2122380018', 'Toybox-A', 5, 0, 20),
(264, '31086348', 'Kokain-A', 5, 0, 500),
(265, '-605287632', 'Kokain-B', 5, 0, 600),
(266, '-303457828', 'Kokain-C', 5, 0, 600),
(267, '286149026', 'Spielzeug-A', 5, 0, 30),
(268, '579156093', 'Toybox-B', 5, 0, 150),
(269, '-1094533482', 'Toybox-C', 5, 0, 30),
(270, '-440576300', 'Palette-A', 5, 0, 50),
(271, '-289305948', 'Milchpulver-A', 24, 0, 20),
(272, '-666057613', 'Milchpulver-B', 24, 0, 20),
(273, '1149677738', 'Presse-A', 6, 0, 200),
(274, '2116540373', 'Presse-B', 6, 0, 200),
(275, '-1928194470', 'Küchenwage-A', 24, 0, 50),
(276, '-1450651833', 'Küchenwage-B', 24, 0, 100),
(277, '-653303203', 'Box-A', 14, 3, 350),
(278, '-1158162337', 'Tasche-A', 5, 1, 50),
(279, '2144032200', 'Müllsack-A', 28, 1, 5),
(280, '431612787', 'Ventilator-A', 5, 0, 45),
(281, '-1519426', 'Waffenschrank-A', 14, 2, 200),
(282, '-1774732668', 'Ammubox-A', 14, 1, 100),
(283, '-1281587804', 'Stuhl-Q', 10, 0, 60),
(284, '807263738', 'Stehventilator-A', 5, 0, 60),
(285, '304964818', 'Tisch-M', 20, 0, 80),
(286, '-291936308', 'Bild-L', 22, 0, 120),
(287, '380123113', 'Bild-M', 22, 0, 120),
(288, '1418196498', 'Bild-N', 22, 0, 120),
(289, '658611078', 'Bild-O', 22, 0, 120),
(290, '-1501554163', 'Bild-P', 22, 0, 120),
(291, '1769840124', 'Deko-G', 8, 0, 30),
(292, '1770552192', 'Deko-H', 8, 0, 30),
(293, '-834149333', 'Deko-I', 8, 1, 30),
(294, '288107954', 'Deko-J', 8, 1, 30),
(295, '-17200819', 'Deko-K', 8, 1, 30),
(296, '1517600834', 'Deko-L', 8, 1, 30),
(297, '1450555460', 'Deko-M', 8, 1, 30),
(298, '1213275131', 'Deko-N', 8, 1, 30),
(299, '-171808030', 'Deko-O', 8, 1, 30),
(300, '-356585437', 'Deko-P', 8, 1, 50),
(301, '-586918738', 'Deko-Q', 8, 1, 50),
(302, '-1450580483', 'Deko-R', 8, 1, 50),
(303, '2056226825', 'Deko-S', 8, 1, 50),
(304, '-724321136', 'Deko-T', 8, 0, 50),
(305, '-1112352786', 'Deko-U', 8, 0, 50),
(306, '-257540644', 'Früchte-A', 24, 0, 35),
(307, '1183048071', 'Kaffeemaschine-A', 24, 0, 90),
(308, '746287557', 'Tisch-N', 20, 0, 120),
(309, '-1368031343', 'Deckenlampe', 11, 0, 90),
(310, '-636869637', 'Sofa-K', 12, 0, 230),
(311, '1227733354', 'Sofa-L', 12, 0, 230),
(312, '1474287310', 'Sofa-M', 12, 0, 230),
(313, '-836132965', 'Stadtmodel-A', 5, 0, 500),
(314, '-1447294189', 'SektuZig', 24, 0, 70),
(315, '-759050414', 'Zigaretten-A', 5, 0, 50),
(316, '-518493185', 'Zigaretten-B', 5, 0, 50),
(317, '436414068', 'Hardwarebox-A', 15, 1, 100),
(318, '-2088099114', 'Hardwarebox-B', 15, 1, 100),
(319, '-1309507674', 'Hardwarebox-C', 15, 1, 100),
(320, '184603368', 'Gem-A', 29, 0, 5000),
(321, '-389443974', 'Gem-B', 29, 0, 5000),
(322, '764319747', 'Gem-C', 29, 0, 7000),
(323, '-1112165181', 'Elfenbein-A', 29, 0, 4000),
(324, '1290417494', 'Elfenbein-B', 29, 0, 7000),
(325, '-1650370877', 'Elfenbein-C', 29, 0, 6000),
(326, '-741681719', 'Uhren-A', 29, 0, 1500),
(327, '-777560671', 'Uhren-B', 29, 0, 3500),
(328, '-665064690', 'Uhren-C', 29, 0, 5500),
(329, '2103944091', 'Meds-A', 30, 0, 100),
(330, '-760066513', 'Meds-B', 30, 0, 150),
(331, '-521999728', 'Meds-C', 30, 0, 130),
(332, '729481163', 'Meds-D', 30, 1, 90),
(333, '-1671234267', 'Kunstwerk-A', 29, 0, 20000),
(334, '-2027105607', 'Kunstwerk-B', 29, 0, 22000),
(335, '959231658', 'Kunstwerk-C', 29, 0, 19000),
(336, '-1292859312', 'Silber-A', 29, 0, 1300),
(337, '1105678628', 'Silber-B', 29, 0, 1600),
(338, '1478589848', 'Silber-C', 29, 0, 2000),
(339, '-249415613', 'Box-B', 14, 2, 200),
(340, '-1143129136', 'Box-C', 14, 2, 200),
(341, '-265116550', 'Box-D', 14, 2, 200),
(342, '1790162299', 'Box-E', 14, 3, 500),
(343, '2055492359', 'Box-F', 14, 3, 500),
(344, '1815664890', 'Box-G', 14, 3, 500),
(345, '1397410834', 'Box-H', 14, 3, 500),
(346, '1057918179', 'Box-I', 14, 3, 500),
(347, '-1655753417', 'Box-J', 14, 3, 500),
(348, '21331302', 'Box-K', 14, 3, 500),
(349, '2014503631', 'Box-L', 14, 3, 500),
(350, '212795208', 'Box-M', 14, 3, 500),
(351, '2092857693', 'Box-N', 14, 2, 300),
(352, '-1958', 'Box-O', 14, 2, 300),
(353, '-1956474755', 'Box-P', 14, 2, 300),
(354, '-1133780421', 'Aschenbecher-A', 5, 0, 30),
(355, '-1227143673', 'Box-Q', 14, 3, 500),
(356, '1915002422', 'Box-R', 14, 3, 500),
(357, '654562429', 'Box-S', 14, 3, 500),
(358, '-1129076059', 'Box-T', 14, 3, 500),
(359, '-290560280', 'Box-U', 14, 3, 500),
(360, '797797701', 'Box-V', 14, 3, 500),
(361, '-80652213', 'Box-W', 14, 3, 30000),
(362, '562429577', 'Egg', 29, 0, 16500),
(363, '-495810123', 'Menschenhaut', 29, 0, 15000),
(364, '73014697', 'Werkzeugschrank-B', 6, 2, 300),
(365, '1881506600', 'Award-A', 8, 0, 200),
(366, '1485377863', 'Award-B', 8, 0, 200),
(367, '-589191305', 'Award-C', 8, 0, 200),
(368, '-352385374', 'Award-D', 8, 0, 200),
(369, '1872072409', 'Award-E', 8, 0, 200),
(370, '314496444', 'Bett-G', 2, 0, 120),
(371, '743064848', 'Monitor-A', 15, 0, 100),
(372, '-1626066319', 'Stuhl-R', 10, 0, 95),
(373, '-1278649385', 'Stuhl-S', 10, 0, 80),
(374, '1580642483', 'Stuhl-T', 10, 0, 75),
(375, '1339364336', 'Sessel-T', 13, 1, 200),
(376, '-1372869508', 'Monitor-B', 15, 0, 105),
(377, '-978849650', 'Receiver-A', 15, 0, 65),
(378, '1881864012', 'Fernbedienung-A', 15, 0, 15),
(379, '796317896', 'Liege-C', 16, 0, 105),
(380, '-1272174018', 'Bett-H', 2, 1, 95),
(381, '355657258', 'Tisch-O', 20, 0, 100),
(382, '56705671', 'Tisch-P', 20, 0, 100),
(383, '276650500', 'Ventilator-B', 5, 0, 45),
(384, '-772025265', 'Maschine-A', 6, 0, 1000),
(385, '-1107776439', 'Maschine-B', 6, 0, 1000),
(386, '749471724', 'Console-A', 6, 0, 200),
(387, '-810969539', 'Akkuschrauber-A', 6, 0, 130),
(388, '1062447216', 'Akkuschrauber-B', 6, 0, 130),
(389, '-1438894292', 'Schleifer-A', 6, 0, 160),
(390, '-1634417838', 'Hammer-A', 6, 0, 25),
(391, '1406038645', 'Schlüssel-A', 5, 0, 5),
(392, '865942478', 'Maschine-C', 6, 0, 900),
(393, '-2057085095', 'Maschine-D', 6, 0, 900),
(394, '1177673975', 'Maschine-E', 6, 0, 900),
(395, '1816174307', 'Zange-A', 6, 0, 33),
(396, '-1083095741', 'Zange-B', 6, 0, 33),
(397, '1065600362', 'Zange-C', 6, 0, 33),
(398, '-863733544', 'Box-X', 14, 2, 250),
(399, '-1673979170', 'Box-Y', 14, 2, 250),
(400, '1433923164', 'Box-Z', 14, 2, 250),
(401, '249707472', 'Maschine-F', 6, 0, 850),
(402, '471619140', 'Maschine-G', 6, 0, 850),
(403, '-769899971', 'Maschine-H', 6, 0, 850),
(404, '1185364510', 'Klebeband', 6, 0, 10),
(405, '1052230687', 'Werkzeugkoffer-A', 6, 1, 50),
(406, '-2095296433', 'Werkzeugkoffer-B', 6, 1, 50),
(407, '143311276', 'Werkzeugkiste-A', 6, 2, 150),
(408, '-1735390234', 'Werkzeugkiste-B', 6, 2, 150),
(409, '-1433948203', 'Werkzeugkiste-C', 6, 2, 250),
(410, '-619343632', 'Werkzeugkiste-D', 6, 3, 450),
(411, '1641567982', 'Schraubenschlüssel-A', 6, 0, 20),
(412, '-1737968014', 'Maschine-I', 6, 0, 850),
(413, '-2027482129', 'Maschine-J', 6, 0, 850),
(414, '-517093473', 'Maschine-K', 6, 0, 850),
(415, '-785319525', 'Tischzwinge-A', 6, 0, 60),
(416, '327628301', 'Disc-A', 22, 0, 40),
(417, '-279450193', 'Disc-B', 22, 0, 40),
(418, '-539078980', 'Disc-C', 22, 0, 40),
(419, '-893639560', 'Disc-D', 22, 0, 40),
(420, '1832337418', 'Pflanze-G', 3, 0, 30),
(421, '2138367109', 'Pflanze-H', 3, 0, 30),
(422, '-641583331', 'Teppich-I', 7, 0, 60),
(423, '1228752495', 'Teppich-J', 7, 0, 60),
(424, '1539730305', 'Teppich-K', 7, 0, 60),
(425, '1741063045', 'Teppich-L', 7, 0, 60),
(426, '-442012286', 'Kiste-A', 8, 1, 20),
(427, '-699619545', 'Sideboard-J', 19, 1, 150),
(428, '-1857343250', 'Bett-I', 2, 1, 210),
(429, '1758176010', 'Bier-A', 24, 0, 20),
(430, '1482870357', 'Stuhl-U', 10, 0, 60),
(431, '-2033210578', 'Stuhl-V', 10, 0, 60),
(432, '2079364464', 'Stuhl-W', 10, 0, 60),
(433, '-1440452137', 'Stuhl-X', 10, 0, 60),
(434, '696447118', 'Stuhl-Y', 10, 0, 60),
(435, '46768928', 'Stuhl-Z', 10, 0, 60),
(436, '1916209788', 'Tischundstuhl-A', 24, 0, 250),
(437, '275188277', 'Mülleimer-A', 24, 1, 19),
(438, '983107514', 'Kaffeemaschine-B', 24, 0, 95),
(439, '1767370432', 'Stehlampe-N', 11, 0, 50),
(440, '-85388824', 'Stehlampe-O', 11, 0, 50),
(441, '-780981863', 'Nachttischlampe-M', 11, 0, 40),
(442, '-2012997956', 'Nachttischlampe-N', 11, 0, 40),
(443, '-1312560581', 'Nachttischlampe-O', 11, 0, 40),
(444, '-383314458', 'Deckenlampe-E', 11, 0, 75),
(445, '-1226030074', 'Deckenlampe-F', 11, 0, 75),
(446, '-977280595', 'Deckenlampe-G', 11, 0, 75),
(447, '1874679314', 'Bong-A', 5, 0, 30),
(448, '-1906772306', 'Alarmknopf-A', 31, 0, 20),
(449, '-1007354661', 'Kamera-A', 31, 0, 60),
(450, '-1842407088', 'Kamera-B', 31, 0, 60),
(451, '-647884455', 'Deckenlampe-H', 11, 0, 100),
(452, '-976225932', 'Tor-B', 23, 0, 250),
(453, '815741875', 'Tor-C', 23, 0, 350),
(454, '-737433441', 'Testwamd1', 32, 0, 10),
(455, '-433280915', 'Telefon-A', 15, 0, 49),
(456, '1652829067', 'Tor-D', 23, 0, 350),
(457, '1529620568', 'Tor-E', 23, 0, 350),
(458, '-889258808', 'Rollwagen-A', 6, 1, 85),
(459, '-636408770', 'Gold-A', 29, 0, 35000),
(460, '-1605837712', 'Telefon-B', 15, 0, 45),
(461, '-1388847408', 'Panel-A', 31, 0, 86),
(462, '79209609', 'Monitor-C', 15, 0, 150),
(463, '-637483755', 'Monitor-D', 15, 0, 250),
(464, '-468144679', 'Statue-A', 29, 0, 4500),
(465, '269934519', 'Geldwagen-A', 29, 0, 5500),
(466, '-108416355', 'Geldwagen-B', 29, 0, 3500),
(467, '769923921', 'Rollwagen-B', 6, 1, 85),
(468, '-2122821887', 'Panel-B', 31, 0, 85),
(469, '-1659828682', 'Panel-C', 31, 0, 65),
(470, '-818415955', 'Monitor-E', 15, 0, 150),
(471, '-2107935824', 'Whiteboard-A', 33, 0, 105),
(472, '-1691492531', 'Wärmelampe-A', 11, 0, 515),
(473, '-1451197454', 'Wärmelampe-B', 11, 0, 715);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `furniturecategories`
--

CREATE TABLE `furniturecategories` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `furniturecategories`
--

INSERT INTO `furniturecategories` (`id`, `name`) VALUES
(1, 'Wohnzimmer'),
(2, 'Schlafzimmer'),
(3, 'Pflanzen'),
(4, 'Türen und Tore'),
(5, 'Sonstiges'),
(6, 'Werkstatt'),
(7, 'Teppich'),
(8, 'Deko'),
(9, 'Badezimmer'),
(10, 'Stuehle'),
(11, 'Lampen'),
(12, 'Sofas'),
(13, 'Sessel'),
(14, 'Lager'),
(15, 'Technik'),
(16, 'Liege'),
(17, 'Schränke'),
(18, 'Regale'),
(19, 'Sideboard'),
(20, 'Tische'),
(21, 'Sport'),
(22, 'Bilder'),
(24, 'Küche'),
(25, 'Barstühle'),
(26, 'Reinigung'),
(27, 'Unterhaltung'),
(28, 'Müll'),
(29, 'Luxus'),
(30, 'Medikamente'),
(31, 'Sicherheit'),
(32, 'Test'),
(33, 'Büro');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `furniturehouse`
--

CREATE TABLE `furniturehouse` (
  `id` int(11) NOT NULL,
  `house` int(11) NOT NULL DEFAULT 0,
  `extra` int(2) NOT NULL DEFAULT 0,
  `name` varchar(64) NOT NULL DEFAULT 'n/A',
  `hash` varchar(64) NOT NULL DEFAULT 'n/A',
  `position` varchar(128) NOT NULL DEFAULT '0.0|0.0|0.0|0.0|0.0|0.0|0',
  `props` longtext NOT NULL,
  `price` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gangzones`
--

CREATE TABLE `gangzones` (
  `id` int(11) NOT NULL,
  `name` varchar(65) NOT NULL,
  `value` varchar(35) NOT NULL,
  `posx` varchar(10) NOT NULL,
  `posy` varchar(10) NOT NULL,
  `posz` varchar(10) NOT NULL,
  `heading` varchar(10) NOT NULL,
  `color` int(3) NOT NULL DEFAULT 39,
  `radius` int(3) NOT NULL DEFAULT 50,
  `percentages` longtext NOT NULL,
  `things` int(6) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `gangzones`
--

INSERT INTO `gangzones` (`id`, `name`, `value`, `posx`, `posy`, `posz`, `heading`, `color`, `radius`, `percentages`, `things`) VALUES
(2, 'ElBurro', 'Geld', '1540.17', '-2137.44', '77.22', '-175.65', 40, 70, '[]', 0),
(3, 'AltaStreet', 'Drogen', '-301.81', '-1641.23', '32.11', '136.03', 40, 60, '[]', 0),
(4, 'LaMesa', 'Drogen', '718.18', '-1218.46', '26.01', '-103.49', 40, 50, '[]', 0),
(5, 'Mirrorpark', 'Drogen', '1135.95', '-645.19', '56.74', '-72.17', 40, 55, '[]', 0),
(6, 'Murrieta', 'Materialien', '1202.36', '-1261.89', '35.23', '-156.68', 40, 55, '[]', 0),
(7, 'Rancho', 'Materialien', '515.5', '-1948.78', '24.99', '114.11', 40, 55, '[]', 0),
(8, 'Elysian', 'Materialien', '-474.77', '-2808.85', '5.99', '-40.86', 40, 70, '[]', 0),
(9, 'Vespucci', 'Geld', '-1275.57', '-1530.69', '4.31', '-100.48', 40, 60, '[]', 0),
(11, 'DelPerro', 'Geld', '-1527.28', '-434.16', '35.44', '134.43', 40, 60, '[]', 0),
(13, 'Hawick', 'Materialien', '284.04', '-194.95', '61.57', '-134.93', 40, 50, '[]', 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `garbageroutes`
--

CREATE TABLE `garbageroutes` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `routes` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `garbageroutes`
--

INSERT INTO `garbageroutes` (`id`, `name`, `routes`) VALUES
(1, 'South Los Santos', '2.5734634,-1352.0028,29.325624|-28.049452,-1352.7498,29.319595|-38.56758,-1352.7006,29.322336|-50.518806,-1351.4177,29.329252|-87.975876,-1330.2919,29.29429|-87.99531,-1298.9127,29.301853|-88.11599,-1287.8364,29.298119|-88.09577,-1278.3083,29.29816|-140.35565,-1362.8479,29.347733|-189.57452,-1374.7352,31.258224|-166.54478, -1414.1825, 30.969149|-77.72002,-1382.6547,29.3208|-16.345375,-1389.3418,29.363941|2.1532252,-1385.8724,29.29755|48.604465,-1385.7236,29.311308|97.12197,-1439.2903,29.291155|77.37971,-1461.2351,29.327684|65.52661,-1471.4519,29.291307|46.35832,-1479.2429,29.288052|-93.63894,-1469.6262,33.085865|-49.3318,-1477.3649,31.978025|-28.232428,-1499.0137,30.50235|-10.452841,-1488.3099,30.10291|-1.2271324,-1480.6364,30.326633|-8.162475,-1564.6561,29.287436|99.07292,-1655.374,29.291258|158.14278,-1654.2446,29.291685|140.31047,-1591.9393,29.405678|119.44987,-1541.7104,29.190182|96.23862,-1526.0438,29.2092'),
(2, 'East Los Santos', '110.38887,-1745.8225,29.17464|191.72464,-1812.828,28.74064|191.6293,-1812.9498,28.746632|223.02583,-1838.9541,27.163168|231.22057,-1847.1775,26.822205|339.60168,-1964.1842,24.522846|337.63278,-1966.241,24.457582|372.02158,-1984.3496,24.12846|374.6263,-1984.7281,24.142118|414.4233,-2012.1022,23.086872|417.2872,-2013.705,23.098238|496.05154,-1886.9728,25.912329|422.5514,-1883.9767,26.488873|432.9755,-1878.9597,27.021526|396.53928,-1923.8735,24.774895|139.89203,-1722.8799,29.222511|100.59155,-1692.4847,29.205164|95.9725,-1687.13,29.21256|99.118675,-1655.2737,29.284458|121.80367,-1540.4774,29.312908|97.58276,-1523.6632,29.36384|117.79122,-1463.2109,29.295666|139.71487,-1364.0442,29.316725|138.13428,-1363.0115,29.315586|2.2548718,-1351.9105,29.379969|-50.590916,-1351.0607,29.328085|-311.2833,-1535.3214,27.837381|-314.05264,-1540.3031,27.898794|'),
(3, 'Innenstadt', '335.7659,-1074.3337,29.555307|341.2841,-1076.737,29.530981|348.65668,-1071.1777,29.582148|379.7592,-901.25684,29.42468|416.6559,-774.87274,29.379135|475.54825,-600.0486,28.499699|161.04817,-776.6121,31.759655|-561.10846,-703.61176,33.017414|-562.0688,-707.87646,33.002956|-561.50195,-709.4044,32.98552|-515.3467,-813.6617,30.480862|-559.8165,-822.5354,27.530754|-656.07025,-893.9073,24.719755|-713.9175,-1134.2582,10.612625|-709.4115,-1139.5343,10.612642|118.38755,-1002.3382,29.40194|116.95777,-1021.5205,29.285479|167.18382,-1031.729,29.326712|189.62485,-1047.0817,29.324652|215.89308,-1021.3529,29.275864|238.7515,-1014.1552,29.3082|257.02542,-992.6817,29.271212|251.23843,-975.5856,29.406855|234.36319,-957.59985,29.289337|276.40167,-906.7313,29.011784|289.3545,-902.18036,29.041536|273.09195,-873.6741,29.302996|257.56598,-860.72943,29.340168|253.64227,-865.6755,29.40743|231.4136,-840.8178,30.158478|221.64157,-831.26886,30.464869|216.54555,-817.84216,30.54997|151.5311,-841.60016,30.93716|141.05698,-854.28265,30.799397|139.57849,-875.41254,30.504452|134.92699,-901.47656,30.288364|141.64523,-917.6429,30.13869|156.04875,-916.1362,30.143024|149.8716,-933.0252,29.944391|144.4107,-947.68774,29.772602|133.26677,-976.25134,29.393469|'),
(4, 'Vespucci', '-715.1959,-1169.7262,10.610853|-709.57574,-1139.3195,10.612625|-713.9447,-1134.3024,10.612623|-846.31396,-1113.384,7.0615177|-849.3903,-1113.1012,7.0620413|-1099.5957,-1287.1616,5.462227|-1251.4955,-1281.0797,4.004455|-1243.3011,-1359.5338,4.0668497|-1215.4948,-1423.765,4.323517|-1213.1127,-1426.6344,4.353111|-1193.8595,-1511.8092,4.3655353|-1192.0476,-1514.5009,4.375064|-1206.9768,-1481.7043,4.368028|-1205.7762,-1480.025,4.3796735|-1202.7761,-1478.1631,4.3796735|-1099.0941,-1590.4458,4.5922127|-1032.1292,-1564.8744,5.131224|-1031.1703,-1566.8434,5.137092|'),
(5, 'Vinewood', '373.9168,212.81728,103.04619|372.7677,210.4234,103.04814|382.36313,250.86397,103.0265|380.1182,251.7654,103.04284|378.32516,252.44078,103.0394|133.40436,484.62988,145.84695|-756.2298,690.3315,144.75679|-1177.6881,724.3068,151.47476|-1178.5814,723.0042,151.31902|-1179.563,721.77997,151.1206|-1251.3492,655.93756,141.33052|-1254.4006,655.00037,141.1391|-1256.0774,653.84033,140.98482|-1258.2345,653.1081,140.81529|-1250.1862,460.3987,93.48911|-1245.9672,460.7631,93.13429|-1233.1423,386.42203,75.37647|-711.6179,-190.79362,37.038227|'),
(6, 'Garbage', '104.1774,-995.3586,29.313307|99.1181,-985.1293,29.373545|115.55818,-988.8293,29.407608|117.32276,-987.84814,29.407259|138.11626,-992.10583,29.358696|159.06995,-1005.28534,29.439915|159.3942,-1015.0834,29.392824|146.67426,-1023.4427,29.379755|167.26706,-1038.5079,29.321|179.48645,-1036.0133,29.315245|205.63376,-1019.79803,29.336187|218.7204,-1030.9585,29.37893|234.54352,-1036.9333,29.381823|249.71962,-1010.98755,29.268593|249.06691,-992.1683,29.302572|245.10149,-966.3088,29.332542|231.89775,-957.23663,29.318897|239.45294,-940.43695,29.28337|276.9377,-950.22144,29.39512|312.58548,-960.55835,29.416487|324.23624,-955.3783,29.41806|334.59778,-947.43726,29.480284|255.09863,-903.6858,28.853073|277.14334,-879.2,29.235256|289.62164,-874.5217,29.319351|302.84836,-873.00757,29.253738|288.92267,-854.285,29.196438|260.9614,-840.48645,29.469437|249.79321,-828.68524,29.79296|227.91638,-818.5968,30.360443|229.71515,-800.2927,30.571495|218.42372,-786.7422,30.80317|191.871,-820.27893,31.272999|145.82288,-842.92725,30.937069|137.8068,-862.77435,30.690727|150.64944,-876.0369,30.786274|165.2044,-886.22003,30.502594|159.16486,-911.9657,30.181181|173.56355,-919.1602,30.686872|196.28331,-935.4894,30.686811|111.39436,-1070.8729,29.192524|124.97473,-1061.2234,29.192354|151.26775,-1055.9156,29.213438|73.80456,-977.78217,29.370796|58.749016,-964.46497,29.357576|38.758537,-954.78107,29.31421|11.128748,-946.50385,29.357351|-17.992182,-968.575,29.399572|0.7853608,-977.5435,29.536858|25.45245,-988.33765,29.420193|50.906784,-992.8824,29.23833|73.19589,-1004.40405,29.35524|78.3179,-1022.9245,29.477348|71.91441,-1052.3779,29.351677|56.774082,-1086.7474,29.337814|39.85522,-1099.2191,29.300114|23.900328,-1121.5953,29.179972|7.984117,-1129.1154,28.525696|-16.220585,-1122.7709,27.179789|-16.761427,-1106.3938,26.672054|-25.412212,-1083.236,26.609053|-49.73248,-1077.3169,26.868116|-65.04671,-1089.0259,26.718018|-58.469433,-1064.3983,27.550032|-47.47563,-1038.874,28.398458|-35.304276,-1001.1455,29.17849|110.33075,-936.3817,29.756157|123.69729,-903.0019,30.132883|196.98935,-779.675,32.073597|205.56773,-747.1918,33.627956|219.41344,-712.22076,34.63979|239.49734,-668.43384,37.30331|251.23871,-618.1938,41.215866|287.35965,-593.48846,42.48913|271.54782,-558.6092,42.665012|245.85095,-579.11664,42.623905|215.17917,-646.88855,38.2152|193.62245,-705.68445,34.46568|189.74411,-724.28015,33.58126|163.41188,-785.43,30.793697|125.24393,-820.4837,31.256044|95.706024,-787.84216,30.897526|-234.14018,-1451.5629,30.640652|430.66727,-951.45624,28.505169|-210.27583,-1470.0876,30.68251|454.08194,-962.1255,27.856955|477.77164,-958.0971,26.872763|-192.20874,-1493.3302,31.204483|462.20557,-950.6972,27.359266|-166.74825,-1507.7908,32.48453|407.93832,-935.4415,28.64887|413.31137,-911.1978,28.7621|-140.94179,-1535.059,33.62421|407.86554,-894.8661,28.747055|408.694,-857.1184,28.675537|-108.480064,-1547.6704,33.136555|394.76398,-846.4892,28.684237|374.10068,-843.47546,28.601175|350.4054,-839.11566,28.633257|-90.813675,-1574.8425,31.105179|321.34503,-836.5141,28.638775|-59.997196,-1589.0249,28.928232|321.2284,-815.83105,28.619087|-49.462646,-1606.2358,28.470226|-56.3749,-1626.2446,28.579128|331.43234,-784.9671,28.621136|-65.05416,-1641.882,28.687506|-92.87201,-1661.2533,28.642258|342.0586,-752.2495,28.615929|-108.73983,-1679.0758,28.656313|344.58313,-726.87976,28.585205|-118.29737,-1704.1661,28.735405|354.0173,-689.5965,28.67544|-132.10117,-1718.8413,29.446087|383.66095,-685.90466,28.598986|-147.30553,-1754.9402,29.43978|408.55032,-681.1347,28.672234|-170.06966,-1771.2476,29.184355|-194.24849,-1779.5729,29.092113|440.8199,-681.04456,28.054565|-228.03423,-1796.727,28.879328|438.77316,-667.6487,28.226847|-244.17578,-1816.5806,29.042007|404.67862,-668.42584,28.635735|-282.82327,-1818.6418,26.198742|346.81787,-671.62115,28.691666|337.41718,-686.30615,28.664495|-339.7586,-1826.6593,22.909313|324.31223,-727.63654,28.691744|-369.37317,-1814.8258,21.56872|310.80594,-742.58356,28.655075|-388.7261,-1795.9536,20.82252|308.17288,-766.30945,28.627457|-394.6323,-1771.3624,20.190496|293.92288,-792.9895,28.655272|-410.01093,-1759.0072,19.82905|-434.39932,-1765.819,19.7926|-470.05878,-1778.5203,20.222488|-494.469,-1774.2556,20.370127|-531.9875,-1763.0386,20.871838|-554.1718,-1743.2072,21.292461|-577.8294,-1731.2339,21.977228|-610.9359,-1718.3572,23.079895|-621.8183,-1698.4126,23.775566|-630.8703,-1680.0084,24.239502|-650.6739,-1672.6815,24.657885|-671.95074,-1662.1578,24.312897|-669.4442,-1647.0413,24.174833|-648.39777,-1646.0836,24.562393|-688.61316,-1633.6831,23.477741|-705.7972,-1615.5255,22.386673|-756.0283,-1624.5127,25.048182|-769.8567,-1656.0896,26.591093|-765.51685,-1688.6302,28.471056|-771.2976,-1736.0752,28.4517|-760.8794,-1766.3486,28.636314|-744.7842,-1743.8347,28.620193|-685.3167,-1581.1886,18.138254|-656.4039,-1534.9701,12.749533|-643.93335,-1477.4034,9.836438|-642.12915,-1427.7335,9.884139|-645.33124,-1381.1045,9.990845|-643.7086,-1348.2717,9.953008|283.77518,-820.9924,28.657192|-607.0284,-1279.2125,9.974271|-569.3015,-1233.3348,14.39602|-546.84985,-1182.5532,17.931877|-539.0771,-1138.9723,19.727213|-495.92755,-1103.4982,23.959023|-468.71344,-1113.0837,27.179676|-395.10248,-1124.3171,28.77199|114.398346,-791.35504,30.783752|-338.8257,-1145.8793,26.170294|77.05187,-764.5723,30.996141|-299.5805,-1145.3716,22.621542|41.223694,-760.6119,30.964363|27.324512,-775.30194,30.94499|-281.3094,-1162.7247,22.426645|-288.03134,-1188.9117,22.786203|12.104142,-826.1539,30.380222|-277.59262,-1242.3179,27.34229|-7.6048946,-871.6629,29.59423|-279.08405,-1264.3193,29.12506|-21.717295,-905.19977,28.983963|-267.9245,-1299.0994,30.632519|-47.321728,-917.0182,28.691347|-245.0177,-1308.9363,30.627108|-224.94691,-1302.5897,30.63659|-75.30201,-919.11536,28.526148|-204.79918,-1304.0748,30.61625|-94.27963,-922.97156,28.694746|-190.92299,-1299.1276,30.63971|-176.58215,-1304.096,30.637272|-104.38248,-940.6377,28.566217|-164.49234,-1306.9866,30.60685|-163.46962,-1295.4949,30.478626|-187.28526,-1281.7489,30.609472|-187.96045,-1266.2882,30.62553|-226.71867,-1255.3065,30.634083|-290.6752,-1290.9677,30.342833|-292.29865,-1311.3275,30.494215|-289.69205,-1357.9457,30.491333|-294.0013,-1380.6842,30.658613|-288.5143,-1408.9735,30.544592|-305.58386,-1443.2048,30.537615|-76.001465,-952.19977,28.583431|-290.83035,-1476.7461,30.148497|-45.99783,-963.65875,28.756966|-297.07123,-1516.7664,27.742367|-49.84989,-989.3765,28.625954|-310.8906,-1529.1453,26.88447|-69.72233,-1017.7611,27.989529|-325.48044,-1520.9703,26.885025|-87.29529,-1050.9834,26.958721|-106.80896,-1095.0692,25.372757|-121.25208,-1117.2098,25.085468|-271.65067,-1457.0316,30.679966|-325.17664,-1577.5214,22.100803|55.21583,-1182.381,28.706427|69.43885,-1239.6849,28.685705|57.37148,-1273.4755,28.676086|63.210796,-1311.4199,28.629292|86.94267,-1339.8027,28.686218|128.5151,-1358.9933,28.611065|165.48079,-1367.9756,28.67666|199.64418,-1346.7869,28.584137|223.92998,-1297.0144,28.666393|220.3431,-1238.8422,28.685335|-205.2493,-1426.3698,30.692848|226.42468,-1200.0885,28.672049|-155.73563,-1408.2561,29.988707|-151.65385,-1415.5857,30.182556|-110.43351,-1451.1185,32.71813|-99.06803,-1463.632,32.531445|226.142,-1170.0687,28.620398|-72.73876,-1470.9318,31.453665|217.255,-1135.417,28.633429|-47.448685,-1468.0835,31.314964|218.65565,-1094.9586,28.67916|-6.032837,-1463.5255,29.888355|21.374222,-1475.7406,29.63006|37.300266,-1489.5197,28.552086|42.081585,-1509.9753,28.585833|7.2800283,-1551.1958,28.574175|-12.869937,-1576.7917,28.623175|-82.24653,-1545.7034,32.14843|-149.52844,-1489.65,32.68331|354.48428,-1058.4176,28.72463|392.83066,-1050.7568,28.678055|396.0344,-1089.3953,28.74608|389.2109,-1111.1296,28.706835|376.399,-1123.4727,28.756565|368.3194,-1129.3179,28.752783|332.07794,-1124.0836,28.76477|312.33942,-1127.3406,28.784382');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `globalitems`
--

CREATE TABLE `globalitems` (
  `id` int(11) NOT NULL DEFAULT 1,
  `json` longtext NOT NULL DEFAULT '[]'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `globalitems`
--

INSERT INTO `globalitems` (`id`, `json`) VALUES
(1, '[]');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `groups`
--

CREATE TABLE `groups` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL DEFAULT 'n/A',
  `created` int(11) NOT NULL DEFAULT 1609462861,
  `banknumber` varchar(20) NOT NULL DEFAULT 'n/A',
  `leader` int(11) NOT NULL,
  `members` int(5) NOT NULL,
  `status` int(1) NOT NULL,
  `licenses` varchar(20) NOT NULL DEFAULT '0|0|0|0|0|0|0|0|0',
  `provision` int(3) NOT NULL DEFAULT 0,
  `maxplusvehicles` int(3) NOT NULL DEFAULT 0,
  `service` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `groupsrangs`
--

CREATE TABLE `groupsrangs` (
  `id` int(11) NOT NULL,
  `groupsid` int(11) NOT NULL,
  `rang1` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang2` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang3` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang4` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang5` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang6` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang7` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang8` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang9` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang10` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang11` varchar(50) NOT NULL DEFAULT 'n/A',
  `rang12` varchar(50) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `groups_members`
--

CREATE TABLE `groups_members` (
  `id` int(11) NOT NULL,
  `groupsid` int(11) NOT NULL,
  `charid` int(11) NOT NULL,
  `rang` int(2) NOT NULL DEFAULT 1,
  `duty_time` int(4) NOT NULL DEFAULT 0,
  `payday` int(7) NOT NULL DEFAULT 0,
  `payday_day` int(4) NOT NULL DEFAULT 0,
  `payday_since` int(4) NOT NULL DEFAULT 0,
  `since` int(11) NOT NULL DEFAULT 1634294819
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `houseinteriors`
--

CREATE TABLE `houseinteriors` (
  `id` int(11) NOT NULL,
  `ipl` varchar(128) NOT NULL DEFAULT 'n/A',
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `posz` float NOT NULL,
  `classify` int(2) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `houseinteriors`
--

INSERT INTO `houseinteriors` (`id`, `ipl`, `posx`, `posy`, `posz`, `classify`) VALUES
(1, 'apa_v_mp_h_01_a', -786.866, 315.764, 217.639, 1),
(2, 'apa_v_mp_h_01_c', -786.956, 315.623, 187.914, 1),
(3, 'apa_v_mp_h_01_b', -774.013, 342.043, 196.686, 1),
(4, 'apa_v_mp_h_02_a', -787.075, 315.82, 217.639, 1),
(5, 'apa_v_mp_h_02_c', -786.819, 315.563, 187.913, 1),
(6, 'apa_v_mp_h_02_b', -774.138, 342.032, 196.686, 1),
(7, 'apa_v_mp_h_03_a', -786.625, 315.617, 217.639, 1),
(8, 'apa_v_mp_h_03_c', -786.958, 315.797, 187.913, 1),
(9, 'apa_v_mp_h_03_b', -774.022, 342.172, 196.686, 1),
(10, 'apa_v_mp_h_04_a', -787.09, 315.704, 217.638, 1),
(11, 'apa_v_mp_h_04_c', -787.016, 315.707, 187.913, 1),
(12, 'apa_v_mp_h_04_b', -773.898, 342.152, 196.686, 1),
(13, 'apa_v_mp_h_05_a', -786.989, 315.739, 217.639, 1),
(14, 'apa_v_mp_h_05_c', -786.881, 315.663, 187.914, 1),
(15, 'apa_v_mp_h_05_b', -774.068, 342.077, 196.686, 1),
(16, 'apa_v_mp_h_06_a', -787.142, 315.694, 217.638, 1),
(17, 'apa_v_mp_h_06_c', -787.096, 315.815, 187.913, 1),
(18, 'apa_v_mp_h_06_b', -773.955, 341.989, 196.686, 1),
(19, 'apa_v_mp_h_07_a', -787.029, 315.711, 217.639, 1),
(20, 'apa_v_mp_h_07_c', -787.057, 315.657, 187.913, 1),
(21, 'apa_v_mp_h_07_b', -774.011, 342.096, 196.686, 1),
(22, 'apa_v_mp_h_08_a', -786.947, 315.565, 217.638, 1),
(23, 'apa_v_mp_h_08_c', -786.976, 315.723, 187.913, 1),
(24, 'apa_v_mp_h_08_b', -774.035, 342.03, 196.686, 1),
(25, 'house_no_ipl_a', 265.978, -1006.97, -100.884, 0),
(26, 'house_no_ipl_b', -30.5808, -595.31, 80.0309, 1),
(27, 'house_no_ipl_c', -30.5808, -595.31, 80.0309, 1),
(28, 'house_no_ipl_d', -17.7251, -588.899, 90.1148, 2),
(29, 'house_no_ipl_e', -1451.65, -523.769, 56.929, 2),
(30, 'house_no_ipl_f', -1451.65, -523.769, 56.929, 2),
(31, 'house_no_ipl_g', -1451.65, -523.769, 56.929, 2),
(32, 'house_no_ipl_h', -1451.65, -523.769, 56.929, 2),
(33, 'house_no_ipl_i', -174.266, 497.384, 137.667, 1),
(34, 'house_no_ipl_j', -1451.65, -523.769, 56.929, 2),
(35, 'house_no_ipl_k', -1451.65, -523.769, 56.929, 2),
(36, 'house_no_ipl_l', -1451.65, -523.769, 56.929, 2),
(37, 'house_no_ipl_m', -1451.65, -523.769, 56.929, 2),
(38, 'house_no_ipl_n', -1451.65, -523.769, 56.929, 2),
(39, 'house_no_ipl_o', -1451.65, -523.769, 56.929, 2),
(40, 'house_no_ipl_p', -1451.65, -523.769, 56.929, 2),
(41, 'TrevorsMP', 1972.96, 3816.53, 33.4287, -1),
(42, 'TrevorsTrailerTidy', 1972.96, 3816.53, 33.4287, 0),
(43, 'vb_30_crimetape', -1151.27, -1519.9, 10.63, 0),
(44, 'lester_house', 1273.9, -1719.31, 54.7714, 0),
(45, 'v_janitor', -107.65, -8.30835, 70.5196, 0),
(46, 'mansion_no_ipl', 1396.42, 1141.85, 114.334, 3),
(47, 'flat_no_ipl', 346.321, -1012.97, -99.1963, 0),
(48, 'franklin_hoo_house', -14.3176, -1439.99, 31.1015, 0),
(49, 'franklin_mansion_no_ipl', 7.69007, 538.066, 176.028, 3),
(50, 'farmint', 2436.88, 4974.92, 46.8106, 1),
(51, 'motel_room_no_ipl', 151.191, -1007.73, -99, 0),
(52, 'imp_dt1_02_modgarage', -141.599, -590.626, 166.505, 4),
(53, 'tuningdlc_interior1', -1357.87, 163.525, -99.7777, 4),
(54, 'tuningdlc_interior2', -1357.87, 163.525, -99.7777, 4),
(55, 'tuningdlc_interior3', -1357.87, 163.525, -99.7777, 4),
(56, 'tuningdlc_interior4', -1357.87, 163.525, -99.7777, 4),
(57, 'tuningdlc_interior5', -1357.87, 163.525, -99.7777, 4),
(58, 'tuningdlc_interior6', -1357.87, 163.525, -99.7777, 4),
(59, 'tuningdlc_interior7', -1357.87, 163.525, -99.7777, 4),
(60, 'tuningdlc_interior8', -1357.87, 163.525, -99.7777, 4),
(61, 'tuningdlc_interior9', -1357.87, 163.525, -99.7777, 4),
(62, 'no_ipl', 482.429, -2623.81, -49.064, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `houses`
--

CREATE TABLE `houses` (
  `id` int(11) NOT NULL,
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `posz` float NOT NULL,
  `dimension` int(11) NOT NULL DEFAULT 0,
  `price` int(11) NOT NULL DEFAULT 0,
  `interior` int(6) NOT NULL DEFAULT 0,
  `owner` varchar(35) NOT NULL DEFAULT 'n/A',
  `status` int(2) NOT NULL DEFAULT 0,
  `tenants` int(5) NOT NULL DEFAULT 0,
  `rental` int(11) NOT NULL DEFAULT 0,
  `locked` int(1) NOT NULL DEFAULT 0,
  `noshield` int(1) NOT NULL DEFAULT 0,
  `streetname` varchar(64) NOT NULL DEFAULT 'n/A',
  `blip` int(4) NOT NULL DEFAULT 40,
  `housegroup` int(6) NOT NULL DEFAULT -1,
  `stock` int(4) NOT NULL DEFAULT 3500,
  `stockprice` int(3) NOT NULL DEFAULT 30,
  `classify` int(2) NOT NULL DEFAULT 0,
  `elec` int(3) NOT NULL DEFAULT 50
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `houses`
--

INSERT INTO `houses` (`id`, `posx`, `posy`, `posz`, `dimension`, `price`, `interior`, `owner`, `status`, `tenants`, `rental`, `locked`, `noshield`, `streetname`, `blip`, `housegroup`, `stock`, `stockprice`, `classify`, `elec`) VALUES
(1, -3089.62, 221.386, 14.1218, 0, 62800, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 51),
(2, -328.986, -159.626, 39.0154, 0, 0, 0, 'ACLS', 5, 0, 0, 0, 0, 'Carcer Way', 40, -1, 0, 30, 0, 50),
(3, -3017.1, 746.693, 27.7814, 0, 414100, 7, 'n/A', 0, 0, 0, 0, 0, 'Great Ocean Hwy', 40, -1, 0, 30, 2, 51),
(4, -1861.31, 310.423, 89.1134, 0, 403200, 39, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(5, -3104.97, 246.876, 12.4929, 0, 63800, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 51),
(6, -3115.85, 294.435, 8.97212, 0, 52900, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 51),
(7, -3106.12, 312.27, 8.38104, 0, 56200, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(8, -3110.61, 335.418, 7.49335, 0, 57300, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(9, -3093.39, 349.246, 7.53873, 0, 174700, 33, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 1, 50),
(10, -3091.37, 379.304, 7.11193, 0, 59900, 45, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(11, -3081.1, 405.718, 6.96852, 0, 56700, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(12, -3059.47, 453.558, 6.355, 0, 64800, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(13, -3047.57, 483.323, 6.77965, 0, 60400, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(14, -3039.31, 492.761, 6.77264, 0, 164400, 24, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 1, 50),
(15, -3031.73, 525.056, 7.42135, 0, 58900, 48, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(16, -3036.59, 544.872, 7.50768, 0, 55600, 42, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(17, -3036.86, 559.235, 7.50768, 0, 52800, 42, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(18, -3029.59, 568.775, 7.82537, 0, 64300, 44, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(19, -3077.85, 658.982, 11.668, 0, 197900, 21, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 1, 50),
(20, -3107.78, 718.852, 20.6563, 0, 63100, 43, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(21, -3101.37, 743.772, 21.2848, 0, 57300, 42, 'n/A', 0, 0, 0, 0, 0, 'Ineseno Road', 40, -1, 0, 30, 0, 50),
(22, -2997.36, 722.928, 28.4969, 0, 195000, 16, 'n/A', 0, 0, 0, 0, 0, 'Great Ocean Hwy', 40, -1, 0, 30, 1, 50),
(23, -2994.61, 682.522, 25.0418, 0, 405500, 34, 'n/A', 0, 0, 0, 0, 0, 'Great Ocean Hwy', 40, -1, 0, 30, 2, 50),
(24, -2972.55, 642.388, 25.9881, 0, 416500, 40, 'n/A', 0, 0, 0, 0, 0, 'Great Ocean Hwy', 40, -1, 0, 30, 2, 50),
(25, -2977.16, 609.343, 20.246, 0, 184800, 3, 'n/A', 0, 0, 0, 0, 0, 'Great Ocean Hwy', 40, -1, 0, 30, 1, 50),
(26, -3225.36, 911.14, 13.9933, 0, 54600, 44, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(27, -3228.86, 927.488, 13.9698, 0, 56700, 47, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(28, -3232.4, 934.833, 13.7984, 0, 55400, 51, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(29, -3238.2, 952.651, 13.2404, 0, 54900, 48, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(30, -3251.2, 1027.34, 11.7577, 0, 63200, 51, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(31, -3254.67, 1064.07, 11.1462, 0, 62500, 51, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(32, -3232.71, 1068.1, 11.0337, 0, 61400, 47, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(33, -3231.85, 1081.57, 10.8089, 0, 25600, 44, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(34, -3228.4, 1092.63, 10.7645, 0, 25600, 51, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(35, -3229.15, 1100.73, 10.5787, 0, 52900, 42, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(36, -3209.87, 1144.7, 9.89541, 0, 57800, 45, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(37, -3205.36, 1152.08, 9.6622, 0, 58900, 25, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(38, -3200.11, 1165.41, 9.65435, 0, 55300, 48, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(39, -3205.73, 1186.02, 9.66467, 0, 52200, 48, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(40, -3193.62, 1208.87, 9.42523, 0, 53200, 51, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(41, -3194.17, 1230.1, 10.0483, 0, 55600, 48, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(42, -3187.01, 1273.92, 12.6624, 0, 62100, 48, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 0, 50),
(43, -3190.93, 1297.73, 19.0694, 0, 87400, 9, 'n/A', 0, 0, 0, 0, 0, 'Barbareno Rd', 40, -1, 0, 30, 1, 50),
(44, -468.5, -280.738, 35.5043, 0, 85400, 11, 'n/A', 1, 0, 0, 0, 0, 'Carcer Way', 40, -1, 0, 30, 1, 51),
(45, 793.599, -80.8561, 80.5975, 0, 811500, 49, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 3, 50),
(46, 1204.72, -557.779, 69.6207, 0, 63000, 45, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(47, 952.2, -252.073, 67.7644, 0, 55900, 45, 'n/A', 1, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 51),
(48, 1201, -575.655, 69.139, 0, 52600, 43, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(49, 930.954, -245.364, 69.0027, 0, 59900, 43, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(50, 920.906, -238.481, 70.168, 0, 58500, 25, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(51, 1203.63, -598.52, 68.0635, 0, 63100, 47, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(52, 1207.01, -620.338, 66.4386, 0, 54400, 25, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(53, 880.289, -205.729, 71.9765, 0, 51800, 44, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(54, 1229.16, -725.5, 60.798, 0, 62700, 51, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(55, 839.843, -181.785, 74.188, 0, 51600, 43, 'n/A', 0, 0, 0, 0, 0, 'York St', 40, -1, 0, 30, 0, 50),
(56, 808.6, -163.996, 75.8817, 0, 63800, 48, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(57, 798.728, -159.042, 74.8924, 0, 50500, 42, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(58, 773.675, -150.292, 75.6261, 0, 52900, 43, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(59, 1222.74, -696.926, 60.8059, 0, 57900, 51, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(60, 1221.4, -668.979, 63.4949, 0, 53400, 47, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(61, 1264.78, -702.757, 64.8965, 0, 62500, 43, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(62, 1083.47, -351.416, 67.1039, 0, 59000, 42, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(63, 1270.93, -683.36, 66.0316, 0, 56900, 42, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(64, 1124.13, -345.663, 67.101, 0, 52100, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(65, 1265.37, -648.229, 67.9215, 0, 55300, 43, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(66, 1250.91, -620.944, 69.5521, 0, 52100, 51, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(67, 1060.84, -378.347, 68.2312, 0, 53900, 51, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(68, 1029.27, -408.861, 65.9493, 0, 63500, 44, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(69, 1240.52, -601.667, 69.7828, 0, 51100, 43, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(70, 1011.59, -422.626, 64.9527, 0, 58900, 51, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(71, 1241.6, -566.225, 69.6574, 0, 55300, 25, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(72, 987.917, -433.554, 63.8907, 0, 50500, 42, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(73, 1250.95, -515.452, 69.349, 0, 63100, 43, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(74, 967.955, -452.779, 62.4028, 0, 54800, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(75, 1251.5, -494.187, 69.9069, 0, 63700, 45, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(76, 943.968, -463.648, 61.3958, 0, 52500, 51, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(77, 1259.72, -479.939, 70.1888, 0, 58200, 45, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(78, 921.994, -478.187, 61.0836, 0, 55700, 42, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(79, 1265.79, -457.945, 70.5157, 0, 60200, 48, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(80, 906.757, -490.126, 59.4362, 0, 62700, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(81, 1262.72, -429.686, 70.0026, 0, 55100, 47, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(82, 879.402, -498.827, 57.8759, 0, 50800, 48, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(83, 862.482, -509.71, 57.329, 0, 64600, 48, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(84, 850.742, -532.579, 57.9253, 0, 62000, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(85, 1303.11, -527.678, 71.4606, 0, 55200, 43, 'n/A', 2, 0, 0, 0, 1, 'Nikola Pl', 40, -1, 0, 30, 0, 51),
(86, 892.673, -540.794, 58.5065, 0, 64900, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(87, 1301.08, -573.979, 71.7321, 0, 60300, 42, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(88, 844.09, -563.405, 57.8336, 0, 63400, 25, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(89, 1348.31, -547.186, 73.8916, 0, 50200, 45, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(90, 1323.51, -582.7, 73.2463, 0, 62200, 47, 'n/A', 1, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(91, 1341.62, -597.502, 74.7007, 0, 60700, 51, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(92, 861.678, -583.089, 58.1565, 0, 60600, 47, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(93, 1367.25, -606.276, 74.7108, 0, 56700, 25, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(94, 1385.97, -593.296, 74.4854, 0, 53800, 51, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(95, 887.506, -607.404, 58.2178, 0, 51800, 42, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(96, 1388.9, -569.525, 74.4965, 0, 57100, 42, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(97, 1373.17, -555.799, 74.6856, 0, 61100, 42, 'n/A', 0, 0, 0, 0, 0, 'Nikola Pl', 40, -1, 0, 30, 0, 50),
(98, 903.65, -615.844, 58.4533, 0, 182300, 6, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 1, 50),
(99, 929.146, -639.234, 58.2424, 0, 59400, 42, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(100, 943.623, -653.585, 58.4287, 0, 63800, 42, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(101, 1090.15, -484.377, 65.6594, 0, 54500, 25, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(102, 960.477, -669.565, 58.4496, 0, 63600, 25, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(103, 1046.23, -497.805, 64.0794, 0, 58800, 25, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(104, 970.929, -700.932, 58.482, 0, 53200, 51, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(105, 1051.42, -470.454, 64.0816, 0, 55900, 25, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(106, 979.617, -715.755, 58.0236, 0, 61500, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(107, 1098.56, -464.663, 67.3193, 0, 58400, 47, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(108, 997.179, -729.308, 57.8157, 0, 63200, 48, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(109, 1099.94, -450.789, 67.7893, 0, 50200, 25, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(110, 1056.23, -448.815, 66.2574, 0, 63300, 43, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(111, 1099.43, -438.485, 67.7859, 0, 64300, 48, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(112, 1060.42, -777.886, 58.2627, 0, 803700, 46, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 3, 50),
(113, 1100.86, -411.39, 67.5553, 0, 60100, 48, 'n/A', 0, 0, 0, 0, 0, 'Bridge St', 40, -1, 0, 30, 0, 50),
(114, 1070.47, -780.486, 58.3274, 0, 63700, 45, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(115, 1105.42, -778.882, 58.2627, 0, 51800, 25, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(116, 1009.99, -572.446, 60.5944, 0, 53600, 45, 'n/A', 0, 0, 0, 0, 0, 'Mirror Pl', 40, -1, 0, 30, 0, 50),
(117, 999.73, -593.961, 59.6324, 0, 52100, 45, 'n/A', 0, 0, 0, 0, 0, 'Mirror Pl', 40, -1, 0, 30, 0, 50),
(118, 1123.33, -784.686, 57.6551, 0, 807900, 46, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 3, 50),
(119, 980.032, -627.458, 59.2358, 0, 179100, 20, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 1, 50),
(120, 920.272, -570.612, 58.3664, 0, 61600, 48, 'n/A', 0, 0, 0, 0, 0, 'Nikola Ave', 40, -1, 0, 30, 0, 50),
(121, 956.495, -547.363, 59.3648, 0, 56500, 44, 'n/A', 0, 0, 0, 0, 0, 'Nikola Ave', 40, -1, 0, 30, 0, 50),
(122, 988.235, -526.517, 60.4761, 0, 63600, 42, 'n/A', 0, 0, 0, 0, 0, 'Nikola Ave', 40, -1, 0, 30, 0, 50),
(123, 1005.98, -511.314, 60.8333, 0, 58400, 25, 'n/A', 0, 0, 0, 0, 0, 'Nikola Ave', 40, -1, 0, 30, 0, 50),
(124, 1014.42, -469.177, 64.5044, 0, 61400, 47, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(125, 970.204, -502.398, 62.1409, 0, 52400, 25, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(126, 946.066, -518.61, 60.6353, 0, 64200, 44, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(127, 924.174, -525.598, 59.8096, 0, 60100, 47, 'n/A', 0, 0, 0, 0, 0, 'West Mirror Drive', 40, -1, 0, 30, 0, 50),
(128, 723, -1069.55, 23.0624, 0, 800200, 46, 'n/A', 1, 0, 0, 0, 0, 'Olympic Fwy', 40, -1, 0, 30, 3, 51),
(129, 1208.93, -455.126, 66.8499, 0, 61600, 48, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(130, 1211.69, -445.06, 66.9566, 0, 65000, 48, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(131, 1157.15, -453.665, 66.9844, 0, 50300, 44, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(132, 1241.58, -416.979, 71.5785, 0, 53900, 51, 'n/A', 0, 0, 0, 0, 0, 'East Mirror Dr', 40, -1, 0, 30, 0, 50),
(133, 566.305, -1777.95, 29.3513, 0, 53500, 42, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(134, 559.965, -1777.12, 33.4427, 0, 53400, 47, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(135, 1169.08, -291.149, 69.0218, 0, 57500, 43, 'n/A', 0, 0, 0, 0, 0, 'Mirror Park Blvd', 40, -1, 0, 30, 0, 50),
(136, 550.229, -1770.6, 33.4427, 0, 51200, 43, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(137, 552.822, -1765.46, 33.4427, 0, 57500, 51, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(138, 556.039, -1758.83, 33.4427, 0, 58900, 43, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(139, 559.661, -1750.89, 33.4426, 0, 51300, 48, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(140, 561.741, -1747.38, 33.4426, 0, 55900, 48, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(141, 561.57, -1751.91, 29.2799, 0, 55400, 47, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(142, 558.104, -1759.76, 29.3138, 0, 56200, 44, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(143, 555.02, -1766.59, 29.3113, 0, 57800, 42, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(144, 552.47, -1771.6, 29.3119, 0, 62100, 43, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(145, 550.677, -1775.62, 29.3117, 0, 51700, 48, 'n/A', 0, 0, 0, 0, 0, 'Little Bighorn Ave', 40, -1, 0, 30, 0, 50),
(146, 500.532, -1697.36, 29.7891, 0, 61100, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(147, 378.777, 227.8, 103.041, 0, 804900, 49, 'n/A', 0, 0, 0, 0, 0, 'Power St', 40, -1, 0, 30, 3, 50),
(148, 489.794, -1714.11, 29.7059, 0, 53000, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(149, 479.551, -1735.96, 29.151, 0, 58500, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(150, 474.677, -1757.73, 29.0908, 0, 63100, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(151, 277.793, 347.992, 105.507, 0, 60100, 44, 'n/A', 0, 0, 0, 0, 0, 'Clinton Ave', 40, -1, 0, 30, 0, 50),
(152, 472.085, -1775.21, 29.0704, 0, 53600, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(153, 514.001, -1780.89, 28.914, 0, 50100, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(154, 512.506, -1790.69, 28.9166, 0, 56200, 42, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(155, 223.092, 373.268, 106.383, 0, 62000, 43, 'n/A', 0, 0, 0, 0, 0, 'Clinton Ave', 40, -1, 0, 30, 0, 50),
(156, 500.606, -1813.07, 28.8912, 0, 53300, 44, 'n/A', 1, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(157, 199.85, 336.359, 105.568, 0, 56500, 48, 'n/A', 0, 0, 0, 0, 0, 'Clinton Ave', 40, -1, 0, 30, 0, 50),
(158, 495.487, -1823.06, 28.8697, 0, 58900, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(159, 188.638, 348.41, 107.543, 0, 802100, 46, 'n/A', 0, 0, 0, 0, 0, 'Clinton Ave', 40, -1, 0, 30, 3, 50),
(160, 138.864, 273.828, 109.974, 0, 804500, 49, 'n/A', 0, 0, 0, 0, 0, 'Vinewood Blvd', 40, -1, 0, 30, 3, 50),
(161, 405.647, -1751.37, 29.7103, 0, 54400, 47, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(162, 418.977, -1735.7, 29.6077, 0, 61900, 42, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(163, 431.046, -1725.62, 29.6014, 0, 64300, 25, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(164, 443.434, -1707.38, 29.7074, 0, 50500, 42, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(165, 135.186, 323.059, 116.634, 0, 57900, 47, 'n/A', 0, 0, 0, 0, 0, 'Clinton Ave', 40, -1, 0, 30, 0, 50),
(166, 465.087, -1672.78, 29.2916, 0, 807900, 49, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 3, 50),
(167, 440.316, -1830, 28.3619, 0, 59000, 47, 'n/A', 0, 0, 0, 0, 0, 'Macdonald St', 40, -1, 0, 30, 0, 50),
(168, 427.26, -1842.06, 28.4634, 0, 58600, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(169, -1898.79, 132.691, 81.9847, 0, 422200, 38, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(170, -1873.76, 201.506, 84.2945, 0, 424000, 28, 'n/A', 1, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(171, -1931.71, 162.818, 84.6526, 0, 411700, 30, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(172, -1961.08, 211.974, 86.8238, 0, 421400, 32, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(173, -1906.14, 252.402, 86.2511, 0, 401100, 40, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(174, -1970.27, 246.221, 87.8094, 0, 423900, 40, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(175, -1922.72, 298.13, 89.2872, 0, 414300, 31, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(176, -1995.18, 300.233, 91.9647, 0, 412900, 31, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(177, -1931.83, 362.299, 93.787, 0, 420800, 32, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(178, -2008.27, 367.51, 94.8142, 0, 417500, 34, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(179, -1940.48, 387.053, 96.5071, 0, 423000, 38, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(180, -1943.17, 449.504, 102.927, 0, 418400, 31, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(181, -2010.22, 445.392, 103.016, 0, 409400, 29, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(182, -2014.48, 499.797, 107.172, 0, 423100, 29, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(183, -1938.15, 551.346, 114.828, 0, 411700, 28, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(184, -1995, 590.757, 117.903, 0, 414600, 32, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(185, -1929.61, 595.27, 122.285, 0, 415200, 37, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(186, -1974.14, 630.559, 122.536, 0, 417500, 39, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(187, -986.65, -1199.35, 6.04768, 0, 50200, 44, 'n/A', 0, 0, 0, 0, 0, 'Prosperity St', 40, -1, 0, 30, 0, 50),
(188, -1896.81, 642.13, 130.209, 0, 418800, 40, 'n/A', 0, 0, 0, 0, 0, 'North Rockford Dr', 40, -1, 0, 30, 2, 50),
(189, -1024.53, -1139.63, 2.74856, 0, 64300, 45, 'n/A', 0, 0, 0, 0, 0, 'Invention Ct', 40, -1, 0, 30, 0, 50),
(190, -1034.86, -1146.93, 2.1586, 0, 56400, 44, 'n/A', 0, 0, 0, 0, 0, 'Invention Ct', 40, -1, 0, 30, 0, 50),
(191, -1068.19, -1163.25, 2.7434, 0, 55100, 47, 'n/A', 0, 0, 0, 0, 0, 'Invention Ct', 40, -1, 0, 30, 0, 50),
(192, -1114.01, -1193.6, 2.36176, 0, 50700, 47, 'n/A', 0, 0, 0, 0, 0, 'Invention Ct', 40, -1, 0, 30, 0, 50),
(193, -1980.2, -520.603, 11.894, 0, 419900, 28, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 2, 50),
(194, -1073.98, -1152.51, 2.15859, 0, 58000, 51, 'n/A', 0, 0, 0, 0, 0, 'Invention Ct', 40, -1, 0, 30, 0, 50),
(195, -1961.01, -513.39, 11.8324, 0, 192500, 17, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(196, -1957.7, -528.303, 12.1811, 0, 161300, 12, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(197, -1051.16, -1006.45, 6.41048, 0, 50900, 44, 'n/A', 0, 0, 0, 0, 0, 'Imagination Ct', 40, -1, 0, 30, 0, 50),
(198, -1022.4, -1022.95, 2.15036, 0, 58100, 45, 'n/A', 0, 0, 0, 0, 0, 'Imagination Ct', 40, -1, 0, 30, 0, 50),
(199, -1947.93, -531.958, 11.8284, 0, 186600, 16, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(200, -1023.05, -998.022, 2.15019, 0, 61900, 44, 'n/A', 0, 0, 0, 0, 0, 'Imagination Ct', 40, -1, 0, 30, 0, 50),
(201, -1947.03, -544.439, 11.8659, 0, 174000, 2, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(202, -1934.73, -536.697, 11.8318, 0, 176900, 10, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(203, -1919.98, -569.989, 11.9121, 0, 197900, 24, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(204, -1902.72, -563.071, 11.8163, 0, 174700, 20, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(205, -1890.33, -572.834, 11.8164, 0, 178800, 1, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(206, -1883.04, -578.68, 11.8191, 0, 163300, 11, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(207, -1873.77, -586.057, 11.866, 0, 169400, 24, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(208, -1864.7, -593.71, 11.8365, 0, 160600, 8, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(209, -1834.77, -617.931, 10.9715, 0, 181400, 4, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(210, -1827.9, -626.368, 11.0265, 0, 172500, 26, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(211, -1816.62, -636.494, 10.9388, 0, 156700, 20, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(212, -1808.01, -644.606, 10.93, 0, 169400, 27, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(213, -1803.46, -662.051, 10.7282, 0, 151900, 27, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(214, -1787.69, -661.175, 10.4784, 0, 150300, 14, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(215, -1780.92, -679.623, 10.652, 0, 180900, 5, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(216, -1770.36, -677.321, 10.3617, 0, 176700, 12, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(217, -1761.5, -686.292, 10.1457, 0, 186300, 21, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(218, -1752.93, -694.633, 10.1422, 0, 190300, 7, 'n/A', 0, 0, 0, 0, 0, 'Del Perro Fwy', 40, -1, 0, 30, 1, 50),
(219, -1743.98, -702.196, 10.1532, 0, 177500, 22, 'n/A', 0, 0, 0, 0, 0, 'Equality Way', 40, -1, 0, 30, 1, 50),
(220, -1807.98, 333.237, 89.5728, 0, 402900, 39, 'n/A', 0, 0, 0, 0, 0, 'Richman St', 40, -1, 0, 30, 2, 50),
(221, -1742.03, 364.947, 88.7283, 0, 407900, 39, 'n/A', 0, 0, 0, 0, 0, 'Richman St', 40, -1, 0, 30, 2, 51),
(222, -1673.18, 385.701, 89.3483, 0, 412000, 29, 'n/A', 0, 0, 0, 0, 0, 'Richman St', 40, -1, 0, 30, 2, 50),
(223, -1094.63, 427.4, 75.8777, 0, 404700, 30, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(224, -1051.89, 432.135, 77.0636, 0, 403100, 36, 'n/A', 0, 0, 0, 0, 0, 'Cockingend Dr', 40, -1, 0, 30, 2, 50),
(225, -1087.77, 479.463, 81.3207, 0, 409600, 31, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(226, -1122.62, 485.648, 82.1609, 0, 402200, 31, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(227, -1158.96, 481.575, 86.0938, 0, 418300, 32, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(228, -1174.55, 440.178, 86.8498, 0, 423400, 30, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(229, -1215.86, 458.115, 92.0541, 0, 406600, 31, 'n/A', 0, 0, 0, 0, 0, 'Mad Wayne Thunder Dr', 40, -1, 0, 30, 2, 50),
(230, -1217.49, 506.043, 95.6671, 0, 417300, 32, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(231, -1277.85, 497.149, 97.8904, 0, 801700, 49, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 3, 50),
(232, -1193.03, 564.037, 100.339, 0, 420400, 31, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(233, -1167.03, 568.674, 101.827, 0, 410900, 36, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(234, -1146.51, 545.875, 101.893, 0, 153100, 7, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 1, 50),
(235, -1125.43, 548.72, 102.573, 0, 418100, 30, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(236, -1107.42, 594.204, 104.455, 0, 424400, 35, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(237, -1090.47, 548.274, 103.633, 0, 410800, 35, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(238, -1022.62, 586.919, 103.425, 0, 414500, 40, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(239, -974.39, 582.026, 102.984, 0, 402500, 34, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(240, -958.07, 604.401, 105.438, 0, 420700, 31, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(241, -924.666, 561.669, 99.9549, 0, 172000, 3, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 1, 50),
(242, -904.446, 587.995, 101.189, 0, 416400, 32, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(243, -907.354, 545.32, 100.205, 0, 186600, 18, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 1, 50),
(244, -869.737, 551.512, 97.0215, 0, 409800, 38, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(245, -884.182, 518.057, 92.4428, 0, 419100, 28, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(246, -848.775, 508.956, 90.817, 0, 403800, 35, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(247, -866.605, 457.338, 88.2811, 0, 422900, 29, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(248, -843.232, 466.754, 87.5977, 0, 423400, 36, 'n/A', 0, 0, 0, 0, 0, 'South Mo Milton Dr', 40, -1, 0, 30, 2, 50),
(249, -824.83, 422.305, 92.1243, 0, 416500, 37, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(250, -762.242, 431.128, 100.196, 0, 423700, 37, 'n/A', 1, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(251, -784.276, 459.066, 100.179, 0, 170700, 26, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 1, 50),
(252, -718.073, 448.975, 106.909, 0, 403900, 39, 'n/A', 1, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(253, -721.471, 490.094, 109.385, 0, 404700, 37, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(254, -678.927, 511.756, 113.526, 0, 404900, 38, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(255, -667.268, 471.764, 114.137, 0, 406300, 30, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(256, -640.745, 520.016, 109.688, 0, 406000, 31, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 50),
(257, -622.899, 489.125, 108.865, 0, 171200, 7, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 1, 50),
(258, -595.289, 530.198, 107.755, 0, 409400, 35, 'n/A', 1, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 2, 51),
(259, -580.514, 492.061, 108.903, 0, 182200, 5, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 1, 50),
(260, -554.463, 540.989, 110.707, 0, 159500, 20, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 1, 50),
(261, -526.834, 517.198, 112.942, 0, 414900, 32, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(262, -520.243, 594.291, 120.837, 0, 404900, 38, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(263, -474.786, 585.895, 128.683, 0, 422300, 35, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(264, -522.888, 628.312, 137.974, 0, 409000, 38, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(265, -559.639, 664.055, 145.455, 0, 161000, 4, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 1, 50),
(266, -551.772, 686.559, 146.075, 0, 424200, 30, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(267, -606.156, 672.35, 151.62, 0, 405400, 31, 'n/A', 4, 0, 0, 0, 1, 'Normandy Dr', 40, -1, 0, 30, 2, 51),
(268, -662.086, 679.043, 153.911, 0, 186500, 9, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(269, -700.798, 647.706, 155.175, 0, 424900, 36, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 2, 50),
(270, -686.468, 596.683, 143.642, 0, 423000, 36, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 2, 50),
(271, -704.264, 588.916, 141.929, 0, 167600, 7, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(272, -733.229, 593.776, 142.474, 0, 154300, 24, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(273, -753.131, 620.618, 142.722, 0, 191000, 14, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(274, -765.73, 650.618, 145.694, 0, 412000, 40, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 2, 50),
(275, -819.578, 696.882, 148.11, 0, 194800, 21, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(276, -853.187, 695.819, 148.786, 0, 168700, 5, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(277, -885.099, 699.398, 151.271, 0, 178900, 10, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(278, -908.797, 693.991, 151.435, 0, 183900, 18, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(279, -931.471, 690.988, 153.466, 0, 154000, 7, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 1, 50),
(280, -1033.05, 685.962, 161.303, 0, 423100, 29, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 2, 50),
(281, -1019.41, 719.348, 163.996, 0, 63400, 51, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 0, 50),
(282, -1065.2, 727.168, 165.475, 0, 400700, 30, 'n/A', 0, 0, 0, 0, 0, 'Hillcrest Ave', 40, -1, 0, 30, 2, 50),
(283, -1056.02, 761.584, 167.317, 0, 196200, 23, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(284, -1067.64, 795.466, 166.959, 0, 405100, 29, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(285, -1100.59, 797.57, 167.256, 0, 403500, 32, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(286, -1117.97, 761.638, 164.289, 0, 160400, 13, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(287, -1130.33, 784.173, 163.887, 0, 409700, 34, 'n/A', 0, 0, 0, 0, 0, 'North Sheldon Ave', 40, -1, 0, 30, 2, 50),
(288, -1165.37, 727.124, 155.607, 0, 152900, 24, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(289, -1196.97, 693.327, 147.423, 0, 181500, 27, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(290, -1218.86, 665.339, 144.534, 0, 194000, 19, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(291, -1248.65, 643.169, 142.699, 0, 152100, 21, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(292, -1254.99, 666.762, 142.825, 0, 188200, 12, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(293, -1291.65, 650.257, 141.501, 0, 401000, 28, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(294, -1277.62, 629.945, 143.197, 0, 418200, 40, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(295, -1366.9, 610.889, 133.903, 0, 176000, 2, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(296, -1337.44, 606.129, 134.38, 0, 191800, 22, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(297, -1346.34, 560.63, 130.532, 0, 406600, 29, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(298, -1405.04, 561.593, 125.406, 0, 188900, 21, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(299, -1405.67, 526.868, 123.831, 0, 193300, 33, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(300, -1452.63, 545.599, 120.799, 0, 414900, 28, 'n/A', 0, 0, 0, 0, 0, 'Ace Jones Dr', 40, -1, 0, 30, 2, 50),
(301, -1454.06, 512.805, 117.628, 0, 198900, 6, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(302, -1500.42, 522.893, 118.272, 0, 420900, 28, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(303, -1496.14, 437.113, 112.498, 0, 410600, 36, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(304, -1540.15, 421.011, 110.014, 0, 419700, 30, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(305, -1413.59, 462.359, 109.209, 0, 177700, 24, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(306, -1371.63, 443.943, 105.856, 0, 401700, 30, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(307, -1343.14, 481.315, 102.762, 0, 409900, 40, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(308, -999.353, 816.691, 173.05, 0, 403100, 31, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(309, -972.624, 752.483, 176.381, 0, 185700, 33, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 0, 30, 0, 50),
(310, -997.945, 768.85, 171.417, 0, 151700, 17, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 0, 30, 0, 51),
(311, -962.696, 813.764, 177.566, 0, 422700, 37, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(312, -912.319, 777.268, 187.012, 0, 413500, 38, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(313, -921.114, 813.564, 184.336, 0, 170700, 18, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(314, -867.427, 785.087, 191.934, 0, 401400, 37, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(315, -824.211, 806.118, 202.783, 0, 416000, 36, 'n/A', 0, 0, 0, 0, 0, 'North Sheldon Ave', 40, -1, 0, 30, 2, 50),
(316, -747.212, 808.352, 215.03, 0, 402400, 32, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(317, -655.213, 803.253, 198.991, 0, 417800, 28, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(318, -587.112, 806.468, 191.248, 0, 407200, 34, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(319, -595.793, 780.751, 189.109, 0, 150100, 5, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 1, 50),
(320, -579.73, 733.377, 184.211, 0, 412300, 39, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(321, -663.986, 742.184, 174.284, 0, 421400, 35, 'n/A', 0, 0, 0, 0, 0, 'Normandy Dr', 40, -1, 0, 30, 2, 50),
(322, -597.018, 851.78, 211.448, 0, 408200, 32, 'n/A', 0, 0, 0, 0, 1, 'Milton Rd', 40, -1, 0, 30, 2, 51),
(323, -536.672, 818.564, 197.51, 0, 401700, 32, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(324, -494.39, 795.943, 184.344, 0, 420800, 29, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(325, -495.579, 738.804, 163.031, 0, 411600, 32, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(326, -533.376, 709.196, 153.114, 0, 415800, 36, 'n/A', 0, 0, 0, 0, 0, 'Milton Rd', 40, -1, 0, 30, 2, 50),
(327, -498.984, 683.273, 151.858, 0, 400200, 28, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(328, -476.733, 647.897, 144.387, 0, 409200, 29, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(329, -445.714, 685.823, 152.951, 0, 416500, 40, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(330, -400.139, 665.247, 163.83, 0, 422100, 40, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(331, -353.14, 668.115, 169.073, 0, 421900, 28, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(332, -340.003, 625.703, 171.357, 0, 420400, 37, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(333, -299.241, 635.242, 175.688, 0, 414000, 29, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(334, -293.598, 601.058, 181.575, 0, 407900, 30, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(335, -245.934, 621.875, 187.81, 0, 419000, 40, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(336, -232.759, 588.333, 190.536, 0, 422800, 40, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(337, -189.347, 617.97, 199.661, 0, 409500, 38, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(338, -185.332, 591.214, 197.823, 0, 423500, 35, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(339, -126.495, 588.399, 204.71, 0, 407200, 30, 'n/A', 0, 0, 0, 0, 0, 'Kimble Hill Dr', 40, -1, 0, 30, 2, 50),
(340, -167.949, 916.237, 235.656, 0, 2350000, 49, 'n/A', 1, 0, 0, 0, 0, 'Lake Vinewood Est', 40, -1, 0, 30, 3, 50),
(341, -85.9438, 834.487, 235.92, 0, 2650000, 49, 'n/A', 0, 0, 0, 0, 0, 'Lake Vinewood Est', 40, -1, 0, 30, 3, 50),
(342, -111.545, 999.971, 235.758, 0, 3250000, 46, 'n/A', 0, 0, 0, 0, 0, 'Lake Vinewood Est', 40, -1, 0, 30, 3, 51),
(343, -425.797, 535.907, 122.177, 0, 421500, 35, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(344, -406.497, 567.133, 124.604, 0, 400600, 30, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(345, -386.77, 504.581, 120.413, 0, 417800, 35, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(346, -377.993, 548.157, 123.851, 0, 411500, 31, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(347, -348.91, 514.838, 120.648, 0, 413700, 37, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(348, -355.504, 469.906, 112.491, 0, 409300, 29, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(349, -312.262, 474.714, 111.832, 0, 405700, 30, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(350, -305.158, 431.714, 110.309, 0, 410700, 40, 'n/A', 0, 0, 0, 0, 0, 'Cox Way', 40, -1, 0, 30, 2, 50),
(351, -340.364, 424.278, 111.049, 0, 424500, 39, 'n/A', 0, 0, 0, 0, 0, 'Cox Way', 40, -1, 0, 30, 2, 50),
(352, -400.245, 427.29, 112.341, 0, 415000, 30, 'n/A', 0, 0, 0, 0, 0, 'Cox Way', 40, -1, 0, 30, 2, 50),
(353, -451.266, 395.496, 104.774, 0, 405900, 35, 'n/A', 0, 0, 0, 0, 0, 'Cox Way', 40, -1, 0, 30, 2, 50),
(354, -477.098, 412.88, 103.122, 0, 155800, 1, 'n/A', 0, 0, 0, 0, 0, 'Cox Way', 40, -1, 0, 30, 1, 50),
(355, -444.401, 343.314, 105.549, 0, 178900, 21, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 1, 50),
(356, -469.196, 330.069, 104.747, 0, 155200, 21, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 1, 50),
(357, -409.44, 341.441, 108.907, 0, 412000, 36, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(358, -371.933, 343.618, 109.944, 0, 158600, 26, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 1, 50),
(359, -328.218, 369.963, 110.011, 0, 181900, 26, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 1, 50),
(360, -297.929, 379.801, 112.095, 0, 419300, 29, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(361, -239.805, 381.314, 112.428, 0, 417100, 32, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(362, -214.273, 400.084, 111.108, 0, 412300, 28, 'n/A', 0, 0, 0, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(363, -166.54, 424.445, 111.806, 0, 412200, 29, 'n/A', 3, 0, 1, 0, 0, 'Didion Dr', 40, -1, 0, 30, 2, 50),
(364, -225.248, 477.173, 128.427, 0, 400700, 40, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(365, -230.529, 488.533, 128.767, 0, 406800, 36, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(366, -174.927, 502.431, 137.42, 0, 199400, 2, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 1, 50),
(367, -110.116, 502.044, 143.478, 0, 406300, 30, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(368, -66.6821, 490.535, 144.69, 0, 409400, 31, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(369, -7.65226, 468.351, 145.87, 0, 415200, 32, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(370, 57.7577, 450.085, 147.031, 0, 417200, 39, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(371, 43.0474, 468.563, 148.096, 0, 415500, 30, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(372, 80.207, 485.87, 148.202, 0, 419600, 28, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(373, 107.088, 466.98, 147.552, 0, 412900, 39, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(374, 119.582, 494.297, 147.343, 0, 423500, 34, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(375, 149.331, 475.003, 142.504, 0, 416100, 35, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 2, 50),
(376, 223.767, 513.826, 140.767, 0, 173400, 8, 'n/A', 0, 0, 0, 0, 0, 'Wild Oats Dr', 40, -1, 0, 30, 1, 50),
(377, 45.719, 556.15, 180.082, 0, 418600, 34, 'n/A', 0, 0, 0, 0, 0, 'Whispymound Dr', 40, -1, 0, 30, 2, 50),
(378, 84.9658, 561.859, 182.771, 0, 420900, 34, 'n/A', 0, 0, 0, 0, 0, 'Whispymound Dr', 40, -1, 0, 30, 2, 50),
(379, -1606.66, -432.358, 40.4315, 0, 197700, 3, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(380, -1597.81, -421.827, 41.4055, 0, 155100, 22, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(381, -1585.86, -400.362, 42.3832, 0, 191800, 18, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 3500, 30, 0, 50),
(382, -1642.39, -412.198, 42.0749, 0, 197700, 4, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 0, 30, 0, 51),
(383, -1667.46, -441.383, 40.3564, 0, 190300, 4, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 0, 30, 0, 51),
(384, -1596.66, -352.434, 45.9764, 0, 165600, 4, 'n/A', 0, 0, 0, 0, 0, 'n/A', 40, -1, 0, 30, 0, 51),
(385, -20.9179, -1390.71, 29.3726, 0, 418200, 0, 'n/A', 0, 0, 0, 0, 0, 'Innocence Blvd', 40, -1, 0, 30, 0, 50),
(386, 269.85, -1917.08, 26.1803, 0, 63400, 45, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(387, -416.628, -186.84, 37.4527, 0, 53400, 47, 'n/A', 0, 0, 0, 0, 0, 'Abe Milton Pkwy', 40, -1, 0, 30, 0, 50),
(388, -1161.37, -1099.68, 2.20682, 0, 197800, 7, 'n/A', 1, 0, 0, 0, 0, 'Imagination Ct', 40, -1, 0, 30, 1, 51),
(389, -1271.71, -1200.75, 5.36625, 0, 177800, 12, 'n/A', 0, 0, 0, 0, 0, 'Cortes St', 40, -1, 0, 30, 1, 50),
(390, -1249.16, -1426.06, 4.32292, 0, 812900, 46, 'n/A', 0, 0, 0, 0, 0, 'Palomino Ave', 40, -1, 0, 30, 3, 50),
(391, -1109.27, -1482.31, 4.9243, 0, 162600, 9, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 1, 50),
(392, -1117.56, -1488.23, 4.73497, 0, 190800, 10, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 1, 50),
(393, -1130.35, -1496.34, 4.42908, 0, 152400, 27, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 1, 50),
(394, -84.2181, -1622.39, 31.4774, 0, 54200, 45, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(395, -80.635, -1608.46, 31.4822, 0, 62600, 25, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(396, -97.4392, -1612.67, 32.3121, 0, 56800, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(397, -93.3188, -1607.76, 32.3123, 0, 51100, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(398, -109.123, -1628.9, 32.9078, 0, 63600, 47, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(399, -105.339, -1632.42, 32.9064, 0, 64600, 44, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(400, -97.3872, -1638.99, 32.1034, 0, 53000, 45, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(401, -89.9595, -1629.89, 31.5062, 0, 63800, 48, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(402, -109.351, -1628.79, 36.289, 0, 64100, 48, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(403, -105.135, -1632.15, 36.2891, 0, 56800, 47, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(404, -89.7785, -1629.82, 34.6892, 0, 54700, 48, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(405, -83.9597, -1622.74, 34.6892, 0, 61000, 51, 'n/A', 0, 0, 0, 0, 0, 'Strawberry Ave', 40, -1, 0, 30, 0, 50),
(406, -80.5097, -1608.36, 34.6891, 0, 53400, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(407, -88.1338, -1601.61, 35.4892, 0, 57200, 44, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(408, -93.3931, -1607.54, 35.4892, 0, 56000, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 51),
(409, -97.3065, -1612.75, 35.4892, 0, 58300, 47, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(410, -119.081, -1585.87, 34.213, 0, 61800, 47, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(411, -120.382, -1574.88, 34.1767, 0, 58200, 45, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(412, -134.226, -1580.41, 34.2081, 0, 63500, 45, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(413, -139.992, -1587.54, 34.2444, 0, 51200, 44, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(414, -147.64, -1596.56, 34.8313, 0, 58500, 44, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(415, -140.201, -1599.4, 34.8314, 0, 53500, 47, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(416, -123.45, -1591.03, 34.2079, 0, 55100, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(417, -140.148, -1598.94, 38.2126, 0, 57000, 47, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(418, -139.921, -1587.66, 37.4078, 0, 60400, 48, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(419, -134.055, -1580.53, 37.4078, 0, 53800, 51, 'n/A', 0, 0, 0, 0, 0, 'Forum Dr', 40, -1, 0, 30, 0, 50),
(420, -120.083, -1574.88, 37.4078, 0, 59700, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(421, -114.207, -1579.68, 37.4078, 0, 50600, 45, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(422, -119.04, -1585.96, 37.4078, 0, 50700, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(423, -123.285, -1590.96, 37.4078, 0, 63300, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(424, 112.791, 6619.44, 31.8331, 0, 402500, 0, 'n/A', 0, 0, 0, 0, 0, 'Paleto Blvd', 40, -1, 0, 30, 0, 50),
(425, -913.656, 108.289, 55.5147, 0, 808100, 46, 'n/A', 0, 0, 0, 0, 0, 'Spanish Ave', 40, -1, 0, 30, 3, 50),
(426, -971.375, 122.224, 57.0486, 0, 807800, 49, 'n/A', 0, 0, 0, 0, 0, 'Steele Way', 40, -1, 0, 30, 3, 50),
(427, -997.982, 157.503, 62.2184, 0, 803400, 46, 'n/A', 0, 0, 0, 0, 0, 'Steele Way', 40, -1, 0, 30, 3, 50),
(428, -1038.36, 222.137, 64.3756, 0, 809900, 46, 'n/A', 0, 0, 0, 0, 0, 'Steele Way', 40, -1, 0, 30, 3, 50),
(429, -1116.8, 304.243, 66.5213, 0, 810900, 49, 'n/A', 1, 0, 0, 0, 0, 'West Eclipse Blvd', 40, -1, 0, 30, 3, 51),
(430, -1189.81, 291.625, 69.8925, 0, 804800, 46, 'n/A', 0, 0, 0, 0, 0, 'West Eclipse Blvd', 40, -1, 0, 30, 3, 50),
(431, -1200.49, 351.797, 71.2733, 0, 804200, 46, 'n/A', 0, 0, 0, 0, 0, 'Greenwich Pl', 40, -1, 0, 30, 3, 50),
(432, -1135.54, 376.443, 71.2998, 0, 814600, 49, 'n/A', 0, 0, 0, 0, 0, 'Picture Perfect Drive', 40, -1, 0, 30, 3, 50),
(433, -830.767, 115.103, 55.8565, 0, 805300, 49, 'n/A', 0, 0, 0, 0, 0, 'Edwood Way', 40, -1, 0, 30, 3, 50),
(434, -1114.89, -1577.44, 4.54293, 0, 188100, 10, 'n/A', 0, 0, 0, 0, 0, 'Melanoma St', 40, -1, 0, 30, 1, 50),
(435, -1112.27, -1578.41, 8.67953, 0, 179100, 15, 'n/A', 0, 0, 0, 0, 0, 'Melanoma St', 40, -1, 0, 30, 1, 50),
(436, -1114.17, -1579.57, 8.67953, 0, 157900, 22, 'n/A', 0, 0, 0, 0, 0, 'Melanoma St', 40, -1, 0, 30, 1, 50),
(437, 1245.02, -1626.42, 53.2821, 0, 61100, 47, 'n/A', 0, 0, 0, 0, 0, 'Fudge Ln', 40, -1, 0, 30, 0, 50),
(438, -2587.84, 1911.31, 167.499, 0, 813800, 46, 'n/A', 0, 0, 0, 0, 0, 'Buen Vino Rd', 40, -1, 0, 30, 3, 50),
(439, -1086.91, -1530.01, 4.69686, 0, 155500, 23, 'n/A', 0, 0, 0, 0, 0, 'Melanoma St', 40, -1, 0, 30, 1, 50),
(440, -1108.67, -1527.51, 6.77953, 0, 157800, 5, 'n/A', 0, 0, 0, 0, 0, 'Bay City Ave', 40, -1, 0, 30, 1, 50),
(441, -1078.28, -1524.49, 4.88259, 0, 199800, 1, 'n/A', 0, 0, 0, 0, 0, 'Melanoma St', 40, -1, 0, 30, 1, 50),
(442, -1069.9, -1514.7, 5.10728, 0, 195100, 15, 'n/A', 0, 0, 0, 0, 0, 'Bay City Ave', 40, -1, 0, 30, 1, 50),
(443, -1135.61, -1468.51, 4.61815, 0, 58400, 45, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 0, 50),
(444, -1141.95, -1461.68, 4.62511, 0, 53200, 48, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 0, 50),
(445, -1145.82, -1466.11, 4.50524, 0, 60400, 47, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(446, -1145.71, -1466.14, 7.6907, 0, 58200, 44, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(447, -1142.09, -1461.75, 7.69071, 0, 51300, 42, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 0, 50),
(448, -1137.27, -1466.68, 7.6907, 0, 51300, 43, 'n/A', 0, 0, 0, 0, 0, 'Goma St', 40, -1, 0, 30, 0, 50),
(449, -1269.44, -1296.06, 4.00394, 0, 51500, 45, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(450, -1271.11, -1297.25, 8.2859, 0, 62300, 51, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(451, -1271.77, -1295.29, 8.28589, 0, 53000, 47, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(452, -1255.49, -1331.33, 4.08075, 0, 164000, 4, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 1, 50),
(453, -1246.84, -1358.63, 7.82149, 0, 55400, 51, 'n/A', 0, 0, 0, 0, 0, 'Magellan Ave', 40, -1, 0, 30, 0, 50),
(454, 313.563, -2040.35, 20.9363, 0, 59900, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(455, 317.173, -2043.46, 20.9365, 0, 55200, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(456, 324.495, -2049.58, 20.9364, 0, 55600, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(457, 325.924, -2050.86, 20.9364, 0, 53600, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(458, 333.251, -2056.9, 20.9365, 0, 65000, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(459, 334.582, -2058.06, 20.9366, 0, 59200, 44, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(460, 341.953, -2064.19, 20.9365, 0, 54100, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 51),
(461, 345.67, -2067.29, 20.9364, 0, 55900, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(462, 356.639, -2074.64, 21.7445, 0, 64700, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50);
INSERT INTO `houses` (`id`, `posx`, `posy`, `posz`, `dimension`, `price`, `interior`, `owner`, `status`, `tenants`, `rental`, `locked`, `noshield`, `streetname`, `blip`, `housegroup`, `stock`, `stockprice`, `classify`, `elec`) VALUES
(463, 357.799, -2073.27, 21.7445, 0, 52500, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(464, 365.004, -2064.55, 21.7445, 0, 61600, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(465, 371.187, -2057.2, 21.7445, 0, 62400, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(466, 372.4, -2055.9, 21.7445, 0, 60300, 25, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(467, 364.279, -2045.73, 22.3543, 0, 54500, 48, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(468, 360.59, -2042.62, 22.3543, 0, 58300, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(469, 353.356, -2036.46, 22.3542, 0, 60700, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(470, 352.047, -2035.24, 22.3543, 0, 57800, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(471, 344.649, -2029.12, 22.3542, 0, 62600, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(472, 343.255, -2027.93, 22.3543, 0, 57500, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(473, 335.945, -2021.8, 22.3543, 0, 56400, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(474, 332.24, -2018.7, 22.3543, 0, 54500, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(475, 324.024, -1990.85, 24.1672, 0, 63600, 43, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(476, 325.216, -1989.51, 24.1672, 0, 51400, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(477, 331.35, -1982.19, 24.1672, 0, 65000, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(478, 334.402, -1978.38, 24.1672, 0, 64100, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(479, 342.367, -1981.87, 24.1736, 0, 57700, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(480, 338.627, -1992.43, 23.592, 0, 54500, 42, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(481, 336.291, -1995.62, 23.5866, 0, 53200, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(482, 332.573, -2001.69, 22.8734, 0, 64400, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(483, 346.35, -2014.4, 22.2473, 0, 56400, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(484, 354.498, -2021.63, 22.3114, 0, 61800, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(485, 357.506, -2024.18, 22.3109, 0, 54300, 43, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(486, 362.905, -2028.55, 22.2489, 0, 59000, 47, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(487, 365.944, -2031.14, 22.273, 0, 63000, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(488, 382.849, -2037.55, 23.4025, 0, 61500, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(489, 383.705, -2036.13, 23.4022, 0, 61700, 42, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(490, 388.418, -2025.73, 23.403, 0, 65000, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(491, 392.493, -2017.09, 23.403, 0, 53000, 25, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(492, 393.233, -2015.44, 23.403, 0, 59700, 47, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(493, 383.334, -2008, 23.8434, 0, 50500, 45, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(494, 376.872, -2005.4, 23.872, 0, 55200, 44, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(495, 373.145, -2004.08, 23.8743, 0, 64000, 44, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(496, 366.624, -2001.68, 23.8718, 0, 55300, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(497, 362.917, -2000.39, 23.897, 0, 59000, 43, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(498, 298.005, -2034.32, 19.8357, 0, 62600, 42, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(499, 294.076, -2044.85, 19.5044, 0, 61000, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(500, 291.929, -2048.16, 19.2811, 0, 55300, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(501, 287.659, -2053.76, 18.9755, 0, 53300, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(502, 295.065, -2067.31, 17.6495, 0, 62900, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(503, 302.249, -2075.99, 17.6899, 0, 56400, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(504, 303.631, -2079.82, 17.6536, 0, 60000, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(505, 305.957, -2086.29, 17.6073, 0, 63900, 47, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(506, 320.083, -2100.79, 18.2388, 0, 53900, 43, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(507, 321.693, -2100, 18.2441, 0, 50600, 42, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(508, 329.913, -2095.06, 18.244, 0, 51800, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(509, 334.124, -2092.76, 18.2442, 0, 52600, 43, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(510, 332.441, -2070.6, 20.9365, 0, 51300, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(511, 323.327, -2064.52, 20.3096, 0, 55700, 51, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(512, 320.303, -2061.87, 20.312, 0, 61800, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(513, 315.684, -2056.61, 20.9395, 0, 53600, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(514, 312.669, -2054, 20.9386, 0, 60100, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(515, 306.177, -2045.06, 20.9118, 0, 50100, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(516, 364.126, -1987.52, 24.2344, 0, 50200, 42, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(517, 374.772, -1991.41, 24.2347, 0, 61600, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(518, 383.682, -1994.7, 24.2342, 0, 60300, 48, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(519, 385.404, -1995.3, 24.2351, 0, 60600, 45, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(520, 405.162, -2018.49, 23.324, 0, 54000, 44, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(521, 402.352, -2024.65, 23.2418, 0, 58700, 25, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(522, 400.592, -2028.2, 23.243, 0, 64200, 47, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(523, 397.961, -2034.7, 23.0029, 0, 56800, 48, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(524, 396.265, -2038.28, 23.006, 0, 54300, 43, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(525, 393.388, -2044.64, 22.4802, 0, 65000, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(526, 383.316, -2061.99, 21.353, 0, 58500, 44, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(527, 378.885, -2067.24, 21.3717, 0, 55100, 45, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(528, 376.216, -2070.29, 21.373, 0, 57200, 51, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(529, 371.859, -2075.55, 21.3677, 0, 60900, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(530, 369.329, -2078.69, 21.3663, 0, 59800, 44, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(531, 364.892, -2084.02, 21.3462, 0, 56200, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(532, 340.516, -2098.65, 18.2048, 0, 59200, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(533, 332.598, -2106.23, 18.1252, 0, 64200, 25, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(534, 329.27, -2108.49, 18.123, 0, 51500, 44, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(535, 323.189, -2111.85, 18.1233, 0, 53200, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(536, 297.277, -2097.81, 17.6635, 0, 53700, 44, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(537, 295.695, -2093.2, 17.6635, 0, 58700, 51, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(538, 293.675, -2087.82, 17.6635, 0, 50400, 51, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(539, 293.04, -2086.17, 17.6635, 0, 64200, 47, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(540, 289.861, -2077.12, 17.6635, 0, 56600, 44, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(541, 288.244, -2072.54, 17.6635, 0, 54600, 48, 'n/A', 0, 0, 0, 0, 0, 'Dutch London St', 40, -1, 0, 30, 0, 50),
(542, 279.704, -2043.5, 19.7676, 0, 54800, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(543, 280.854, -2042.15, 19.7676, 0, 54400, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(544, 286.883, -2034.65, 19.7676, 0, 50700, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(545, 290.049, -2031.02, 19.7675, 0, 57500, 48, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(546, 279.782, -1993.75, 20.8038, 0, 55100, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(547, 291.255, -1980.52, 21.6005, 0, 63400, 47, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(548, 256.577, -2023.62, 19.2663, 0, 54000, 42, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(549, 236.162, -2046.14, 18.38, 0, 56700, 51, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(550, 295.828, -1971.92, 22.9008, 0, 60200, 45, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(551, 313.305, -1957.27, 24.2228, 0, 61300, 25, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(552, 324.031, -1937.64, 25.019, 0, 54900, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(553, 282.409, -1898.82, 27.2676, 0, 59300, 48, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(554, 257.987, -1927.17, 25.4448, 0, 57600, 48, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(555, 250.582, -1934.83, 24.6993, 0, 61200, 48, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(556, 178.96, -1924.3, 21.371, 0, 63400, 25, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(557, 165.514, -1945.24, 20.2354, 0, 61300, 44, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(558, 149.198, -1961.01, 19.4575, 0, 64200, 48, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(559, 144.062, -1969.31, 18.8577, 0, 58500, 25, 'n/A', 0, 0, 0, 0, 0, 'Roy Lowenstein Blvd', 40, -1, 0, 30, 0, 50),
(560, 104.083, -1885.17, 24.3187, 0, 61500, 45, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 50),
(561, 115.006, -1887.62, 23.9283, 0, 53300, 47, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 50),
(562, 127.913, -1896.51, 23.6741, 0, 55600, 44, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 51),
(563, 148.824, -1904.2, 23.5311, 0, 56600, 45, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 50),
(564, 192.265, -1883.49, 25.0449, 0, 62600, 51, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 50),
(565, 171.02, -1871.33, 24.4002, 0, 50800, 45, 'n/A', 0, 0, 0, 0, 0, 'Covenant Ave', 40, -1, 0, 30, 0, 50),
(566, 361.075, -1986.31, 24.2345, 0, 406000, 0, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(567, 335.562, -2010.72, 22.3131, 0, 55900, 44, 'n/A', 0, 0, 0, 0, 0, 'Jamestown St', 40, -1, 0, 30, 0, 50),
(568, 371.725, -2040.98, 22.1988, 0, 53100, 51, 'n/A', 0, 0, 0, 0, 0, 'Carson Ave', 40, -1, 0, 30, 0, 50),
(569, 3806.39, 4477.63, 5.99274, 0, 56300, 25, 'n/A', 0, 0, 0, 0, 0, 'Catfish View', 40, -1, 0, 30, 0, 50),
(570, 3725.29, 4525.08, 22.4705, 0, 61400, 48, 'n/A', 0, 0, 0, 0, 0, 'Catfish View', 40, -1, 0, 30, 0, 50),
(571, 3688.51, 4562.8, 25.1831, 0, 52300, 48, 'n/A', 0, 0, 0, 0, 0, 'Catfish View', 40, -1, 0, 30, 0, 50),
(572, 3426.26, 5174.66, 7.38214, 0, 59800, 47, 'n/A', 0, 0, 0, 0, 0, 'Catfish View', 40, -1, 0, 30, 0, 50),
(573, 3311.41, 5175.92, 19.6146, 0, 169700, 17, 'n/A', 0, 0, 0, 0, 0, 'Catfish View', 40, -1, 0, 30, 1, 50),
(574, -1040.22, 312.071, 67.2745, 0, 416600, 0, 'n/A', 0, 0, 0, 0, 0, 'Greenwich Way', 40, -1, 0, 30, 0, 50),
(575, -1520.73, 851.571, 181.595, 0, 403500, 0, 'n/A', 0, 0, 0, 0, 0, 'Marlowe Dr', 40, -1, 0, 30, 0, 50),
(576, 1393, 1139.6, 114.442, 0, 415900, 0, 'n/A', 0, 0, 0, 0, 0, 'Senora Rd', 40, -1, 0, 30, 0, 50),
(577, -1891.9, 2050.45, 140.972, 0, 408300, 0, 'n/A', 0, 0, 0, 0, 0, 'Buen Vino Rd', 40, -1, 0, 30, 0, 50),
(578, 810.795, 2713.96, 40.2699, 0, 805800, 46, 'n/A', 0, 0, 0, 0, 0, 'Route 68', 40, -1, 0, 30, 3, 50),
(579, -1972.74, 1253.99, 261.489, 0, 800600, 46, 'n/A', 0, 0, 0, 0, 0, 'Banham Canyon Dr', 40, -1, 0, 30, 3, 50),
(580, -1982.81, 1240.19, 265.321, 0, 419500, 4, 'n/A', 0, 0, 0, 0, 0, 'Banham Canyon Dr', 40, -1, 0, 30, 0, 50),
(581, -504.839, -294.678, 35.4567, 0, 802100, 46, 'n/A', 0, 0, 0, 0, 0, 'Carcer Way', 40, -1, 0, 30, 3, 50),
(582, 339.243, -1709.54, 29.2612, 0, 179500, 9, 'n/A', 0, 0, 0, 0, 0, 'Macdonald St', 40, -1, 0, 30, 1, 51),
(583, -1629.86, 36.5072, 62.936, 0, 414700, 36, 'n/A', 0, 0, 0, 0, 0, 'Americano Way', 40, -1, 0, 30, 2, 50),
(584, 240.649, 3107.94, 42.4872, 0, 408800, 0, 'n/A', 0, 0, 0, 0, 0, 'Joshua Rd', 40, -1, 0, 30, 0, 50),
(585, 1843.39, 3778.25, 33.592, 0, 52500, 44, 'n/A', 0, 0, 0, 0, 0, 'Armadillo Ave', 40, -1, 0, 30, 0, 50),
(586, 1848.24, 3786.61, 33.5264, 0, 51500, 25, 'n/A', 0, 0, 0, 0, 0, 'Algonquin Blvd', 40, -1, 0, 30, 0, 50),
(587, 1864.26, 3790.83, 32.8551, 0, 52600, 51, 'n/A', 0, 0, 0, 0, 0, 'Algonquin Blvd', 40, -1, 0, 30, 0, 50),
(588, 1880.77, 3811.05, 32.7788, 0, 54900, 45, 'n/A', 0, 0, 0, 0, 0, 'Algonquin Blvd', 40, -1, 0, 30, 0, 50),
(589, 1898.9, 3781.76, 32.8769, 0, 59900, 62, 'n/A', 0, 0, 0, 0, 0, 'Niland Ave', 40, -1, 0, 30, 0, 50),
(590, 1830.81, 3738.08, 33.9619, 0, 59700, 45, 'n/A', 0, 0, 0, 0, 0, 'Armadillo Ave', 40, -1, 0, 30, 0, 50),
(591, 1826.42, 3729.43, 33.9619, 0, 59800, 48, 'n/A', 0, 0, 0, 0, 0, 'Zancudo Ave', 40, -1, 0, 30, 0, 50),
(592, 1774.61, 3742.9, 34.6554, 0, 57000, 44, 'n/A', 0, 0, 0, 0, 0, 'Mountain View Dr', 40, -1, 0, 30, 0, 50),
(593, 1777.3, 3738.22, 34.6551, 0, 52900, 51, 'n/A', 0, 0, 0, 0, 0, 'Mountain View Dr', 40, -1, 0, 30, 0, 50),
(594, 1748.68, 3783.37, 34.8349, 0, 51300, 44, 'n/A', 0, 0, 0, 0, 0, 'Mountain View Dr', 40, -1, 0, 30, 0, 50),
(595, 1746.1, 3788.05, 34.8349, 0, 61100, 44, 'n/A', 0, 0, 0, 0, 0, 'Mountain View Dr', 40, -1, 0, 30, 0, 50),
(596, 1777.6, 3799.82, 34.5241, 0, 50200, 44, 'n/A', 0, 0, 0, 0, 0, 'Algonquin Blvd', 40, -1, 0, 30, 0, 50),
(597, 1857.61, 3854.63, 33.1026, 0, 54400, 42, 'n/A', 0, 0, 0, 0, 0, 'Niland Ave', 40, -1, 0, 30, 0, 50),
(598, 1807.79, 3851.05, 34.3544, 0, 61200, 25, 'n/A', 0, 0, 0, 0, 0, 'Armadillo Ave', 40, -1, 0, 30, 0, 50),
(599, 1813.49, 3853.93, 34.3545, 0, 59800, 48, 'n/A', 0, 0, 0, 0, 0, 'Cholla Springs Ave', 40, -1, 0, 30, 0, 50),
(600, 1832.5, 3868.47, 34.2975, 0, 61700, 45, 'n/A', 0, 0, 0, 0, 0, 'Cholla Springs Ave', 40, -1, 0, 30, 0, 50);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `inactiv`
--

CREATE TABLE `inactiv` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `date1` int(11) NOT NULL,
  `date2` int(11) NOT NULL,
  `text` varchar(35) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `invoices`
--

CREATE TABLE `invoices` (
  `id` int(11) NOT NULL,
  `value` int(11) NOT NULL DEFAULT 0,
  `empfänger` varchar(64) NOT NULL DEFAULT 'n/A',
  `modus` varchar(35) NOT NULL DEFAULT 'Privatrechnung',
  `text` varchar(128) NOT NULL DEFAULT 'n/A',
  `banknumber` varchar(15) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `itemmodels`
--

CREATE TABLE `itemmodels` (
  `id` int(11) NOT NULL,
  `hash` varchar(25) NOT NULL DEFAULT 'n/A',
  `description` varchar(64) NOT NULL DEFAULT 'n/A',
  `type` int(3) NOT NULL DEFAULT 0,
  `weight` int(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `itemmodels`
--

INSERT INTO `itemmodels` (`id`, `hash`, `description`, `type`, `weight`) VALUES
(1, '3602873787', 'Sandwich', 1, 200),
(2, '1020618269', 'Cola', 2, 150),
(3, '-1282513796', 'EC-Karte', 3, 85),
(4, '494219267', 'Hausschlüssel', 3, 55),
(5, '494219267', 'Mietschlüssel', 3, 55),
(6, '494219267', 'Bizzschlüssel', 3, 55),
(7, '-1158162337', 'Benzinkanister', 4, 2500),
(8, '974883178', 'Smartphone', 4, 115),
(9, '-1321253704', 'Sprunk', 2, 150),
(10, '-2086291657', 'Pfandflasche', 2, 150),
(11, '-1158162337', 'Pistole', 5, 900),
(12, '-1158162337', '9mm-Munition', 6, 3),
(13, '-1158162337', 'Pistole-MK2', 5, 975),
(14, '-1158162337', 'Pistole-50', 5, 925),
(15, '-1158162337', 'Combat-Pistole', 5, 950),
(16, '-1158162337', 'Heavy-Pistole', 5, 975),
(17, '-1158162337', 'Keramik-Pistole', 5, 945),
(18, '-1158162337', 'Flaregun', 5, 850),
(19, '-1158162337', 'Revolver', 5, 1000),
(20, '-1158162337', 'Revolver-MK2', 5, 1100),
(21, '-1158162337', 'SNS-Pistole', 5, 925),
(22, '-1158162337', 'SNS-Pistole-MK2', 5, 930),
(23, '-1158162337', 'Taser', 5, 650),
(24, '-1158162337', 'Flare', 6, 85),
(25, '-1158162337', 'Dolch', 5, 255),
(26, '-1158162337', 'Baseballschläger', 5, 975),
(27, '-1158162337', 'Brechstange', 5, 700),
(28, '-1158162337', 'Taschenlampe', 5, 350),
(29, '-1158162337', 'Golfschläger', 5, 475),
(30, '-1158162337', 'Axt', 5, 1250),
(31, '-1158162337', 'Schlagring', 5, 300),
(32, '-1158162337', 'Messer', 5, 350),
(33, '-1158162337', 'Machete', 5, 850),
(34, '-1158162337', 'Klappmesser', 5, 450),
(35, '-1158162337', 'Schlagstock', 5, 475),
(36, '-1158162337', 'Poolcue', 5, 315),
(37, '-1158162337', 'Micro-SMG', 5, 2150),
(38, '-1158162337', 'MP5', 5, 2750),
(39, '-1158162337', 'MP5-MK2', 5, 2850),
(40, '-1158162337', 'Assault-SMG', 5, 3000),
(41, '-1158162337', 'Combat-PDW', 5, 2950),
(42, '-1158162337', 'TEC9', 5, 2850),
(43, '-1158162337', 'Mini-SMG', 5, 2350),
(44, '-1158162337', 'Pumpshotgun', 5, 3500),
(45, '-1158162337', 'Pumpshotgun-MK2', 5, 3750),
(46, '-1158162337', 'Musket', 5, 3350),
(47, '-1158162337', 'Sawnoffshotgun', 5, 3750),
(48, '-1158162337', 'Combatshutgon', 5, 3600),
(49, '-1158162337', 'Shotgun-Munition', 6, 8),
(50, '-1158162337', 'Angriffsgewehr', 5, 3000),
(51, '-1158162337', 'Angriffsgewehr-MK2', 5, 3200),
(52, '-1158162337', 'Karabiner-Gewehr', 5, 3350),
(53, '-1158162337', 'Karabiner-Gewehr-MK2', 5, 3450),
(54, '-1158162337', 'Spezial-Karabiner', 5, 3400),
(55, '-1158162337', 'Spezial-Karabiner-MK2', 5, 3500),
(56, '-1158162337', 'Kompaktgewehr', 5, 2950),
(57, '-1158162337', '5.56-Munition', 6, 4),
(58, '-1158162337', 'Maschinengewehr', 5, 4000),
(59, '-1158162337', 'Gusenberg', 5, 4150),
(60, '-1158162337', 'Sniper', 5, 3850),
(61, '-1158162337', 'RPG', 5, 4500),
(62, '-1158162337', 'Granate', 5, 950),
(63, '-1158162337', 'BZGas', 5, 850),
(64, '-1158162337', 'Rauchgranate', 5, 700),
(65, '-1158162337', 'Molotowcocktail', 5, 725),
(66, '-1158162337', '7.62-Munition', 6, 5),
(67, '-1158162337', 'Schokolade', 1, 120),
(68, '-1158162337', 'Kleine-Schutzweste', 5, 1250),
(69, '-1158162337', 'Schutzweste', 5, 1500),
(70, '-1158162337', 'Grosse-Schutzweste', 5, 1850),
(71, '-1503146199', 'F-Zeugnis', 4, 25),
(72, '494219267', 'Fahrzeugschlüssel', 3, 55),
(73, '-1158162337', 'Rakete', 6, 215),
(74, '-1158162337', 'Reparaturwerkzeug', 4, 1500),
(75, '-1158162337', 'Dietrich', 4, 65),
(76, '-2086291657', 'Fleisch', 1, 250),
(77, '2017086435', 'Zigaretten', 4, 15),
(78, '-680040094', 'Feuerzeug', 4, 85),
(79, '-1910604593', 'Angel', 4, 500),
(80, '-1158162337', 'Köder', 1, 25),
(81, '-1158162337', 'Dorsch', 1, 225),
(82, '-1158162337', 'Wildkarpfen', 1, 250),
(83, '-1158162337', 'Teufelskärpfling', 1, 275),
(84, '-1158162337', 'Makrele', 1, 235),
(85, '-1158162337', 'Forelle', 1, 245),
(86, '-1158162337', 'Busticket', 3, 50),
(87, '-1158162337', '55$-Prepaidkarte', 4, 50),
(88, '-1158162337', 'Handyvertrag', 4, 50),
(89, '-1158162337', 'Schweissgerät', 4, 3250),
(90, '-1158162337', 'Drohne', 4, 2500),
(91, '-1158162337', 'Kabelbinder', 4, 85),
(92, '-1158162337', 'Kleines-Messer', 4, 250),
(93, '-1158162337', 'Funkgerät', 4, 200),
(94, '-2086291657', 'Wasser', 2, 990),
(95, '1847598393', 'Donut', 1, 75),
(96, '-1158162337', 'Chips', 1, 175),
(97, '-1158162337', 'Erdnussschale', 1, 125),
(98, '683570518', 'Bier', 2, 850),
(99, '398314184', 'Sekt', 2, 875),
(100, '398314184', 'Champagne', 2, 875),
(101, '1925761914', 'Vodka', 2, 880),
(102, '398314184', 'Wein', 2, 855),
(103, '-1158162337', 'Glowstick', 4, 125),
(104, '-1158162337', 'Horsestick', 4, 215),
(105, '-1158162337', 'Grippofein-C', 4, 50),
(106, '-1158162337', 'Antibiotika', 4, 50),
(107, '-1158162337', 'Ibuprofee-400mg', 4, 50),
(108, '-1158162337', 'Ibuprofee-800mg', 4, 50),
(109, '-1158162337', 'Morphin-10mg', 4, 50),
(110, '-1158162337', 'Bandage', 4, 25),
(111, '-1158162337', 'Rezept', 3, 25),
(112, '-1158162337', 'Feuerlöscher', 5, 3000),
(113, '-1158162337', 'Defribrilator', 4, 2500),
(114, '-1158162337', 'Stethoskop', 4, 475),
(115, '-1158162337', 'Gehstock', 4, 415),
(116, '-1158162337', 'L-Schein', 4, 50),
(117, '-1158162337', 'Ghettoblaster', 4, 1500),
(118, '-1158162337', 'Parkkralle', 4, 4500),
(119, '-1158162337', 'Materialien', 4, 10),
(120, '-1158162337', 'Hackwerkzeug', 4, 850),
(121, '-1158162337', 'Marihuanasamen', 4, 5),
(122, '-1158162337', 'Marihuana', 4, 5),
(123, '-1158162337', 'Papes', 4, 5),
(124, '-1158162337', 'Joint', 4, 5),
(125, '-1158162337', 'Kokainsamen', 4, 5),
(126, '-1158162337', 'Kokain', 4, 5),
(127, '-1158162337', 'Kokablatt', 4, 5),
(128, '-1158162337', 'Schwefelsäure', 4, 150),
(129, '-1158162337', 'Frostschutzmittel', 4, 125),
(130, '-1158162337', 'Crystal-Meth', 4, 5),
(131, '-1158162337', 'Space-Cookies', 4, 15),
(132, '-1158162337', 'Backmischung', 4, 100),
(133, '-1158162337', 'M16', 5, 945),
(134, '-1158162337', 'Spitzhacke', 4, 650),
(135, '-1158162337', 'Kleine-Schaufel', 4, 375),
(136, '-1158162337', 'Mikrofon', 4, 500),
(137, '-1158162337', 'Filmkamera', 4, 2350),
(138, '-1158162337', 'Snowball', 5, 125),
(139, '-1158162337', 'Haustier', 4, 3500);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `lifeinvaderads`
--

CREATE TABLE `lifeinvaderads` (
  `id` int(11) NOT NULL,
  `text` varchar(128) NOT NULL,
  `name` varchar(35) NOT NULL,
  `number` varchar(10) NOT NULL,
  `status` varchar(15) NOT NULL,
  `editor` varchar(35) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `logs`
--

CREATE TABLE `logs` (
  `id` int(11) NOT NULL,
  `loglabel` varchar(35) NOT NULL,
  `text` varchar(256) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `namechanges`
--

CREATE TABLE `namechanges` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `oldname` varchar(35) NOT NULL,
  `newname` varchar(35) NOT NULL,
  `status` int(1) NOT NULL DEFAULT 0,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `outfits`
--

CREATE TABLE `outfits` (
  `id` int(11) NOT NULL,
  `name` varchar(35) NOT NULL,
  `owner` varchar(35) NOT NULL,
  `json1` longtext NOT NULL,
  `json2` longtext NOT NULL,
  `category1` varchar(35) NOT NULL,
  `category2` varchar(35) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `password_resets`
--

CREATE TABLE `password_resets` (
  `email` varchar(255) NOT NULL,
  `token` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `paydays`
--

CREATE TABLE `paydays` (
  `id` int(11) NOT NULL,
  `characterid` int(11) NOT NULL,
  `text` longtext NOT NULL,
  `timestamp` int(11) NOT NULL,
  `total` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personal_access_tokens`
--

CREATE TABLE `personal_access_tokens` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `tokenable_type` varchar(255) NOT NULL,
  `tokenable_id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `token` varchar(64) NOT NULL,
  `abilities` text DEFAULT NULL,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `policefile`
--

CREATE TABLE `policefile` (
  `id` int(11) NOT NULL,
  `name` varchar(35) NOT NULL DEFAULT 'n/A',
  `police` varchar(35) NOT NULL DEFAULT 'n/A',
  `text` varchar(225) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861,
  `commentary` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `screenshots`
--

CREATE TABLE `screenshots` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `charid` int(11) NOT NULL,
  `screenshot` varchar(128) NOT NULL DEFAULT 'https://i.imgur.com/JjpH0qO.jpg',
  `screenname` varchar(128) NOT NULL DEFAULT 'n/A',
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `services`
--

CREATE TABLE `services` (
  `id` int(11) NOT NULL,
  `groupid` int(11) NOT NULL,
  `text` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `settings`
--

CREATE TABLE `settings` (
  `id` int(11) NOT NULL,
  `wartung` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `settings`
--

INSERT INTO `settings` (`id`, `wartung`) VALUES
(1, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `shopitems`
--

CREATE TABLE `shopitems` (
  `id` int(11) NOT NULL,
  `shoplabel` varchar(64) NOT NULL DEFAULT '24/7',
  `itemname` varchar(64) NOT NULL DEFAULT 'n/A',
  `itemprice` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `shopitems`
--

INSERT INTO `shopitems` (`id`, `shoplabel`, `itemname`, `itemprice`) VALUES
(1, '24/7', 'Wasser', 17),
(2, '24/7', 'Sandwich', 35),
(3, '24/7', 'Sprunk', 30),
(4, '24/7', 'Schokolade', 45),
(5, '24/7', 'Benzinkanister', 175),
(6, 'Ammunation1', 'Baseballschläger', 1250),
(7, 'Ammunation1', 'Schlagstock', 1500),
(8, 'Ammunation1', 'Pistole', 3500),
(9, 'Ammunation1', 'Pistole-50', 3750),
(10, 'Ammunation1', 'Combat-Pistole', 3850),
(11, 'Ammunation1', 'Keramik-Pistole', 4000),
(12, 'Ammunation1', 'Flaregun', 4250),
(13, 'Ammunation1', 'Micro-SMG', 4550),
(14, 'Ammunation1', 'Mini-SMG', 4750),
(17, 'Ammunation2', '9mm-Munition', 350),
(18, 'Ammunation2', '5.56-Munition', 450),
(19, 'Ammunation2', 'Shotgun-Munition', 415),
(20, 'Ammunation2', 'Flare', 175),
(22, '24/7', 'Reparaturwerkzeug', 475),
(23, '24/7', 'Taschenlampe', 215),
(24, 'Ammunation1', 'Kleine-Schutzweste', 5000),
(25, '24/7', 'Feuerzeug', 215),
(26, 'Ammunation1', 'Musket', 4250),
(27, 'Ammunation2', '7.62-Munition', 550),
(28, '24/7', 'Zigaretten', 425),
(29, '24/7', 'Smartphone', 1050),
(30, 'Ammunation1', 'Messer', 1600),
(31, '24/7', '55$-Prepaidkarte', 55),
(32, '24/7', 'Handyvertrag', 1),
(35, 'Waffenkammer-1', 'Schlagstock', 25),
(36, 'Waffenkammer-1', 'Taser', 25),
(37, 'Waffenkammer-1', 'Pistole', 25),
(38, 'Waffenkammer-1', 'Pistole-MK2', 25),
(39, 'Waffenkammer-1', 'Pistole-50', 25),
(40, 'Waffenkammer-1', 'MP5-MK2', 15),
(41, 'Waffenkammer-1', 'Karabiner-Gewehr-MK2', 15),
(42, 'Waffenkammer-1', 'Sniper', 10),
(43, 'Waffenkammer-1', 'BZGas', 15),
(44, 'Waffenkammer-1', 'Rauchgranate', 15),
(45, 'Waffenkammer-1', 'Grosse-Schutzweste', 50),
(46, 'Waffenkammer-1', '9mm-Munition', 250),
(47, 'Waffenkammer-1', '5.56-Munition', 250),
(48, 'Waffenkammer-1', '7.62-Munition', 250),
(49, 'Waffenkammer-1', 'Dietrich', 50),
(50, 'Waffenkammer-1', 'Drohne', 5),
(51, '24/7', 'Funkgerät', 525),
(52, 'Snackpoint', 'Cola', 25),
(53, 'Snackpoint', 'Sandwich', 30),
(54, 'Snackpoint', 'Sprunk', 25),
(55, 'Snackpoint', 'Schokolade', 35),
(56, 'Snackpoint', 'Wasser', 15),
(57, 'Snackpoint', 'Donut', 40),
(58, 'Snackpoint', 'Chips', 50),
(59, 'Bar', 'Wasser', 50),
(60, 'Bar', 'Cola', 75),
(61, 'Bar', 'Bier', 100),
(62, 'Bar', 'Vodka', 150),
(63, 'Bar', 'Wein', 215),
(64, 'Bar', 'Sekt', 275),
(65, 'Bar', 'Champagne', 500),
(66, 'Bar', 'Erdnussschale', 75),
(67, 'Bar', 'Chips', 85),
(68, 'Bar', 'Glowstick', 550),
(69, 'Bar', 'Horsestick', 650),
(70, 'Apotheke', 'Bandage', 115),
(71, 'Apotheke', 'Grippofein-C', 255),
(72, 'Apotheke', 'Ibuprofee-400mg', 315),
(73, 'Apotheke', 'Ibuprofee-800mg', 415),
(74, 'Apotheke', 'Antibiotika', 500),
(75, 'Apotheke', 'Morphin-10mg', 655),
(76, 'Waffenkammer-2', 'Defribrilator', 25),
(77, 'Waffenkammer-2', 'Taser', 25),
(78, 'Waffenkammer-2', 'Stethoskop', 25),
(79, 'Waffenkammer-2', 'Drohne', 5),
(80, 'Waffenkammer-2', 'Grippofein-C', 25),
(81, 'Waffenkammer-2', 'Antibiotika', 25),
(82, 'Waffenkammer-2', 'Ibuprofee-400mg', 25),
(83, 'Waffenkammer-2', 'Ibuprofee-800mg', 25),
(84, 'Waffenkammer-2', 'Morphin-10mg', 25),
(85, 'Waffenkammer-2', 'Bandage', 25),
(86, 'Waffenkammer-2', 'Feuerlöscher', 15),
(87, 'Waffenkammer-2', 'Axt', 15),
(88, 'Apotheke', 'Gehstock', 575),
(89, '24/7', 'L-Schein', 500),
(90, 'Waffenkammer-2', 'Dietrich', 25),
(92, '24/7', 'Ghettoblaster', 850),
(93, 'Waffenkammer-3', 'Taser', 25),
(94, 'Waffenkammer-3', 'Parkkralle', 15),
(95, 'Waffenkammer-3', 'Reparaturwerkzeug', 15),
(96, 'Waffenkammer-3', 'Dietrich', 25),
(97, 'Waffenkammer-3', 'Taschenlampe', 25),
(98, 'Waffenkammer-3', 'Brechstange', 25),
(99, 'Waffenkammer-3', 'Benzinkanister', 25),
(100, 'Waffenkammer-A', 'Pistole', 4500),
(101, 'Waffenkammer-A', 'Pistole-MK2', 4750),
(102, 'Waffenkammer-A', 'Combat-Pistole', 5000),
(103, 'Waffenkammer-A', 'Micro-SMG', 10000),
(104, 'Waffenkammer-A', 'TEC9', 12500),
(105, 'Waffenkammer-A', 'Pumpshotgun', 19500),
(106, 'Waffenkammer-A', 'M16', 25000),
(107, 'Waffenkammer-A', 'Rauchgranate', 950),
(108, 'Waffenkammer-A', 'Dietrich', 85),
(109, 'Waffenkammer-A', 'Kleines-Messer', 1500),
(110, 'Waffenkammer-A', 'Kabelbinder', 60),
(111, 'Waffenkammer-A', 'Ibuprofee-800mg', 450),
(112, 'Waffenkammer-A', 'Drohne', 35000),
(113, 'Waffenkammer-A', 'Schweissgerät', 12500),
(114, 'Waffenkammer-A', 'Kleine-Schutzweste', 8000),
(115, 'Waffenkammer-A', 'Schutzweste', 10000),
(116, 'Waffenkammer-A', '9mm-Munition', 500),
(117, 'Waffenkammer-A', '5.56-Munition', 600),
(118, 'Waffenkammer-A', '7.62-Munition', 750),
(119, 'Waffenkammer-A', 'Shotgun-Munition', 585),
(120, 'Waffenkammer-A', 'Materialien', 42),
(121, 'Waffenkammer-A', 'Hackwerkzeug', 3750),
(122, '24/7', 'Papes', 95),
(123, 'Waffenkammer-D', 'Schwefelsäure', 515),
(124, '24/7', 'Frostschutzmittel', 315),
(125, 'Waffenkammer-D', 'Marihuanasamen', 275),
(126, 'Waffenkammer-D', 'Kokainsamen', 300),
(127, '24/7', 'Backmischung', 215),
(128, 'Waffenkammer-1', 'Haustier', 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `smartphonecalls`
--

CREATE TABLE `smartphonecalls` (
  `id` int(11) NOT NULL,
  `tonumber` varchar(15) NOT NULL,
  `fromnumber` varchar(15) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `smartphonemessages`
--

CREATE TABLE `smartphonemessages` (
  `id` int(11) NOT NULL,
  `tomessage` varchar(15) NOT NULL,
  `frommessage` varchar(15) NOT NULL,
  `text` varchar(128) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `smartphones`
--

CREATE TABLE `smartphones` (
  `id` int(11) NOT NULL,
  `phonenumber` varchar(10) NOT NULL DEFAULT 'n/A',
  `phoneprops` longtext NOT NULL,
  `contacts` longtext NOT NULL DEFAULT '[]',
  `akku` int(2) NOT NULL DEFAULT 48,
  `prepaid` int(4) NOT NULL DEFAULT 55,
  `owner` varchar(35) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `spedvehicles`
--

CREATE TABLE `spedvehicles` (
  `id` int(11) NOT NULL,
  `name` varchar(64) NOT NULL DEFAULT 'n/A',
  `capa` int(4) NOT NULL DEFAULT 0,
  `skill` int(1) NOT NULL DEFAULT 0,
  `skilltext` varchar(64) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `spedvehicles`
--

INSERT INTO `spedvehicles` (`id`, `name`, `capa`, `skill`, `skilltext`) VALUES
(1, 'Burrito', 175, 1, 'Anfänger'),
(2, 'Rumpo', 200, 1, 'Anfänger'),
(3, 'Boxville4', 225, 1, 'Anfänger'),
(4, 'Mule', 250, 2, 'Erfahrener'),
(5, 'Mule3', 375, 2, 'Erfahrener'),
(6, 'Mule4', 500, 3, 'Profi'),
(7, 'Packer', 1000, 4, 'Meister'),
(8, 'Phantom', 1500, 5, 'Experte'),
(9, 'Phantom3', 1800, 5, 'Experte'),
(10, 'Pounder', 2000, 5, 'Experte');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `speedcameras`
--

CREATE TABLE `speedcameras` (
  `id` int(11) NOT NULL,
  `who` varchar(35) NOT NULL,
  `from` varchar(35) NOT NULL,
  `maxspeed` int(4) NOT NULL,
  `heading` float NOT NULL,
  `position` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `standingorder`
--

CREATE TABLE `standingorder` (
  `id` int(11) NOT NULL,
  `ownercharid` int(11) NOT NULL DEFAULT 0,
  `bankfrom` varchar(20) NOT NULL DEFAULT 'n/A',
  `bankto` varchar(20) NOT NULL DEFAULT 'n/A',
  `bankvalue` int(11) NOT NULL DEFAULT 0,
  `banktext` varchar(64) NOT NULL DEFAULT 'n/A',
  `days` int(3) NOT NULL DEFAULT 0,
  `daysrun` int(3) NOT NULL DEFAULT 0,
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `tattoos`
--

CREATE TABLE `tattoos` (
  `id` int(11) NOT NULL,
  `characterid` int(11) NOT NULL,
  `name` varchar(64) NOT NULL,
  `dlcname` varchar(64) NOT NULL,
  `zoneid` int(2) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `taxiroutes`
--

CREATE TABLE `taxiroutes` (
  `id` int(11) NOT NULL,
  `routes` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `taxiroutes`
--

INSERT INTO `taxiroutes` (`id`, `routes`) VALUES
(1, '-553.53687,-278.66092,35.18272,Carcer Way|-600.07605,-336.02112,34.860695,Carcer Way|-665.6767,-385.64133,34.58592,Dorset Dr|215.51198,-817.51483,30.645046,San Andreas Ave|263.3449,-620.8089,42.063885,Integrity Way|318.34903,-270.2416,53.905613,Hawick Ave|-346.45547,-31.73156,47.527096,Hawick Ave|-478.07654,-249.10158,35.93023,Carcer Way|-291.66568,-1459.8224,31.304583,Alta St|144.40103,-1729.3121,29.200035,Carson Ave|344.66443,-1554.1592,29.29156,Innocence Blvd|36.16025,-1114.55,29.18677,Elgin Ave|-546.4895,-645.2583,33.233864,San Andreas Ave|-729.77673,-139.31232,37.39842,Portola Dr|57.75981,-306.292,47.20252,Occupation Ave|262.31256,-382.2267,44.80589,Occupation Ave|766.25684,-926.8357,25.47069,Popular St|785.10895,-1397.3455,27.188658,Popular St|781.60126,-1904.537,29.178637,Popular St|696.4868,-2456.2932,19.928865,Chum St|627.324,-2641.5898,6.0994306,Abattoir Ave|-341.15805,-1840.3398,23.34815,Davis Ave|-725.9134,-1300.3856,5.102056,Tackle St|-801.4815,-1119.565,10.57759,Palomino Ave|746.72046,115.2337,78.72628,Elgin Ave|70.85181,-193.78412,54.48667,Alta St|-924.67194,-291.13275,39.714413,Marathon Ave|-879.0028,-897.3245,15.725238,South Rockford Dr|474.29456,-1450.8254,29.292292,Capital Blvd|853.28937,-1560.8857,30.009628,Popular St|-968.7531,-2156.1929,8.877692,Greenwich Pkwy|-954.1937,-2719.4844,13.756636,New Empire Way|779.8662,-3018.022,5.9145546,Buccaneer Way|1208.8987,-1706.2017,36.532894,Innocence Blvd|-253.07951,-1025.8647,28.565275,Alta St');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `tickets`
--

CREATE TABLE `tickets` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `title` varchar(64) NOT NULL,
  `prio` varchar(10) NOT NULL,
  `text` varchar(3500) NOT NULL,
  `timestamp` int(11) NOT NULL DEFAULT 1609462861,
  `admin` int(11) NOT NULL DEFAULT -1,
  `status` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ticket_answers`
--

CREATE TABLE `ticket_answers` (
  `id` int(11) NOT NULL,
  `ticketid` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `text` longtext NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ticket_user`
--

CREATE TABLE `ticket_user` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `ticketid` int(11) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `timeline`
--

CREATE TABLE `timeline` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `charid` int(11) NOT NULL,
  `text` varchar(128) NOT NULL,
  `icon` varchar(25) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `transfer`
--

CREATE TABLE `transfer` (
  `id` int(11) NOT NULL,
  `bankfrom` varchar(20) NOT NULL DEFAULT 'n/A',
  `bankto` varchar(20) NOT NULL DEFAULT 'n/A',
  `banktext` varchar(64) NOT NULL DEFAULT 'n/A',
  `bankvalue` int(11) NOT NULL DEFAULT 0,
  `bankname` varchar(35) NOT NULL DEFAULT 'n/A'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `userfile`
--

CREATE TABLE `userfile` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `admin` varchar(35) NOT NULL,
  `text` varchar(60) NOT NULL,
  `penalty` varchar(35) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `userlog`
--

CREATE TABLE `userlog` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `action` varchar(128) NOT NULL,
  `timestamp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(35) NOT NULL,
  `adminlevel` int(2) NOT NULL DEFAULT 0,
  `password` varchar(255) NOT NULL,
  `remember_token` varchar(100) DEFAULT NULL,
  `admin_since` int(11) NOT NULL DEFAULT 1609462861,
  `selectedcharacter` int(3) NOT NULL DEFAULT -1,
  `selectedcharacterintern` int(11) NOT NULL DEFAULT -1,
  `last_login` int(11) NOT NULL DEFAULT 1609462861,
  `account_created` int(11) NOT NULL DEFAULT 1609462861,
  `last_save` int(11) NOT NULL DEFAULT 1609462861,
  `level` int(4) NOT NULL DEFAULT 1,
  `play_time` int(6) NOT NULL DEFAULT 0,
  `play_points` int(6) NOT NULL DEFAULT 0,
  `kills` int(6) NOT NULL DEFAULT 0,
  `deaths` int(6) NOT NULL DEFAULT 0,
  `crimes` int(6) NOT NULL DEFAULT 0,
  `premium` int(1) NOT NULL DEFAULT 0,
  `premium_time` int(11) NOT NULL DEFAULT 1609462861,
  `coins` int(6) NOT NULL DEFAULT 0,
  `warns` int(1) NOT NULL DEFAULT 0,
  `warns_text` varchar(175) NOT NULL DEFAULT 'n/A|n/A|n/A|n/A|n/A',
  `online` int(1) NOT NULL DEFAULT 0,
  `namechanges` int(3) NOT NULL DEFAULT 0,
  `geworben` varchar(35) NOT NULL DEFAULT 'Keiner',
  `theme` varchar(5) NOT NULL DEFAULT 'dark',
  `ban` int(11) NOT NULL DEFAULT 0,
  `bantext` varchar(35) NOT NULL DEFAULT 'n/A',
  `admin_rang` varchar(35) NOT NULL DEFAULT 'n/A',
  `prison` int(6) NOT NULL DEFAULT 0,
  `last_ip` varchar(64) NOT NULL DEFAULT 'n/A',
  `identifier` int(35) NOT NULL DEFAULT 10,
  `login_bonus` int(7) NOT NULL DEFAULT 0,
  `login_bonus_before` varchar(15) NOT NULL DEFAULT 'n/A',
  `google2fa_secret` text DEFAULT NULL,
  `dsgvo_closed` int(1) NOT NULL DEFAULT 0,
  `forumaccount` int(11) NOT NULL DEFAULT -1,
  `forumcode` int(4) NOT NULL DEFAULT 0,
  `forumupdate` int(11) NOT NULL DEFAULT 1609462861,
  `autologin` int(1) NOT NULL DEFAULT 0,
  `rpquizfinish` int(1) NOT NULL DEFAULT 0,
  `timestampchat` int(1) NOT NULL DEFAULT 1,
  `crosshair` int(3) NOT NULL DEFAULT 17,
  `shootingrange` int(4) NOT NULL DEFAULT 0,
  `faq` varchar(25) NOT NULL DEFAULT '1,0,0,0,0,0,0,0,0,0',
  `givepremium` int(1) NOT NULL DEFAULT 0,
  `houseslots` int(2) NOT NULL DEFAULT 0,
  `vehicleslots` int(2) NOT NULL DEFAULT 0,
  `epboost` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Daten für Tabelle `users`
--

INSERT INTO `users` (`id`, `name`, `adminlevel`, `password`, `remember_token`, `admin_since`, `selectedcharacter`, `selectedcharacterintern`, `last_login`, `account_created`, `last_save`, `level`, `play_time`, `play_points`, `kills`, `deaths`, `crimes`, `premium`, `premium_time`, `coins`, `warns`, `warns_text`, `online`, `namechanges`, `geworben`, `theme`, `ban`, `bantext`, `admin_rang`, `prison`, `last_ip`, `identifier`, `login_bonus`, `login_bonus_before`, `google2fa_secret`, `dsgvo_closed`, `forumaccount`, `forumcode`, `forumupdate`, `autologin`, `rpquizfinish`, `timestampchat`, `crosshair`, `shootingrange`, `faq`, `givepremium`, `houseslots`, `vehicleslots`, `epboost`) VALUES
(1, 'Testuser', 8, '$2y$10$6n4eP023KIEGINK1j6XE5Op3EDlh.t0BLMm4fXjEyh5CIbA6ra4t6', 'mpBECbLt2fRNcEiSMCVzNB1APnDOki1wdbulTsL6iikfzBxaLccd44IN8Kkg', 1633797716, 0, 1, 1633357259, 1633357259, 0, 1, 1, 8, 0, 0, 0, 1, 1633357259, 0, 1, 'test|testt|n/A|n/A|n/A', 1, 2, 'Keiner', 'dark', 0, 'testtt', 'Testrang', 0, 'n/A', 18021891, 1, '10-10-2022', NULL, 0, -1, 0, 1682087207, 0, 1, 1, 17, 0, '1,0,0,0,0,0,0,0,0,0', 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `vehicles`
--

CREATE TABLE `vehicles` (
  `id` int(11) NOT NULL,
  `position` varchar(115) NOT NULL DEFAULT '0.0|0.0|0.0|0.0|0|0|0',
  `owner` varchar(35) DEFAULT NULL,
  `vehiclename` varchar(60) NOT NULL DEFAULT 'n/A',
  `ownname` varchar(60) NOT NULL DEFAULT 'n/A',
  `plate` varchar(10) NOT NULL DEFAULT 'n/A',
  `health` varchar(25) NOT NULL DEFAULT '1000.0|1000.0|1000.0',
  `battery` int(3) NOT NULL DEFAULT 100,
  `status` int(1) NOT NULL DEFAULT 1,
  `engine` int(1) NOT NULL DEFAULT 0,
  `kilometre` float NOT NULL DEFAULT 0,
  `tuev` int(11) NOT NULL DEFAULT 1633780009,
  `oel` int(3) NOT NULL DEFAULT 100,
  `fuel` float NOT NULL DEFAULT 0,
  `rang` int(2) NOT NULL DEFAULT 1,
  `tuning` longtext NOT NULL,
  `garage` varchar(25) NOT NULL DEFAULT 'n/A',
  `sync` varchar(35) NOT NULL DEFAULT '0,0,0,0,0,0,0',
  `color` varchar(25) NOT NULL DEFAULT '0,0,-1,-1',
  `products` int(4) NOT NULL DEFAULT 0,
  `vlock` int(1) NOT NULL DEFAULT 0,
  `doors` varchar(55) NOT NULL DEFAULT '[false,false,false,false,false]',
  `windows` varchar(55) NOT NULL DEFAULT '[false,false,false,false]',
  `towed` int(5) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `vehicleshop`
--

CREATE TABLE `vehicleshop` (
  `id` int(11) NOT NULL,
  `bizzid` int(4) NOT NULL,
  `vehiclename` varchar(60) NOT NULL,
  `price` int(11) NOT NULL,
  `maxspeed` int(4) NOT NULL,
  `maxacceleration` float NOT NULL,
  `maxbraking` float NOT NULL,
  `maxhandling` float NOT NULL,
  `trunk` int(4) NOT NULL,
  `fuel` int(4) NOT NULL,
  `products` int(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `vehicleshop`
--

INSERT INTO `vehicleshop` (`id`, `bizzid`, `vehiclename`, `price`, `maxspeed`, `maxacceleration`, `maxbraking`, `maxhandling`, `trunk`, `fuel`, `products`) VALUES
(1, 22, 'Rmodbugatti', 670000, 335, 0.575, 1.4, 2.25, 15, 75, 0),
(2, 22, 'Adder', 374000, 187, 0.32, 1, 2.5, 15, 75, 0),
(3, 22, 'Infernus', 352000, 176, 0.34, 0.5, 2.65, 15, 75, 0),
(4, 22, 'Banshee2', 324000, 162, 0.3475, 1, 2.5, 15, 75, 0),
(5, 22, 'Bullet', 360000, 180, 0.33, 0.8, 2.55, 15, 75, 0),
(6, 22, 'Cheetah', 360000, 180, 0.32, 0.8, 2.65, 15, 75, 0),
(7, 22, 'Entity2', 388000, 194, 0.355, 1, 2.77, 15, 75, 0),
(8, 22, 'Entityxf', 366000, 183, 0.33, 0.9, 2.75, 15, 75, 0),
(9, 22, 'Emerus', 366000, 183, 0.378, 1.2, 2.78, 15, 75, 0),
(10, 22, 'Fmj', 380000, 190, 0.3655, 1.1, 2.7, 15, 75, 0),
(11, 22, 'Gp1', 360000, 180, 0.37, 1.2, 2.68, 15, 75, 0),
(12, 22, 'Italigtb2', 374000, 187, 0.34, 1.2, 2.55, 15, 75, 0),
(13, 22, 'Nero2', 380000, 190, 0.34005, 1.1, 2.675, 15, 75, 0),
(14, 22, 'Osiris', 360000, 180, 0.36, 1.05, 2.66, 15, 75, 0),
(15, 22, 'Penetrator', 352000, 176, 0.3, 0.8, 2.58, 15, 75, 0),
(16, 22, 'Pfister811', 380000, 190, 0.356, 1.12, 2.688, 15, 75, 0),
(17, 22, 'S80', 360000, 180, 0.3725, 1.25, 2.77, 15, 75, 0),
(18, 22, 'Sheava', 352000, 176, 0.33, 1, 2.65, 15, 75, 0),
(19, 22, 'Sultanrs', 352000, 176, 0.33, 1, 2.5, 15, 75, 0),
(20, 22, 'T20', 360000, 180, 0.365, 1.1, 2.68, 15, 75, 0),
(21, 22, 'Taipan', 388000, 194, 0.357, 1, 2.65, 15, 75, 0),
(22, 22, 'Tempesta', 352000, 176, 0.36, 1, 2.65, 15, 75, 0),
(23, 25, 'Stretch', 108800, 136, 0.17, 0.8, 1.85, 20, 75, 0),
(24, 22, 'Gt63samg', 520000, 260, 0.47, 1.28, 2.3, 15, 75, 0),
(25, 22, 'F812', 600000, 300, 0.545, 1.25, 2.15, 15, 75, 0),
(26, 23, 'Nrg500sa', 405000, 225, 0.525, 1.15, 2.6, 5, 30, 0),
(27, 23, 'Faggio', 20000, 100, 0.1975, 0.4, 1.7, 5, 30, 0),
(28, 23, 'Akuma', 34400, 172, 0.4, 1.2, 2.15, 5, 30, 0),
(29, 23, 'Avarus', 31600, 158, 0.27, 1, 1.85, 5, 30, 0),
(30, 23, 'Bagger', 27200, 136, 0.21, 1.2, 1.65, 5, 30, 0),
(31, 23, 'Bati', 35200, 176, 0.3, 1.4, 2.32, 5, 30, 0),
(32, 23, 'Bati2', 35200, 176, 0.3, 1.4, 2.32, 5, 30, 0),
(33, 23, 'Bf400', 31600, 158, 0.29, 1.1, 2.15, 5, 30, 0),
(34, 23, 'Carbonrs', 33800, 169, 0.3, 1.3, 2.15, 5, 30, 0),
(35, 23, 'Chimera', 25200, 126, 0.275, 1, 2.1, 5, 30, 0),
(36, 23, 'Cliffhanger', 35200, 176, 0.318, 1.1, 2.25, 5, 30, 0),
(37, 23, 'Daemon', 30800, 154, 0.26, 0.6, 1.85, 5, 30, 0),
(38, 23, 'Daemon2', 30800, 154, 0.262, 0.6, 1.85, 5, 30, 0),
(39, 23, 'Defiler', 35200, 176, 0.405, 1.2, 2.15, 5, 30, 0),
(40, 23, 'Diablous', 33800, 169, 0.312, 1.2, 1.95, 5, 30, 0),
(41, 23, 'Diablous2', 34400, 172, 0.32, 1.25, 2, 5, 30, 0),
(42, 23, 'Double', 34400, 172, 0.31, 1.4, 2.18, 5, 30, 0),
(43, 23, 'Enduro', 28800, 144, 0.3, 1.05, 2.16, 5, 30, 0),
(44, 23, 'Esskey', 31600, 158, 0.295, 1.2, 2.15, 5, 30, 0),
(45, 23, 'Faggio2', 17200, 86, 0.1, 0.4, 1.6, 5, 30, 0),
(46, 23, 'Faggio3', 19400, 97, 0.195, 0.4, 1.7, 5, 30, 0),
(47, 23, 'Fcr', 33000, 165, 0.305, 1.2, 2.1, 5, 30, 0),
(48, 23, 'Fcr2', 33800, 169, 0.31, 1.25, 2.15, 5, 30, 0),
(49, 23, 'Gargoyle', 34400, 172, 0.3125, 1.1, 2.25, 5, 30, 0),
(50, 23, 'Hakuchou', 36000, 180, 0.315, 1.4, 2.3, 5, 30, 0),
(51, 23, 'Hexer', 30800, 154, 0.26, 1, 1.85, 5, 30, 0),
(52, 23, 'Innovation', 32400, 162, 0.32, 1, 1.9, 5, 30, 0),
(53, 23, 'Lectro', 30800, 154, 0.28, 1.2, 1.95, 5, 30, 0),
(54, 23, 'Manchez', 31600, 158, 0.295, 1.2, 2.15, 5, 30, 0),
(55, 23, 'Nemesis', 32400, 162, 0.3, 1.2, 1.95, 5, 30, 0),
(56, 23, 'Nightblade', 33800, 169, 0.31, 1.2, 1.95, 5, 30, 0),
(57, 23, 'Pcj', 28800, 144, 0.26, 1.3, 2.05, 5, 30, 0),
(58, 23, 'Ratbike', 26600, 133, 0.215, 1.2, 1.65, 5, 30, 0),
(59, 23, 'Ruffian', 33800, 169, 0.34, 1.1, 1.95, 5, 30, 0),
(60, 23, 'Sanchez', 28000, 140, 0.28, 1.1, 2.15, 5, 30, 0),
(61, 23, 'Sanchez2', 28000, 140, 0.28, 1.1, 2.15, 5, 30, 0),
(62, 23, 'Sanctus', 33800, 169, 0.405, 1, 1.95, 5, 30, 0),
(63, 23, 'Sovereign', 31600, 158, 0.27, 1.1, 1.9, 5, 30, 0),
(64, 23, 'Stryder', 30200, 151, 0.36, 1.2, 2.45, 35, 55, 0),
(65, 23, 'Thrust', 35200, 176, 0.265, 1.5, 1.98, 5, 30, 0),
(66, 23, 'Vader', 30200, 151, 0.27, 1.1, 1.9, 5, 30, 0),
(67, 23, 'Vortex', 35200, 176, 0.4025, 1.2, 2.15, 5, 30, 0),
(68, 23, 'Wolfsbane', 26600, 133, 0.215, 1.2, 1.65, 5, 30, 0),
(69, 23, 'Zombiea', 32400, 162, 0.29, 0.8, 1.875, 5, 30, 0),
(70, 23, 'Zombieb', 32400, 162, 0.29, 0.8, 1.875, 5, 30, 0),
(71, 23, 'Manchez2', 29400, 147, 0.265, 0.8, 2.12, 35, 55, 0),
(72, 23, 'Blazer', 24400, 122, 0.2, 1, 2.6, 20, 80, 0),
(73, 23, 'Blazer2', 18000, 90, 0.12, 0.8, 2, 20, 80, 0),
(74, 23, 'Blazer3', 24400, 122, 0.2, 1, 2.6, 20, 80, 0),
(75, 23, 'Blazer4', 28000, 140, 0.25, 1, 2.7, 20, 80, 0),
(76, 23, 'Verus', 19400, 97, 0.135, 0.26, 2.1, 35, 55, 0),
(77, 24, 'Alpha', 252000, 180, 0.33, 1, 2.5, 20, 80, 0),
(78, 24, 'Banshee', 240800, 172, 0.34, 1, 2.42, 20, 80, 0),
(79, 24, 'Bestiagts', 240800, 172, 0.32, 1, 2.42, 20, 80, 0),
(80, 24, 'Blista2', 211400, 151, 0.23, 0.55, 2.1, 20, 80, 0),
(81, 24, 'Blista3', 211400, 151, 0.23, 0.55, 2.1, 20, 80, 0),
(82, 24, 'Buffalo', 226800, 162, 0.27, 0.9, 2.45, 20, 80, 0),
(83, 24, 'Buffalo2', 231000, 165, 0.29, 0.9, 2.45, 20, 80, 0),
(84, 24, 'Buffalo3', 240800, 172, 0.31, 1, 2.45, 20, 80, 0),
(85, 24, 'Comet2', 252000, 180, 0.34, 0.8, 2.6, 20, 80, 0),
(86, 24, 'Comet3', 252000, 180, 0.34, 0.8, 2.8, 20, 80, 0),
(87, 24, 'Comet4', 236600, 169, 0.2925, 0.8, 2.1, 20, 80, 0),
(88, 24, 'Comet5', 236600, 169, 0.32, 1.2, 2.7, 20, 80, 0),
(89, 24, 'Coquette', 252000, 180, 0.33, 0.8, 2.55, 20, 80, 0),
(90, 24, 'Coquette4', 246400, 176, 0.32, 0.6, 2.6, 35, 55, 0),
(91, 24, 'Drafter', 240800, 172, 0.342, 1, 2.695, 20, 80, 0),
(92, 24, 'Elegy', 236600, 169, 0.33, 1, 2.7, 20, 80, 0),
(93, 24, 'Elegy2', 246400, 176, 0.33, 0.5, 2.7, 20, 80, 0),
(94, 24, 'Feltzer2', 252000, 180, 0.34, 0.8, 2.65, 20, 80, 0),
(95, 24, 'Flashgt', 221200, 158, 0.32, 1, 2.45, 20, 80, 0),
(96, 24, 'Furoregt', 256200, 183, 0.335, 1, 2.56, 20, 80, 0),
(97, 24, 'Fusilade', 246400, 176, 0.32, 0.9, 2.45, 20, 80, 0),
(98, 24, 'Futo', 226800, 162, 0.29, 0.5, 2.05, 20, 80, 0),
(99, 24, 'Gb200', 226800, 162, 0.315, 1, 2.3, 20, 80, 0),
(100, 24, 'Komoda', 252000, 180, 0.367, 0.95, 2.69, 35, 55, 0),
(101, 24, 'Imorgon', 226800, 162, 0.66, 0.835, 2.598, 35, 55, 0),
(102, 24, 'Issi7', 211400, 151, 0.305, 1, 2.55, 20, 80, 0),
(103, 24, 'Italigto', 261800, 187, 0.4, 1.1, 2.62, 20, 80, 0),
(104, 24, 'Jugular', 246400, 176, 0.378, 1.1, 2.62, 20, 80, 0),
(105, 24, 'Jester', 231000, 165, 0.3, 0.95, 2.55, 20, 80, 0),
(106, 24, 'Jester2', 236600, 169, 0.31, 0.95, 2.57, 20, 80, 0),
(107, 24, 'Jester3', 240800, 172, 0.32, 0.95, 2.575, 20, 80, 0),
(108, 24, 'Khamelion', 161000, 115, 0.15, 0.9, 2.6, 20, 80, 0),
(109, 24, 'Kuruma', 236600, 169, 0.31, 0.5, 2.45, 20, 80, 0),
(110, 24, 'Kuruma2', 231000, 165, 0.31, 0.5, 2.25, 20, 80, 0),
(111, 24, 'Lynx', 246400, 176, 0.315, 1, 2.56, 20, 80, 0),
(112, 24, 'Massacro', 252000, 180, 0.364, 0.9, 2.42, 20, 80, 0),
(113, 24, 'Massacro2', 252000, 180, 0.364, 0.9, 2.43, 20, 80, 0),
(114, 24, 'Neo', 256200, 183, 0.387, 1.2, 2.62, 20, 80, 0),
(115, 24, 'Neon', 196000, 140, 0.2505, 1.3, 2.275, 20, 80, 0),
(116, 24, 'Ninef', 246400, 176, 0.33, 1, 2.55, 20, 80, 0),
(117, 24, 'Ninef2', 246400, 176, 0.33, 1, 2.55, 20, 80, 0),
(118, 24, 'Omnis', 221200, 158, 0.305, 1, 2, 20, 80, 0),
(119, 24, 'Paragon', 226800, 162, 0.329, 1, 2.675, 20, 80, 0),
(120, 24, 'Pariah', 246400, 176, 0.321, 1, 2.625, 20, 80, 0),
(121, 24, 'Penumbra', 205800, 147, 0.22, 0.8, 2.25, 20, 80, 0),
(122, 24, 'Penumbra2', 226800, 162, 0.3, 0.8, 2.35, 35, 55, 0),
(123, 24, 'Raiden', 190400, 136, 0.245, 1.3, 2.15, 20, 80, 0),
(124, 24, 'Rapidgt', 252000, 180, 0.36, 1, 2.45, 20, 80, 0),
(125, 24, 'Rapidgt2', 252000, 180, 0.36, 1, 2.45, 20, 80, 0),
(126, 24, 'Revolter', 226800, 162, 0.35, 0.8, 2.25, 20, 80, 0),
(127, 24, 'Schafter2', 196000, 140, 0.2, 0.9, 2.55, 20, 75, 0),
(128, 24, 'Schafter3', 252000, 180, 0.3, 0.95, 2.55, 20, 80, 0),
(129, 24, 'Schafter4', 196000, 140, 0.2, 0.85, 2.55, 20, 80, 0),
(130, 24, 'Schafter5', 252000, 180, 0.29, 0.92, 2.55, 20, 75, 0),
(131, 24, 'Schlagen', 261800, 187, 0.37, 0.8, 2.5, 20, 80, 0),
(132, 24, 'Schwarzer', 240800, 172, 0.29, 0.9, 2.3, 20, 80, 0),
(133, 24, 'Sentinel3', 226800, 162, 0.265, 0.8, 2.25, 20, 80, 0),
(134, 24, 'Seven70', 256200, 183, 0.335, 1, 2.56, 20, 80, 0),
(135, 24, 'Specter', 246400, 176, 0.32, 1, 2.62, 20, 80, 0),
(136, 24, 'Specter2', 246400, 176, 0.33, 1.1, 2.67, 20, 80, 0),
(137, 24, 'Streiter', 215600, 154, 0.2125, 0.8, 2.08, 20, 80, 0),
(138, 24, 'Sugoi', 226800, 162, 0.31, 0.85, 2.49, 35, 55, 0),
(139, 24, 'Sultan', 226800, 162, 0.26, 0.4, 2.35, 20, 80, 0),
(140, 24, 'Sultan2', 236600, 169, 0.33, 0.5, 2.495, 35, 55, 0),
(141, 24, 'Surano', 252000, 180, 0.34, 1, 2.55, 20, 80, 0),
(142, 24, 'Verlierer2', 252000, 180, 0.335, 1, 2.43, 20, 80, 0),
(143, 24, 'Vstr', 240800, 172, 0.379, 0.625, 2.58, 35, 55, 0),
(144, 24, 'Italirsx', 261800, 187, 0.40125, 1.35, 2.737, 35, 55, 0),
(145, 24, 'Zr350', 240800, 172, 0.33, 0.85, 2.485, 35, 55, 0),
(146, 24, 'Calico', 246400, 176, 0.3385, 0.825, 2.435, 35, 55, 0),
(147, 24, 'Futo2', 231000, 165, 0.2965, 0.525, 2.095, 35, 55, 0),
(148, 24, 'Euros', 236600, 169, 0.324, 0.9, 2.5615, 35, 55, 0),
(149, 24, 'Jester4', 240800, 172, 0.3291, 0.85, 2.59, 35, 55, 0),
(150, 24, 'Remus', 236600, 169, 0.327, 0.9, 2.685, 35, 55, 0),
(151, 24, 'Comet6', 252000, 180, 0.346, 0.88, 2.655, 35, 55, 0),
(152, 24, 'Growler', 252000, 180, 0.33375, 0.84, 2.63, 35, 55, 0),
(153, 24, 'Vectre', 221200, 158, 0.324, 0.6, 2.635, 35, 55, 0),
(154, 24, 'Cypher', 231000, 165, 0.326, 0.7, 2.59, 35, 55, 0),
(155, 24, 'Sultan3', 236600, 169, 0.3385, 0.52, 2.485, 35, 55, 0),
(156, 24, 'Rt3000', 240800, 172, 0.312, 1, 2.58, 35, 55, 0),
(157, 24, 'Btype', 211400, 151, 0.27, 0.55, 2.1, 20, 85, 0),
(158, 24, 'Btype3', 211400, 151, 0.27, 0.55, 2.1, 20, 85, 0),
(159, 24, 'Casco', 252000, 180, 0.32, 0.6, 2.3, 20, 85, 0),
(160, 24, 'Cheetah2', 252000, 180, 0.3, 0.8, 2.65, 20, 85, 0),
(161, 24, 'Coquette2', 252000, 180, 0.34, 0.5, 2.3, 20, 85, 0),
(162, 24, 'Deluxo', 215600, 154, 0.2275, 0.7, 2.05, 20, 85, 0),
(163, 24, 'Dynasty', 180600, 129, 0.18, 0.4, 1.7, 20, 85, 0),
(164, 24, 'Fagaloa', 170800, 122, 0.21, 0.775, 2.375, 20, 85, 0),
(165, 24, 'Feltzer3', 231000, 165, 0.3, 0.8, 2.35, 20, 85, 0),
(166, 24, 'Gt500', 231000, 165, 0.29, 0.77, 2.2, 20, 85, 0),
(167, 24, 'Infernus2', 240800, 172, 0.33, 0.5, 2.635, 20, 85, 0),
(168, 24, 'Jb7002', 252000, 180, 0.26, 0.6, 2.15, 35, 55, 0),
(169, 24, 'Mamba', 246400, 176, 0.34, 0.5, 2.5, 20, 85, 0),
(170, 24, 'Manana', 186200, 133, 0.16, 0.25, 1.95, 20, 85, 0),
(171, 24, 'Manana2', 221200, 158, 0.24, 0.275, 1.98, 35, 55, 0),
(172, 24, 'Michelli', 226800, 162, 0.2825, 0.75, 2.3, 20, 85, 0),
(173, 24, 'Monroe', 252000, 180, 0.28, 0.65, 2.2, 20, 85, 0),
(174, 24, 'Nebula', 190400, 136, 0.24, 0.5, 1.95, 20, 85, 0),
(175, 24, 'Peyote', 186200, 133, 0.16, 0.25, 1.85, 20, 85, 0),
(176, 24, 'Peyote3', 215600, 154, 0.205, 0.28, 1.98, 35, 55, 0),
(177, 24, 'Pigalle', 252000, 180, 0.265, 0.85, 2.36, 20, 85, 0),
(178, 24, 'Rapidgt3', 236600, 169, 0.3, 0.5, 2.6, 20, 85, 0),
(179, 24, 'Retinue', 215600, 154, 0.2275, 0.7, 2.05, 20, 85, 0),
(180, 24, 'Savestra', 221200, 158, 0.2375, 0.7, 2.05, 20, 85, 0),
(181, 24, 'Stinger', 226800, 162, 0.26, 0.6, 2.15, 20, 85, 0),
(182, 24, 'Stingergt', 226800, 162, 0.26, 0.6, 2.15, 20, 85, 0),
(183, 24, 'Viseris', 236600, 169, 0.3, 0.8, 2.3, 20, 85, 0),
(184, 24, 'Z190', 221200, 158, 0.27, 0.95, 2.3, 20, 85, 0),
(185, 24, 'Zion3', 226800, 162, 0.305, 0.85, 2.35, 20, 85, 0),
(186, 24, 'Cheburek', 226800, 162, 0.265, 0.8, 2.25, 20, 85, 0),
(187, 25, 'Baller', 115200, 144, 0.21, 0.6, 1.9, 45, 100, 0),
(188, 25, 'Baller2', 129600, 162, 0.27, 0.6, 2, 45, 100, 0),
(189, 25, 'Baller3', 129600, 162, 0.275, 0.6, 2, 45, 100, 0),
(190, 25, 'Baller4', 129600, 162, 0.27, 0.57, 2, 45, 100, 0),
(191, 25, 'Bjxl', 103200, 129, 0.19, 0.8, 2.05, 45, 100, 0),
(192, 25, 'Cavalcade', 108800, 136, 0.2, 0.6, 1.9, 45, 100, 0),
(193, 25, 'Cavalcade2', 108800, 136, 0.2, 0.6, 1.9, 45, 100, 0),
(194, 25, 'Contender', 117600, 147, 0.26, 0.6, 2.1, 45, 100, 0),
(195, 25, 'Dubsta', 108800, 136, 0.2, 0.8, 2.15, 45, 100, 0),
(196, 25, 'Dubsta2', 108800, 136, 0.2, 0.8, 2.15, 45, 100, 0),
(197, 25, 'Fq2', 115200, 144, 0.18, 0.25, 2, 45, 100, 0),
(198, 25, 'Granger', 103200, 129, 0.19, 0.8, 2.15, 45, 100, 0),
(199, 25, 'Gresley', 108800, 136, 0.2, 0.6, 1.9, 45, 100, 0),
(200, 25, 'Habanero', 115200, 144, 0.18, 0.25, 2.1, 45, 100, 0),
(201, 25, 'Huntley', 129600, 162, 0.265, 0.55, 2.1, 45, 100, 0),
(202, 25, 'Landstalker', 106400, 133, 0.18, 0.8, 2.1, 45, 100, 0),
(203, 25, 'Landstalker2', 108800, 136, 0.19, 0.8, 2.15, 35, 55, 0),
(204, 25, 'Mesa', 100800, 126, 0.17, 0.3, 2, 45, 100, 0),
(205, 25, 'Novak', 135200, 169, 0.3, 0.8, 2.3, 45, 100, 0),
(206, 25, 'Patriot', 108800, 136, 0.2, 0.32, 1.7, 45, 100, 0),
(207, 25, 'Patriot2', 103200, 129, 0.18, 0.32, 1.6, 45, 100, 0),
(208, 25, 'Radi', 112000, 140, 0.2, 0.8, 2.25, 45, 100, 0),
(209, 25, 'Rebla', 135200, 169, 0.335, 0.85, 2.195, 35, 55, 0),
(210, 25, 'Rocoto', 117600, 147, 0.19, 0.25, 2.1, 45, 100, 0),
(211, 25, 'Seminole', 106400, 133, 0.18, 0.8, 2.05, 45, 100, 0),
(212, 25, 'Seminole2', 112000, 140, 0.2, 0.8, 2, 35, 55, 0),
(213, 25, 'Serrano', 115200, 144, 0.2, 0.4, 2.1, 45, 100, 0),
(214, 25, 'Toros', 140800, 176, 0.32, 0.8, 2.3, 45, 100, 0),
(215, 25, 'Xls', 126400, 158, 0.26, 0.58, 2, 45, 100, 0),
(216, 25, 'Xls2', 126400, 158, 0.265, 0.59, 2.05, 45, 100, 0),
(217, 25, 'Asea', 108800, 136, 0.2, 0.4, 2.05, 20, 75, 0),
(218, 25, 'Asterope', 108800, 136, 0.2, 0.9, 2.5, 20, 75, 0),
(219, 25, 'Cog55', 132000, 165, 0.265, 0.57, 2.2, 20, 75, 0),
(220, 25, 'Cog552', 132000, 165, 0.26, 0.55, 2.2, 20, 75, 0),
(221, 25, 'Cognoscenti', 132000, 165, 0.26, 0.55, 2.1, 20, 75, 0),
(222, 25, 'Cognoscenti2', 129600, 162, 0.255, 0.52, 2.1, 20, 75, 0),
(223, 25, 'Emperor', 97600, 122, 0.14, 0.6, 1.9, 20, 75, 0),
(224, 25, 'Fugitive', 115200, 144, 0.2, 0.9, 2.5, 20, 75, 0),
(225, 25, 'Emperor2', 97600, 122, 0.14, 0.6, 1.9, 20, 75, 0),
(226, 25, 'Glendale', 115200, 144, 0.235, 0.65, 2.05, 20, 75, 0),
(227, 25, 'Glendale2', 115200, 144, 0.233, 0.625, 2.25, 35, 55, 0),
(228, 25, 'Ingot', 92000, 115, 0.14, 0.6, 1.95, 35, 55, 0),
(229, 25, 'Intruder', 112000, 140, 0.2, 0.9, 2.35, 20, 75, 0),
(230, 25, 'Premier', 108800, 136, 0.2, 0.6, 2.1, 20, 75, 0),
(231, 25, 'Primo', 108800, 136, 0.2, 0.9, 2.35, 20, 75, 0),
(232, 25, 'Primo2', 108800, 136, 0.2, 0.9, 2.35, 20, 75, 0),
(233, 25, 'Regina', 88800, 111, 0.14, 0.6, 1.9, 20, 75, 0),
(234, 25, 'Stafford', 108800, 136, 0.2, 0.45, 2, 20, 75, 0),
(235, 25, 'Stanier', 123200, 154, 0.2, 0.9, 2.45, 20, 75, 0),
(236, 25, 'Stratum', 120800, 151, 0.21, 0.6, 2.2, 20, 75, 0),
(237, 25, 'Superd', 129600, 162, 0.26, 0.6, 2.1, 20, 75, 0),
(238, 25, 'Surge', 68800, 86, 0.1, 0.6, 2.15, 20, 75, 0),
(239, 25, 'Tailgater', 108800, 136, 0.2, 0.9, 2.55, 20, 75, 0),
(240, 25, 'Warrener', 108800, 136, 0.245, 0.95, 2.16, 20, 75, 0),
(241, 25, 'Washington', 123200, 154, 0.2, 0.9, 2.45, 20, 75, 0),
(242, 25, 'Tailgater2', 135200, 169, 0.2775, 0.88, 2.595, 35, 55, 0),
(243, 26, 'Asbo', 136000, 136, 0.234, 0.47, 1.92, 35, 55, 0),
(244, 26, 'Blista', 151000, 151, 0.23, 0.6, 2.05, 35, 55, 0),
(245, 26, 'Brioso', 151000, 151, 0.29, 0.6, 2.3, 35, 55, 0),
(246, 26, 'Club', 151000, 151, 0.2375, 0.72, 2.05, 35, 55, 0),
(247, 26, 'Dilettante', 86000, 86, 0.1, 0.6, 1.76, 35, 55, 0),
(248, 26, 'Kanjo', 158000, 158, 0.32, 0.5, 1.97, 35, 55, 0),
(249, 26, 'Issi2', 151000, 151, 0.23, 0.6, 2.05, 35, 55, 0),
(250, 26, 'Panto', 147000, 147, 0.27, 0.6, 1.97, 35, 55, 0),
(251, 26, 'Rhapsody', 147000, 147, 0.23, 0.6, 2.05, 35, 55, 0),
(252, 26, 'Brioso2', 118000, 118, 0.179, 0.25, 1.885, 35, 55, 0),
(253, 26, 'Weevil', 118000, 118, 0.188, 0.26, 1.78, 35, 55, 0),
(254, 26, 'Cogcabrio', 162000, 162, 0.26, 0.6, 2.3, 25, 65, 0),
(255, 26, 'Exemplar', 172000, 172, 0.26, 0.9, 2.6, 25, 65, 0),
(256, 26, 'F620', 172000, 172, 0.24, 0.9, 2.5, 25, 65, 0),
(257, 26, 'Felon', 165000, 165, 0.24, 0.9, 2.55, 25, 65, 0),
(258, 26, 'Felon2', 154000, 154, 0.24, 0.9, 2.5, 25, 65, 0),
(259, 26, 'Jackal', 165000, 165, 0.22, 0.9, 2.6, 25, 65, 0),
(260, 26, 'Oracle', 162000, 162, 0.26, 0.9, 2.25, 25, 65, 0),
(261, 26, 'Oracle2', 165000, 165, 0.27, 0.9, 2.25, 25, 65, 0),
(262, 26, 'Sentinel', 162000, 162, 0.21, 0.9, 2.45, 25, 65, 0),
(263, 26, 'Sentinel2', 162000, 162, 0.21, 0.9, 2.45, 25, 65, 0),
(264, 26, 'Windsor', 172000, 172, 0.28, 0.7, 2.2, 25, 65, 0),
(265, 26, 'Zion', 169000, 169, 0.22, 0.9, 2.6, 25, 65, 0),
(266, 26, 'Zion2', 169000, 169, 0.22, 0.9, 2.6, 25, 65, 0),
(267, 26, 'Previon', 169000, 169, 0.3185, 0.85, 2.4865, 35, 55, 0),
(268, 27, 'Blade', 151000, 151, 0.324, 0.8, 2.23, 20, 60, 0),
(269, 27, 'Buccaneer', 165000, 165, 0.28, 0.8, 2.15, 20, 60, 0),
(270, 27, 'Buccaneer2', 165000, 165, 0.28, 0.8, 2.15, 20, 60, 0),
(271, 27, 'Chino', 126000, 126, 0.2, 0.6, 2.05, 20, 60, 0),
(272, 27, 'Chino2', 129000, 129, 0.21, 0.6, 2.07, 20, 60, 0),
(273, 27, 'Clique', 165000, 165, 0.3, 0.85, 2.35, 20, 60, 0),
(274, 27, 'Coquette3', 162000, 162, 0.29, 0.6, 2.25, 20, 60, 0),
(275, 27, 'Deviant', 151000, 151, 0.29, 0.5, 2.25, 20, 60, 0),
(276, 27, 'Dominator', 172000, 172, 0.29, 0.8, 2.25, 20, 60, 0),
(277, 27, 'Dominator3', 169000, 169, 0.335, 0.5, 2.57, 20, 60, 0),
(278, 27, 'Dukes', 172000, 172, 0.32, 0.8, 2.25, 20, 60, 0),
(279, 27, 'Faction', 169000, 169, 0.28, 0.8, 2.25, 20, 60, 0),
(280, 27, 'Faction2', 169000, 169, 0.28, 0.8, 2.25, 20, 60, 0),
(281, 27, 'Faction3', 126000, 126, 0.2, 0.8, 2.35, 20, 60, 0),
(282, 27, 'Gauntlet', 169000, 169, 0.3, 0.9, 2.5, 20, 60, 0),
(283, 27, 'Gauntlet3', 158000, 158, 0.28, 0.9, 2.5, 20, 60, 0),
(284, 27, 'Gauntlet4', 176000, 176, 0.36, 0.9, 2.35, 20, 60, 0),
(285, 27, 'Gauntlet5', 172000, 172, 0.29, 0.9, 2.25, 35, 55, 0),
(286, 27, 'Hermes', 147000, 147, 0.285, 0.775, 2.375, 20, 60, 0),
(287, 27, 'Hotknife', 162000, 162, 0.3, 0.43, 1.85, 20, 60, 0),
(288, 27, 'Hustler', 158000, 158, 0.3, 0.43, 1.95, 20, 60, 0),
(289, 27, 'Impaler', 162000, 162, 0.31, 0.6, 2.35, 20, 60, 0),
(290, 27, 'Impaler2', 183000, 183, 0.38, 0.7, 2.4, 20, 60, 0),
(291, 27, 'Imperator', 176000, 176, 0.365, 0.5, 2.25, 20, 60, 0),
(292, 27, 'Imperator2', 176000, 176, 0.365, 0.5, 2.25, 20, 60, 0),
(293, 27, 'Moonbeam', 151000, 151, 0.21, 0.4, 2, 20, 60, 0),
(294, 27, 'Moonbeam2', 151000, 151, 0.21, 0.4, 2, 20, 60, 0),
(295, 27, 'Nightshade', 144000, 144, 0.25, 0.6, 2.25, 20, 60, 0),
(296, 27, 'Phoenix', 169000, 169, 0.28, 0.8, 2.15, 20, 60, 0),
(297, 27, 'Picador', 147000, 147, 0.22, 0.8, 2.05, 20, 60, 0),
(298, 26, 'Ratloader', 133000, 133, 0.22, 0.4, 1.65, 20, 60, 0),
(299, 27, 'Ratloader2', 140000, 140, 0.24, 0.7, 1.75, 20, 60, 0),
(300, 27, 'Ruiner', 172000, 172, 0.28, 0.8, 2.2, 20, 60, 0),
(301, 27, 'Sabregt', 169000, 169, 0.28, 0.8, 2.25, 20, 60, 0),
(302, 27, 'Sabregt2', 169000, 169, 0.282, 0.82, 2.26, 20, 60, 0),
(303, 27, 'Slamvan', 144000, 144, 0.245, 0.6, 1.65, 20, 60, 0),
(304, 27, 'Slamvan2', 144000, 144, 0.25, 0.7, 1.85, 20, 60, 0),
(305, 27, 'Slamvan3', 144000, 144, 0.25, 0.6, 2.35, 20, 60, 0),
(306, 27, 'Stalion', 162000, 162, 0.29, 0.7, 2.25, 20, 60, 0),
(307, 27, 'Tampa', 151000, 151, 0.27, 0.8, 2.25, 20, 60, 0),
(308, 27, 'Tulip', 176000, 176, 0.32, 0.5, 2.25, 20, 60, 0),
(309, 27, 'Vamos', 165000, 165, 0.33, 0.5, 2.25, 20, 60, 0),
(310, 27, 'Vigero', 169000, 169, 0.29, 0.8, 2.05, 20, 60, 0),
(311, 27, 'Virgo2', 129000, 129, 0.211, 0.72, 2.1, 20, 60, 0),
(312, 27, 'Virgo3', 129000, 129, 0.21, 0.7, 2.05, 20, 60, 0),
(313, 27, 'Voodoo', 144000, 144, 0.18, 0.7, 1.85, 20, 60, 0),
(314, 27, 'Voodoo2', 136000, 136, 0.17, 0.25, 1.85, 20, 60, 0),
(315, 27, 'Yosemite', 151000, 151, 0.285, 0.75, 2.375, 20, 60, 0),
(316, 27, 'Yosemite2', 158000, 158, 0.395, 0.85, 2.625, 35, 55, 0),
(317, 27, 'Yosemite3', 144000, 144, 0.275, 0.48, 2.1, 35, 55, 0),
(318, 27, 'Dominator7', 180000, 180, 0.342, 0.92, 2.535, 35, 55, 0),
(319, 28, 'Bfinjection', 176400, 147, 0.22, 0.62, 1.85, 20, 80, 0),
(320, 28, 'Bifta', 194400, 162, 0.26, 0.7, 2.05, 20, 80, 0),
(321, 28, 'Bodhi2', 154800, 129, 0.215, 1.1, 2.25, 20, 80, 0),
(322, 28, 'Brawler', 202800, 169, 0.28, 0.88, 1.92, 20, 80, 0),
(323, 28, 'Caracara2', 168000, 140, 0.27, 0.3, 2.05, 20, 80, 0),
(324, 29, 'Dloader', 141600, 118, 0.17, 0.6, 1.7, 20, 80, 0),
(325, 28, 'Dubsta3', 168000, 140, 0.28, 0.6, 2, 20, 80, 0),
(326, 28, 'Dune', 168000, 140, 0.25, 0.63, 2.2, 20, 80, 0),
(327, 28, 'Everon', 181200, 151, 0.295, 0.3, 2.05, 35, 55, 0),
(328, 28, 'Hellion', 172800, 144, 0.25, 0.3, 2, 20, 80, 0),
(329, 28, 'Kalahari', 151200, 126, 0.26, 1, 1.78, 20, 80, 0),
(330, 28, 'Kamacho', 176400, 147, 0.255, 0.3, 2, 20, 80, 0),
(331, 28, 'Mesa3', 151200, 126, 0.17, 0.3, 2, 20, 80, 0),
(332, 28, 'Rancherxl', 151200, 126, 0.18, 0.8, 1.95, 20, 80, 0),
(333, 28, 'Rebel', 163200, 136, 0.2, 0.6, 2.05, 20, 80, 0),
(334, 28, 'Rebel2', 163200, 136, 0.2, 0.6, 2.05, 20, 80, 0),
(335, 28, 'Riata', 172800, 144, 0.25, 0.3, 2.05, 20, 80, 0),
(336, 28, 'Sandking', 163200, 136, 0.2, 0.6, 2, 20, 80, 0),
(337, 28, 'Sandking2', 163200, 136, 0.2, 0.6, 2, 20, 80, 0),
(338, 28, 'Winky', 129600, 108, 0.15, 0.26, 1.85, 35, 55, 0),
(339, 29, 'Benson', 159600, 133, 0.16, 0.25, 1.75, 100, 150, 0),
(340, 29, 'Mule', 124800, 104, 0.11, 0.25, 1.5, 100, 150, 0),
(341, 29, 'Mule2', 124800, 104, 0.11, 0.25, 1.5, 100, 150, 0),
(342, 29, 'Mule3', 138000, 115, 0.17, 0.25, 1.5, 100, 150, 0),
(343, 29, 'Pounder2', 159600, 133, 0.16, 0.25, 1.5, 100, 150, 0),
(344, 29, 'Bison', 163200, 136, 0.2, 0.6, 2.05, 85, 60, 0),
(345, 29, 'Bobcatxl', 151200, 126, 0.18, 0.8, 1.95, 85, 60, 0),
(346, 29, 'Boxville2', 124800, 104, 0.11, 0.25, 1.55, 85, 60, 0),
(347, 29, 'Burrito3', 159600, 133, 0.16, 0.6, 1.95, 85, 60, 0),
(348, 29, 'Gburrito2', 172800, 144, 0.18, 0.7, 2.05, 85, 60, 0),
(349, 29, 'Pony', 159600, 133, 0.16, 0.6, 1.95, 85, 60, 0),
(350, 29, 'Rumpo3', 154800, 129, 0.18, 0.3, 2, 85, 60, 0),
(351, 29, 'Speedo', 172800, 144, 0.18, 0.6, 1.8, 85, 60, 0),
(352, 29, 'Youga', 146400, 122, 0.14, 0.3, 1.8, 85, 60, 0),
(353, 29, 'Youga2', 146400, 122, 0.14, 0.3, 1.8, 85, 60, 0),
(354, 29, 'Youga3', 163200, 136, 0.17, 0.3, 1.95, 35, 55, 0),
(355, 30, 'Bmx', 2700, 54, 0.16, 3, 1.85, 0, 0, 0),
(356, 30, 'Cruiser', 2700, 54, 0.08, 2.8, 1.8, 0, 0, 0),
(357, 30, 'Fixter', 3200, 64, 0.135, 0.4, 1.85, 0, 0, 0),
(358, 30, 'Scorcher', 2700, 54, 0.17, 2.8, 2.05, 0, 0, 0),
(359, 30, 'Tribike', 3750, 75, 0.135, 2.5, 1.85, 0, 0, 0),
(360, 30, 'Tribike2', 3750, 75, 0.135, 2.5, 1.85, 0, 0, 0),
(361, 30, 'Tribike3', 3750, 75, 0.135, 2.5, 1.85, 0, 0, 0),
(362, 29, 'Trailers', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(363, 29, 'Trailers2', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(364, 29, 'Trailers3', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(365, 29, 'Trailers4', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(366, 29, 'Tanker', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(367, 29, 'Freighttrailer', 60000, 1, 0.001, 0.01, 0.01, 200, 0, 0),
(368, 29, 'Packer', 154800, 129, 0.21, 0.8, 1.55, 35, 55, 0),
(369, 29, 'Phantom', 146400, 122, 0.21, 0.8, 1.65, 35, 55, 0),
(370, 29, 'Phantom3', 184800, 151, 0.3, 0.85, 2.05, 35, 55, 0),
(371, 29, 'Pounder', 146400, 122, 0.14, 0.25, 1.65, 35, 55, 0),
(372, 31, 'Jetmax', 259200, 162, 17, 0.4, 0, 55, 125, 0),
(373, 31, 'Marquis', 57600, 36, 2.5, 0.4, 0, 55, 125, 0),
(374, 31, 'Seashark', 246400, 154, 12.5, 0.4, 0, 55, 125, 0),
(375, 31, 'Speeder', 252800, 158, 16, 0.4, 0, 55, 125, 0),
(376, 31, 'Squalo', 212800, 133, 11.5, 0.4, 0, 55, 125, 0),
(377, 31, 'Toro', 252800, 158, 18, 0.4, 0, 55, 125, 0),
(378, 31, 'Tropic', 217600, 136, 13, 0.4, 0, 55, 125, 0),
(379, 31, 'Longfin', 270400, 169, 18, 0.4, 0, 35, 55, 0),
(380, 32, 'Cuban800', 941800, 277, 5.88, 4.51559, 2.15, 60, 215, 0),
(381, 32, 'Luxor2', 1111800, 327, 8.036, 7.33085, 1.15, 60, 215, 0),
(382, 32, 'Mammatus', 843200, 248, 4.9, 3.39901, 2.15, 60, 215, 0),
(383, 32, 'Nimbus', 1162800, 342, 8.134, 7.6912, 1.15, 60, 215, 0),
(384, 32, 'Shamal', 1111800, 327, 7.938, 7.19298, 1.15, 60, 215, 0),
(385, 32, 'Velum', 918000, 270, 5.586, 4.16918, 2.15, 60, 215, 0),
(386, 32, 'Buzzard2', 707200, 208, 5.39, 3.1212, 1.3, 60, 175, 0),
(387, 32, 'Frogger', 697000, 205, 5.488, 3.10995, 1.3, 60, 175, 0),
(388, 29, 'utillitruck2', 60000, 108, 0.12, 0.25, 1.6, 90, 65, 0),
(389, 29, 'utillitruck3', 60000, 108, 0.12, 0.25, 1.6, 90, 65, 0),
(390, 29, 'towtruck', 60000, 126, 0.15, 0.25, 1.6, 90, 65, 0),
(391, 29, 'towtruck2', 60000, 118, 0.15, 0.25, 1.45, 90, 65, 0),
(392, 29, 'bus', 60000, 93, 0.12, 0.35, 1.45, 100, 125, 0),
(393, 29, 'coach', 60000, 93, 0.12, 0.25, 1.45, 100, 125, 0),
(394, 29, 'rentalbus', 60000, 93, 0.12, 0.25, 1.55, 100, 125, 0),
(395, 29, 'tourbus', 60000, 93, 0.12, 0.25, 1.55, 100, 125, 0),
(397, 26, 'taxi', 50000, 154, 0.2, 0.9, 2.55, 100, 125, 0),
(398, 29, 'stockade', 60000, 122, 0.12, 0.25, 1.65, 100, 150, 0),
(399, 32, 'Conada', 170000, 223, 5.6644, 3.5215, 1.3, 35, 55, 0),
(400, 22, 'Corsita', 100000, 187, 0.4, 1.3, 2.726, 35, 55, 0),
(401, 22, 'Omnisegt', 100000, 172, 0.58, 1, 2.595, 35, 55, 0),
(402, 22, 'Sm722', 100000, 172, 0.358, 0.85, 2.508, 35, 55, 0),
(403, 22, 'Tenf', 100000, 176, 0.3545, 1.1, 2.68, 35, 55, 0),
(404, 22, 'Tenf2', 100000, 176, 0.356, 1.15, 2.69, 35, 55, 0),
(405, 22, 'Torero2', 100000, 187, 0.385, 1.15, 2.7695, 35, 55, 0),
(406, 26, 'Brioso3', 50000, 133, 0.216, 0.25, 1.995, 35, 55, 0),
(407, 27, 'Vigero2', 50000, 176, 0.3645, 0.95, 2.585, 35, 55, 0),
(408, 27, 'Weevil2', 50000, 183, 0.3375, 0.775, 2.1075, 35, 55, 0),
(409, 27, 'Ruiner4', 50000, 172, 0.3085, 0.82, 2.36, 35, 55, 0),
(410, 27, 'Greenwood', 50000, 172, 0.282, 0.7, 2.3, 35, 55, 0),
(411, 27, 'Draugur', 50000, 158, 0.3425, 0.484, 2.485, 35, 55, 0),
(413, 24, 'Draftgpr', 70000, 184, 0.34005, 1.4, 2.675, 35, 55, 0),
(414, 24, 'Komodafr', 70000, 192, 0.335, 1, 2.45, 35, 55, 0),
(416, 22, 'Audia6', 100000, 230, 4.6, 3.34, 2.3, 35, 55, 0),
(417, 29, 'Flatbed', 60000, 100, 0.14, 0.25, 1.65, 200, 0, 0),
(418, 25, 'boor', 40000, 144, 0.267, 0.68, 2.065, 35, 55, 0),
(419, 29, 'brickade2', 60000, 118, 0.2, 0.4, 2, 35, 55, 0),
(420, 27, 'broadway', 50000, 133, 0.195, 0.32, 1.995, 35, 55, 0),
(421, 32, 'cargoplane2', 170000, 284, 6.174, 4.87143, 0.85, 35, 55, 0),
(422, 27, 'eudora', 50000, 147, 0.195, 0.265, 1.89, 35, 55, 0),
(423, 27, 'tahoma', 50000, 158, 0.298, 0.625, 2.308, 35, 55, 0),
(424, 27, 'tulip2', 50000, 162, 0.3153, 0.68, 2.33, 35, 55, 0),
(425, 23, 'manchez3', 10000, 151, 0.272, 0.8, 2.11, 35, 55, 0),
(426, 23, 'powersurge', 10000, 154, 0.82, 1.3, 2.365, 35, 55, 0),
(427, 22, 'entity3', 100000, 194, 0.3545, 1, 2.782, 35, 55, 0),
(428, 22, 'virtue', 100000, 162, 0.3418, 1.28, 2.588, 35, 55, 0),
(430, 29, 'surfer3', 60000, 75, 0.1, 0.3, 1.55, 35, 55, 0),
(431, 25, 'issi8', 40000, 172, 0.3215, 0.548, 2.1495, 35, 55, 0),
(432, 29, 'journey2', 60000, 115, 0.13, 0.25, 1.4, 35, 55, 0),
(433, 24, 'everon2', 70000, 172, 0.3395, 0.685, 2.5445, 35, 55, 0),
(434, 24, 'panthere', 70000, 180, 0.3462, 0.84, 2.5814, 35, 55, 0),
(435, 24, 'r300', 70000, 172, 0.3288, 0.78, 2.495, 35, 55, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `weapons`
--

CREATE TABLE `weapons` (
  `id` int(11) NOT NULL,
  `ident` varchar(12) NOT NULL,
  `name` varchar(35) NOT NULL,
  `shop` varchar(64) NOT NULL,
  `weaponname` varchar(64) NOT NULL,
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `whitelist`
--

CREATE TABLE `whitelist` (
  `id` int(11) NOT NULL,
  `name` varchar(35) NOT NULL,
  `socialclubid` int(15) NOT NULL,
  `timestamp` int(11) NOT NULL DEFAULT 1609462861
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `adminlogs`
--
ALTER TABLE `adminlogs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `adminlogsnames`
--
ALTER TABLE `adminlogsnames`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `adminsettings`
--
ALTER TABLE `adminsettings`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `animations`
--
ALTER TABLE `animations`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indizes für die Tabelle `bank`
--
ALTER TABLE `bank`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `bankfile`
--
ALTER TABLE `bankfile`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `banksettings`
--
ALTER TABLE `banksettings`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `bans`
--
ALTER TABLE `bans`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `business`
--
ALTER TABLE `business`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `busroutes`
--
ALTER TABLE `busroutes`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `cctvs`
--
ALTER TABLE `cctvs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `characters`
--
ALTER TABLE `characters`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `coupons`
--
ALTER TABLE `coupons`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `coupon` (`coupon`);

--
-- Indizes für die Tabelle `documents`
--
ALTER TABLE `documents`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `doors`
--
ALTER TABLE `doors`
  ADD PRIMARY KEY (`doorsid`),
  ADD KEY `id` (`id`);

--
-- Indizes für die Tabelle `drugs`
--
ALTER TABLE `drugs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `factionbudgets`
--
ALTER TABLE `factionbudgets`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `factions`
--
ALTER TABLE `factions`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `factionsalary`
--
ALTER TABLE `factionsalary`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `factionsrangs`
--
ALTER TABLE `factionsrangs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `fahndungen`
--
ALTER TABLE `fahndungen`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `fahndungskommentare`
--
ALTER TABLE `fahndungskommentare`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `failed_jobs`
--
ALTER TABLE `failed_jobs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`);

--
-- Indizes für die Tabelle `fire`
--
ALTER TABLE `fire`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `furniture`
--
ALTER TABLE `furniture`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `furniturecategories`
--
ALTER TABLE `furniturecategories`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `furniturehouse`
--
ALTER TABLE `furniturehouse`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `gangzones`
--
ALTER TABLE `gangzones`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indizes für die Tabelle `garbageroutes`
--
ALTER TABLE `garbageroutes`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `globalitems`
--
ALTER TABLE `globalitems`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `groupsrangs`
--
ALTER TABLE `groupsrangs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `groups_members`
--
ALTER TABLE `groups_members`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `houseinteriors`
--
ALTER TABLE `houseinteriors`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `houses`
--
ALTER TABLE `houses`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `inactiv`
--
ALTER TABLE `inactiv`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `invoices`
--
ALTER TABLE `invoices`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `itemmodels`
--
ALTER TABLE `itemmodels`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `lifeinvaderads`
--
ALTER TABLE `lifeinvaderads`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `logs`
--
ALTER TABLE `logs`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `namechanges`
--
ALTER TABLE `namechanges`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `outfits`
--
ALTER TABLE `outfits`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `password_resets`
--
ALTER TABLE `password_resets`
  ADD KEY `password_resets_email_index` (`email`);

--
-- Indizes für die Tabelle `paydays`
--
ALTER TABLE `paydays`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  ADD KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`);

--
-- Indizes für die Tabelle `policefile`
--
ALTER TABLE `policefile`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `screenshots`
--
ALTER TABLE `screenshots`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `settings`
--
ALTER TABLE `settings`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `shopitems`
--
ALTER TABLE `shopitems`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `smartphonecalls`
--
ALTER TABLE `smartphonecalls`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `smartphonemessages`
--
ALTER TABLE `smartphonemessages`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `smartphones`
--
ALTER TABLE `smartphones`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `spedvehicles`
--
ALTER TABLE `spedvehicles`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `speedcameras`
--
ALTER TABLE `speedcameras`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `standingorder`
--
ALTER TABLE `standingorder`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `tattoos`
--
ALTER TABLE `tattoos`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `taxiroutes`
--
ALTER TABLE `taxiroutes`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `tickets`
--
ALTER TABLE `tickets`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `ticket_answers`
--
ALTER TABLE `ticket_answers`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `ticket_user`
--
ALTER TABLE `ticket_user`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `timeline`
--
ALTER TABLE `timeline`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `transfer`
--
ALTER TABLE `transfer`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `userfile`
--
ALTER TABLE `userfile`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `userlog`
--
ALTER TABLE `userlog`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `vehicles`
--
ALTER TABLE `vehicles`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `vehicleshop`
--
ALTER TABLE `vehicleshop`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `weapons`
--
ALTER TABLE `weapons`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `whitelist`
--
ALTER TABLE `whitelist`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `adminlogs`
--
ALTER TABLE `adminlogs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `adminlogsnames`
--
ALTER TABLE `adminlogsnames`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `adminsettings`
--
ALTER TABLE `adminsettings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `animations`
--
ALTER TABLE `animations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=335;

--
-- AUTO_INCREMENT für Tabelle `bank`
--
ALTER TABLE `bank`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `bankfile`
--
ALTER TABLE `bankfile`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `banksettings`
--
ALTER TABLE `banksettings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `bans`
--
ALTER TABLE `bans`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `business`
--
ALTER TABLE `business`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;

--
-- AUTO_INCREMENT für Tabelle `busroutes`
--
ALTER TABLE `busroutes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT für Tabelle `cctvs`
--
ALTER TABLE `cctvs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `characters`
--
ALTER TABLE `characters`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `coupons`
--
ALTER TABLE `coupons`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `documents`
--
ALTER TABLE `documents`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `doors`
--
ALTER TABLE `doors`
  MODIFY `doorsid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=123;

--
-- AUTO_INCREMENT für Tabelle `drugs`
--
ALTER TABLE `drugs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `factionbudgets`
--
ALTER TABLE `factionbudgets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `factions`
--
ALTER TABLE `factions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `factionsalary`
--
ALTER TABLE `factionsalary`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `factionsrangs`
--
ALTER TABLE `factionsrangs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `fahndungen`
--
ALTER TABLE `fahndungen`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `fahndungskommentare`
--
ALTER TABLE `fahndungskommentare`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `failed_jobs`
--
ALTER TABLE `failed_jobs`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `fire`
--
ALTER TABLE `fire`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=471;

--
-- AUTO_INCREMENT für Tabelle `furniture`
--
ALTER TABLE `furniture`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=474;

--
-- AUTO_INCREMENT für Tabelle `furniturecategories`
--
ALTER TABLE `furniturecategories`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT für Tabelle `furniturehouse`
--
ALTER TABLE `furniturehouse`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `gangzones`
--
ALTER TABLE `gangzones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT für Tabelle `garbageroutes`
--
ALTER TABLE `garbageroutes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT für Tabelle `groups`
--
ALTER TABLE `groups`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `groupsrangs`
--
ALTER TABLE `groupsrangs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `groups_members`
--
ALTER TABLE `groups_members`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `houseinteriors`
--
ALTER TABLE `houseinteriors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=63;

--
-- AUTO_INCREMENT für Tabelle `houses`
--
ALTER TABLE `houses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=601;

--
-- AUTO_INCREMENT für Tabelle `inactiv`
--
ALTER TABLE `inactiv`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `invoices`
--
ALTER TABLE `invoices`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `itemmodels`
--
ALTER TABLE `itemmodels`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=138;

--
-- AUTO_INCREMENT für Tabelle `lifeinvaderads`
--
ALTER TABLE `lifeinvaderads`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `logs`
--
ALTER TABLE `logs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `namechanges`
--
ALTER TABLE `namechanges`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `outfits`
--
ALTER TABLE `outfits`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `paydays`
--
ALTER TABLE `paydays`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `policefile`
--
ALTER TABLE `policefile`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `screenshots`
--
ALTER TABLE `screenshots`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `services`
--
ALTER TABLE `services`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `settings`
--
ALTER TABLE `settings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `shopitems`
--
ALTER TABLE `shopitems`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=128;

--
-- AUTO_INCREMENT für Tabelle `smartphonecalls`
--
ALTER TABLE `smartphonecalls`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `smartphonemessages`
--
ALTER TABLE `smartphonemessages`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `smartphones`
--
ALTER TABLE `smartphones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `spedvehicles`
--
ALTER TABLE `spedvehicles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT für Tabelle `speedcameras`
--
ALTER TABLE `speedcameras`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `standingorder`
--
ALTER TABLE `standingorder`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `tattoos`
--
ALTER TABLE `tattoos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `taxiroutes`
--
ALTER TABLE `taxiroutes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `tickets`
--
ALTER TABLE `tickets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `ticket_answers`
--
ALTER TABLE `ticket_answers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `ticket_user`
--
ALTER TABLE `ticket_user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `timeline`
--
ALTER TABLE `timeline`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `transfer`
--
ALTER TABLE `transfer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `userfile`
--
ALTER TABLE `userfile`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `userlog`
--
ALTER TABLE `userlog`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `vehicles`
--
ALTER TABLE `vehicles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `vehicleshop`
--
ALTER TABLE `vehicleshop`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=436;

--
-- AUTO_INCREMENT für Tabelle `weapons`
--
ALTER TABLE `weapons`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `whitelist`
--
ALTER TABLE `whitelist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
