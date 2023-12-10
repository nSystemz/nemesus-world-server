//Chat
const chat = mp.browsers.new('package://misc/chat/index.html');
//GUI
var hudWindow = mp.browsers.new('package://web/index.html');
//Imports
const Camera = require('./helper/camera');
//Cam
let hudcam = null;
//Localplayer
let localPlayer = mp.players.local;
//Keys
let pressedF = 0;
let pressedF3 = 0;
let pressedF4 = 0;
let pressedF5 = 0;
let pressedF6 = 0;
let pressedF7 = 0;
let pressedF8 = 0;
let pressedF9 = 0;
let pressedF10 = 0;
let pressedEinf = 0;
let pressedSTRG = 0;
let pressedK = 0;
let pressedX = 0;
let pressedL = 0;
let pressedI = 0;
let pressedP = 0;
let pressedB = 0;
let pressedE = 0;
let pressedH = 0;
let pressedM = 0;
let pressed0 = 0;
let pressed1 = 0;
let pressed2 = 0;
let pressed3 = 0;
let pressed4 = 0;
let pressedTab = 0;
let pressedLeft = 0;
let pressedRight = 0;
let pressedBS = 0;
let pressedSpace = 0;
let pressedAlt = 0;
let pressedShift = 0;
let pressedDown = 0;
let pressedUp = 0;
let pressedG = 0;
let pressedESC = 0;
let pressedNum = 0;
//Crosshair
let crosshair = 17;
//Anticheat
let triggerAntiCheat = false;
let calledAntiCheat = 0;
let antiCheatWait = 0;
let wait = false;
let wait2 = 0;
let oldposition = null;
let vehicleEntrys = 0;
let vehicleTime = 0;
let flyTrys = 0;
let flyTime = 0;
let speedTrys = 0;
let speedTime = 0;
let antiCheatTime = 0;
let godmodeCall = 0;
//Livestream
let livestreamfov_max = 70.0
let livestreamfov_min = 5.0
let livestreamzoomspeed = 10.0
let livestreamspeed_lr = 8.0
let livestreamspeed_ud = 8.0
let livestreamfov = (livestreamfov_max + livestreamfov_min) * 0.5
let livestreamCam = null;
let livestreamPlayer = 0;
let livestream = 0;
let livestreamscaleform = null
let livestreamscaleform2 = null
let new_z = -1;
//Menu
let showMenu = false;
//MDC
let showMdc = false;
//CenterMenu
let showCenterMenu = false;
let centerHeader = "n/A";
//Wheel
let wheelSelected = 0;
let showWheel = false;
//Bank
let showBank = false;
//Perso
let showPerso = false;
//Lics
let showLics = false;
//Animations
let animations = [];
//Tabmenu
let showTab = false;
let tabJson;
//Spectate
let specTarget = null;
let specName = '';
let specWaiting = 0;
//Inventory
let inventory = [];
let showInventory = false;
//Speedo
let showSpeedo = false;
//GOV
let showGov = false;
//Cityhall
let showCityhall = false;
let showCarSetting = false;
//House
let InteriorSwitch = false;
//Furniture
let showFurniture = false;
let editFurniture = false;
let modusFurniture = false;
let modusFurniture2 = false;
let moebelModus = false;
let furniture = null;
let speedFurniture = 1;
//Lockpicking
let startLockpicking = false;
let lastProgress = 'n/A';
//Maskenhändler
let clothMenu3 = false;
//Fraktionskleidung
let clothMenu4 = false;
//Arrested
let arrested = false;
//Juwelier
let clothMenu2 = false;
//Clothmenu
let clothMenu = false;
//Tattoomenu
let tattooShop = false;
//Barbermenu
let barberMenu = false;
//CCTV
let cctvShow = false;
//Spedition
let showSped = false;
//Dealership
let defCamera = null;
let dealerVehicle = null;
let showDealer = false;
let dealerShipBizz = -1;
let oldVehicleShipColor = 0;
//Nametags
let nameTagList = null;
let nametagSystem = 0;
let maxDistance = 25 * 4;
const color = [255, 255, 255, 255];
//Nitro
let activateNitro = 0;
let vehiclesWithNitro = [];
let exhausts = ["exhaust", "exhaust_2", "exhaust_4", "exhaust_5", "exhaust_6", "exhaust_7"];
let nitroCd = 0;
let nitroTimer = null;
//Shops
let showShop = false;
let showShop2 = false;
let showAmmu = false;
let showAmmuPolice = false;
let lastShop = 'n/A';
//Drivingschool
let drivingSchoolHandle = null;
let drivingSchoolHandle2 = null;
let drivingSchoolCount = 0;
let drivingSchoolPositions = [];
let drivingSchoolPositions1 = [
    new mp.Vector3(-735.4921, -1294.3248, 4.379889),
    new mp.Vector3(-714.6124, -1255.5382, 8.597668),
    new mp.Vector3(-658.6529, -1318.0498, 9.942729),
    new mp.Vector3(-666.1681, -1508.7233, 10.945299),
    new mp.Vector3(-731.53253, -1904.8663, 26.54007),
    new mp.Vector3(-790.2929, -2192.7844, 15.83792),
    new mp.Vector3(-925.5045, -2322.6697, 19.509548),
    new mp.Vector3(-1084.4204, -2597.5989, 19.503988),
    new mp.Vector3(-1030.6913, -2730.1328, 19.506716),
    new mp.Vector3(-874.26624, -2657.8228, 18.552904),
    new mp.Vector3(-769.2096, -2469.4219, 13.593178),
    new mp.Vector3(-743.4607, -2365.8545, 14.199974),
    new mp.Vector3(-967.3989, -2142.0144, 8.224508),
    new mp.Vector3(-1054.3973, -1917.6249, 12.417672),
    new mp.Vector3(-846.5301, -1708.8427, 18.317205),
    new mp.Vector3(-730.1941, -1576.924, 13.782566),
    new mp.Vector3(-639.32294, -1387.8281, 9.928852),
    new mp.Vector3(-665.47797, -1245.0192, 9.961682),
    new mp.Vector3(-723.89844, -1255.8777, 7.994992),
    new mp.Vector3(-743.9326, -1310.7203, 4.3785753),
    new mp.Vector3(-729.22394, -1291.5095, 4.3787866)
];
let drivingSchoolPositions2 = [
    new mp.Vector3(-738.66644, -1296.7916, 4.518988),
    new mp.Vector3(-707.00226, -1248.6412, 9.547168),
    new mp.Vector3(-711.19165, -1182.2786, 10.095307),
    new mp.Vector3(-807.7439, -1030.0435, 12.658868),
    new mp.Vector3(-850.9806, -867.291, 17.922604),
    new mp.Vector3(-856.40436, -723.363, 25.269161),
    new mp.Vector3(-943.6793, -448.77615, 37.135155),
    new mp.Vector3(-1075.8932, -424.9722, 36.08337),
    new mp.Vector3(-1234.691, -433.45825, 33.07177),
    new mp.Vector3(-1279.4784, -546.3593, 30.586172),
    new mp.Vector3(-1275.8823, -626.5663, 26.43129),
    new mp.Vector3(-1249.4984, -759.17523, 19.467834),
    new mp.Vector3(-1145.3654, -889.2623, 7.6130667),
    new mp.Vector3(-1050.7305, -1048.5221, 1.6670437),
    new mp.Vector3(-966.1043, -1195.7009, 3.9785683),
    new mp.Vector3(-892.991, -1200.2788, 4.407906),
    new mp.Vector3(-793.57227, -1138.9305, 9.861857),
    new mp.Vector3(-707.392, -1220.021, 10.134834),
    new mp.Vector3(-749.983, -1280.4211, 4.8476453),
    new mp.Vector3(-737.5554, -1303.4386, 4.512655)
];
let drivingSchoolPositions3 = [
    new mp.Vector3(-706.91235, -1248.7957, 10.088962),
    new mp.Vector3(-657.97327, -1417.8785, 10.644517),
    new mp.Vector3(-712.66254, -1528.5583, 13.045022),
    new mp.Vector3(-834.7123, -1689.1531, 18.424568),
    new mp.Vector3(-1068.1359, -1921.8511, 13.106361),
    new mp.Vector3(-945.68146, -2171.869, 8.953328),
    new mp.Vector3(-757.1612, -2359.7788, 14.987264),
    new mp.Vector3(-685.8613, -2355.9243, 13.8876),
    new mp.Vector3(-512.0201, -2156.2737, 8.544771),
    new mp.Vector3(-538.1541, -2091.8389, 8.356854),
    new mp.Vector3(-783.4361, -1972.6978, 8.956832),
    new mp.Vector3(-949.6354, -2138.5652, 9.076312),
    new mp.Vector3(-1035.3778, -2074.459, 13.530155),
    new mp.Vector3(-1043.1089, -1905.9833, 13.275422),
    new mp.Vector3(-864.9945, -1727.5272, 19.07052),
    new mp.Vector3(-704.66974, -1520.007, 12.5752735),
    new mp.Vector3(-645.0358, -1393.4344, 10.690985),
    new mp.Vector3(-664.4717, -1246.7212, 10.604069),
    new mp.Vector3(-793.28125, -1286.1301, 5.0279775)
];
let drivingSchoolPositions4 = [
    new mp.Vector3(-741.36786, -1371.6785, 0.12720284),
    new mp.Vector3(-790.5286, -1430.1981, 0.08934292),
    new mp.Vector3(-852.7024, -1536.2435, 0.32978973),
    new mp.Vector3(-945.7268, -1636.96, 0.1278775),
    new mp.Vector3(-1065.4069, -1759.6003, 0.5077292),
    new mp.Vector3(-1178.6902, -1870.3298, 0.9560079),
    new mp.Vector3(-1301.4478, -1970.5891, 0.83128846),
    new mp.Vector3(-1468.8413, -1921.5795, 1.2421584),
    new mp.Vector3(-1681.6122, -1798.2424, 0.7304739),
    new mp.Vector3(-1893.4557, -1725.6509, 1.0503943),
    new mp.Vector3(-2008.442, -1870.4641, 0.7628157),
    new mp.Vector3(-1961.4049, -2120.1682, -0.053651333),
    new mp.Vector3(-1790.9545, -2329.0176, 1.9127429),
    new mp.Vector3(-1663.1897, -2266.4077, 1.0447954),
    new mp.Vector3(-1513.0526, -2168.8638, 0.45764285),
    new mp.Vector3(-1348.1732, -2041.6376, 0.65081024),
    new mp.Vector3(-1228.7336, -1928.8258, 0.5513771),
    new mp.Vector3(-1092.2294, -1793.3069, 0.8354314),
    new mp.Vector3(-953.38477, -1659.4198, 0.4110764),
    new mp.Vector3(-856.8498, -1564.7324, 0.3000309),
    new mp.Vector3(-817.4618, -1489.2484, 0.29898947),
    new mp.Vector3(-733.9692, -1359.5272, 0.35097012)
];
let drivingSchoolPositions5 = [
    new mp.Vector3(-1176.0404, -1960.9254, 300.0394),
    new mp.Vector3(-672.0059, -1790.9781, 500.1073),
    new mp.Vector3(1095.4496, -1675.1566, 500.1073),
    new mp.Vector3(1710.6465, 324.96594, 500.1073),
    new mp.Vector3(-175.87207, 1960.9841, 500.1073),
    new mp.Vector3(-1430.4324, 439.79715, 500.1073),
    new mp.Vector3(-1515.6934, -1207.9508, 500.1073),
    new mp.Vector3(-1460.199, -2450.4688, 249.26515)
];
//Ammunation
let rangeStatus = 0;
let rangeTime = 0;
let startRange = false;
let rangePoints = 0;
let rangeHandle = null
let getPoint = 0;
let oldTargetPosition = [
    new mp.Vector3(15.3432045, -1079.3007, 28.747013),
    new mp.Vector3(20.942717, -1081.562, 28.747013),
    new mp.Vector3(19.85347, -1090.7338, 28.747013),
    new mp.Vector3(21.230152, -1091.2755, 28.747013),
    new mp.Vector3(12.08647, -1088.0034, 28.747013),
    new mp.Vector3(18.978653, -1080.5413, 28.747013),
    new mp.Vector3(17.944483, -1084.5223, 28.747013),
    new mp.Vector3(13.858859, -1084.0061, 28.747013),
    new mp.Vector3(25.032175, -1079.4131, 28.747013),
    new mp.Vector3(21.410034, -1075.7052, 28.747013),
    new mp.Vector3(16.759706, -1073.4792, 28.747013),
    new mp.Vector3(22.497166, -1071.5581, 28.747013),
    new mp.Vector3(19.202332, -1072.9413, 28.747013)
];
let oldTargetPosition2 = [
    new mp.Vector3(479.24756, -1013.935, 24.734655),
    new mp.Vector3(481.58252, -1012.5743, 24.734655),
    new mp.Vector3(480.34586, -1011.64825, 24.734655),
    new mp.Vector3(479.62195, -1009.60913, 24.734655),
    new mp.Vector3(481.4284, -1008.49786, 24.734655),
    new mp.Vector3(479.32953, -1003.8369, 24.734655),
    new mp.Vector3(480.79913, -1002.57227, 24.734655),
    new mp.Vector3(480.47272, -1014.67163, 24.734655),
    new mp.Vector3(481.5011, -1012.0936, 24.734655)
];
//Busdriver
let busHandle = null;
let showBusPlan = false;
//Garbage
let garbageHandle = null;
let garbageHandle2 = null;
//Prison
let prisonCount = 0;
let prisonCount2 = 0;
let prisonHandle = null;
let prisonTime = 0;
let prisonPosition = [
    new mp.Vector3(-1999.9506, 1123.6338, -27.969992 - 0.5),
    new mp.Vector3(-1945.0227, 1123.5461, -27.972258 - 0.5),
    new mp.Vector3(-1940.0676, 1087.2427, -27.983965 - 0.5),
    new mp.Vector3(-1966.2281, 1058.1459, -27.98502 - 0.5),
    new mp.Vector3(-2016.3542, 1046.4849, -27.986984 - 0.5),
    new mp.Vector3(-2054.4358, 1066.8162, -27.98443 - 0.5),
    new mp.Vector3(-2085.4673, 1120.3956, -27.985823 - 0.5),
    new mp.Vector3(-2113.3284, 1166.7535, -27.985037 - 0.5),
    new mp.Vector3(-2044.4899, 1194.7478, -27.98651 - 0.5),
    new mp.Vector3(-1991.4299, 1217.3386, -27.968805 - 0.5),
    new mp.Vector3(-1927.8436, 1224.2819, -27.972454 - 0.5),
    new mp.Vector3(-1884.7992, 1198.5847, -27.984356 - 0.5),
    new mp.Vector3(-1852.8992, 1147.8524, -27.987005 - 0.5),
    new mp.Vector3(-1872.6754, 1103.3475, -27.985561 - 0.5),
    new mp.Vector3(-1906.1658, 1070.8755, -27.986189 - 0.5),
    new mp.Vector3(-1888.4071, 1035.8058, -27.985888 - 0.5),
    new mp.Vector3(-1984.4401, 999.0564, -27.985739 - 0.5),
    new mp.Vector3(-2027.4943, 1016.44995, -27.970888 - 0.5),
    new mp.Vector3(-2085.144, 1004.7106, -27.985353 - 0.5),
    new mp.Vector3(-2114.6904, 1023.68604, -27.978657 - 0.5),
    new mp.Vector3(-2127.205, 1065.8312, -27.985579 - 0.5)
];
//Beach Garbage
let beachGarbagePosition = [
    new mp.Vector3(-1394.7266, -1432.8102, 4.0241566 - 0.5),
    new mp.Vector3(-1400.4176, -1477.4281, 3.2325783 - 0.5),
    new mp.Vector3(-1446.9103, -1484.2922, 2.2548735 - 0.5),
    new mp.Vector3(-1415.2599, -1537.9025, 2.1480672 - 0.5),
    new mp.Vector3(-1367.3555, -1549.9121, 3.3391027 - 0.5),
    new mp.Vector3(-1341.0334, -1563.1396, 4.4082417 - 0.5),
    new mp.Vector3(-1340.2853, -1541.5903, 4.3332405 - 0.5),
    new mp.Vector3(-1435.4648, -1361.9458, 4.091404 - 0.5),
    new mp.Vector3(-1412.8696, -1330.4465, 3.7204833 - 0.5),
    new mp.Vector3(-1466.475, -1306.2706, 3.7203689 - 0.5),
    new mp.Vector3(-1426.073, -1504.3804, 2.333782 - 0.5),
    new mp.Vector3(-1464.3301, -1478.91, 2.1496906 - 0.5),
    new mp.Vector3(-1458.2062, -1419.3647, 2.5832167 - 0.5),
    new mp.Vector3(-1428.9884, -1397.4807, 4.3341093 - 0.5),
    new mp.Vector3(-1458.0189, -1382.5803, 2.7698476 - 0.5)
];
//Farmer
let cowPosition = [
    new mp.Vector3(2174.1628, 4986.506, 41.291763 - 0.5),
    new mp.Vector3(2163.0586, 4975.764, 41.31196 - 0.5),
    new mp.Vector3(2152.0818, 4965.274, 41.336136 - 0.5),
    new mp.Vector3(2175.6726, 4947.546, 41.311344 - 0.5),
    new mp.Vector3(2190.1565, 4961.912, 41.285393 - 0.5),
    new mp.Vector3(2197.0276, 4968.819, 41.37545 - 0.5)
];
let tomatoPosition = [
    new mp.Vector3(1935.6422, 5007.799, 42.914547 - 0.5),
    new mp.Vector3(1902.8834, 4979.786, 48.690224 - 0.5),
    new mp.Vector3(1893.7389, 4983.3633, 49.867172 - 0.5),
    new mp.Vector3(1932.9733, 5016.8247, 43.512005 - 0.5),
    new mp.Vector3(1887.1461, 4992.7646, 50.563168 - 0.5),
    new mp.Vector3(1879.0542, 4997.032, 51.6406 - 0.5),
    new mp.Vector3(1878.111, 5028.7056, 49.725986 - 0.5),
    new mp.Vector3(1939.3019, 5080.9727, 41.821827 - 0.5),
    new mp.Vector3(1933.6744, 5087.112, 42.12909 - 0.5),
    new mp.Vector3(1860.5497, 5024.8154, 52.733757 - 0.5),
    new mp.Vector3(1850.256, 5025.545, 54.515663 - 0.5),
    new mp.Vector3(1927.8367, 5092.142, 42.50914 - 0.5),
    new mp.Vector3(1927.8367, 5092.142, 42.50914 - 0.5)
];
let wheatPosition = [
    new mp.Vector3(2187.4507, 5153.274, 54.595844 - 0.5),
    new mp.Vector3(2177.985, 5163.0615, 55.25079 - 0.5),
    new mp.Vector3(2167.0256, 5174.879, 56.5556 - 0.5),
    new mp.Vector3(2158.2607, 5183.9526, 57.354652 - 0.5),
    new mp.Vector3(2148.4272, 5194.54, 58.03798 - 0.5),
    new mp.Vector3(2139.2817, 5203.956, 58.066833 - 0.5),
    new mp.Vector3(2130.7498, 5202.1597, 57.495052 - 0.5),
    new mp.Vector3(2141.652, 5189.9585, 57.22196 - 0.5),
    new mp.Vector3(2156.8022, 5173.6387, 55.9726 - 0.5),
    new mp.Vector3(2166.9985, 5162.689, 54.76256 - 0.5),
    new mp.Vector3(2167.688, 5147.518, 52.16612 - 0.5),
    new mp.Vector3(2160.1624, 5155.699, 53.16621 - 0.5),
    new mp.Vector3(2154.0417, 5162.392, 54.034447 - 0.5),
    new mp.Vector3(2144.1826, 5173.021, 55.27473 - 0.5),
    new mp.Vector3(2131.6836, 5185.9673, 56.16777 - 0.5),
    new mp.Vector3(2122.0886, 5196.316, 56.86418 - 0.5),
    new mp.Vector3(2112.2854, 5192.852, 55.932636 - 0.5),
    new mp.Vector3(2122.275, 5181.82, 54.98251 - 0.5),
    new mp.Vector3(2133.4607, 5169.6943, 53.92107 - 0.5),
    new mp.Vector3(2145.4263, 5156.4966, 52.518196 - 0.5),
    new mp.Vector3(2155.0632, 5146.5005, 51.182316 - 0.5),
    new mp.Vector3(2153.664, 5132.8657, 49.04862 - 0.5),
    new mp.Vector3(2147.0369, 5139.832, 49.868618 - 0.5),
    new mp.Vector3(2137.3674, 5150.3936, 51.17532 - 0.5),
    new mp.Vector3(2128.6907, 5160.099, 52.377262 - 0.5),
    new mp.Vector3(2117.327, 5171.9697, 53.521107 - 0.5),
    new mp.Vector3(2104.7783, 5185.014, 54.83008 - 0.5),
    new mp.Vector3(2091.0498, 5186.761, 54.58533 - 0.5),
    new mp.Vector3(2097.526, 5179.6416, 54.079525 - 0.5),
    new mp.Vector3(2106.6206, 5169.7554, 52.99779 - 0.5),
    new mp.Vector3(2117.7734, 5157.621, 51.657093 - 0.5),
    new mp.Vector3(2131.0647, 5143.1714, 49.859734 - 0.5),
    new mp.Vector3(2143.7954, 5129.5757, 48.147446 - 0.5),
    new mp.Vector3(2139.467, 5122.958, 47.09555 - 0.5),
    new mp.Vector3(2127.3518, 5136.079, 48.758526 - 0.5),
    new mp.Vector3(2117.5427, 5146.4907, 49.992348 - 0.5),
    new mp.Vector3(2108.095, 5156.6655, 50.934456 - 0.5),
    new mp.Vector3(2092.1646, 5173.05, 52.483677 - 0.5),
    new mp.Vector3(2092.1646, 5173.05, 52.483677 - 0.5)
];
let farmerStatus = 0;
let farmerCount = 0;
let farmerTime = 0;
let farmerHandle = null;
let farmerHandle2 = null;
let farmerHandles = [];
//Cleaner
let cleanerCount = 0;
let cleaningTask = 0;
let cleanerTime = 0;
let cleanerHandle = null;
let cleanerHandle2 = null;
let cleanerHandle3 = null;
let cleanerHandle4 = null;
let cleanerPositions = [
    new mp.Vector3(20.166084, 31.440306, -146.62814),
    new mp.Vector3(28.396372, 32.797367, -146.62624),
    new mp.Vector3(34.97436, 32.50852, -146.62575),
    new mp.Vector3(38.911903, 38.514614, -146.62268),
    new mp.Vector3(38.817684, 28.036922, -146.62964),
    new mp.Vector3(39.0936, 20.45733, -146.64114),
    new mp.Vector3(38.90094, 11.224712, -146.63708),
    new mp.Vector3(39.114414, 3.0247693, -146.63168),
    new mp.Vector3(34.438347, -0.8269255, -146.6405),
    new mp.Vector3(20.935827, -0.5841898, -146.63869),
    new mp.Vector3(8.480937, -1.0706104, -146.6443),
    new mp.Vector3(45.34298, -0.63429093, -146.6314),
    new mp.Vector3(51.535015, -0.7521671, -146.63792),
    new mp.Vector3(60.40985, -0.92845446, -146.64189),
    new mp.Vector3(64.103745, -0.91144776, -146.63846),
    new mp.Vector3(38.94172, -3.6230047, -146.62973),
    new mp.Vector3(39.192566, -9.5548725, -146.6282),
    new mp.Vector3(36.393314, -12.37644, -146.62439),
    new mp.Vector3(31.75195, -12.063446, -146.62929),
    new mp.Vector3(16.22595, -12.50482, -146.64912),
    new mp.Vector3(39.104027, -22.886467, -146.62672)
];
//Tuning
let showTuning = false;
let showCursorTemp = false;
let vehicleListTuning = [];
//Mechatroniker
let showMecha = false;
//Trash
let listTrash = [];
//Weaponsystem
let listWeaponComponents = [];
//Fuel
let showFuel = false;
let fuelPositions = [
    //Bizz 5
    new mp.Vector3(1175.8339, -322.80164, 69.3508),
    new mp.Vector3(1183.2217, -321.4986, 69.351875),
    new mp.Vector3(1184.7297, -329.1253, 69.31523),
    new mp.Vector3(1177.3427, -330.53165, 69.31664),
    new mp.Vector3(1175.5642, -321.74146, 69.35079),
    new mp.Vector3(1182.9696, -320.4142, 69.349754),
    new mp.Vector3(1186.2598, -337.6653, 69.3517),
    new mp.Vector3(1178.8286, -338.972, 69.35641),
    new mp.Vector3(1179.0657, -340.09277, 69.356476),
    new mp.Vector3(1186.4966, -338.79626, 69.357765),
    new mp.Vector3(1184.9692, -330.18472, 69.3179),
    new mp.Vector3(1177.5522, -331.4926, 69.316574),
    //Bizz 6
    new mp.Vector3(-63.152065, -1768.0918, 29.261726),
    new mp.Vector3(-60.549255, -1760.9589, 29.261694),
    new mp.Vector3(-61.581593, -1760.6464, 29.261728),
    new mp.Vector3(-64.16449, -1767.736, 29.2611),
    new mp.Vector3(-71.56, -1765.2302, 29.53404),
    new mp.Vector3(-68.99002, -1758.1678, 29.533997),
    new mp.Vector3(-70.00452, -1757.8163, 29.53369),
    new mp.Vector3(-72.522446, -1764.9323, 29.533525),
    new mp.Vector3(-79.62542, -1762.3412, 29.794657),
    new mp.Vector3(-77.05797, -1755.2039, 29.800293),
    new mp.Vector3(-78.06932, -1754.9265, 29.800333),
    new mp.Vector3(-80.6763, -1761.9991, 29.800333),
    //Bizz 7
    new mp.Vector3(2573.2622, 364.7006, 108.647804),
    new mp.Vector3(2572.9868, 359.21198, 108.647804),
    new mp.Vector3(2574.033, 359.22083, 108.647804),
    new mp.Vector3(2574.3328, 364.76498, 108.6478),
    new mp.Vector3(2580.5867, 364.37717, 108.88851),
    new mp.Vector3(2580.3574, 358.9027, 108.6478),
    new mp.Vector3(2581.4875, 358.85263, 108.6478),
    new mp.Vector3(2581.7566, 364.34555, 108.62464),
    new mp.Vector3(2588.1624, 364.0315, 108.64774),
    new mp.Vector3(2587.9172, 358.5365, 108.647804),
    new mp.Vector3(2588.99, 358.50677, 108.63903),
    new mp.Vector3(2589.1453, 364.10083, 108.6478),
    //Bizz 12
    new mp.Vector3(1705.5573, 6414.1465, 32.76403),
    new mp.Vector3(1705.9689, 6415.112, 32.764038),
    new mp.Vector3(1701.9115, 6416.966, 32.764015),
    new mp.Vector3(1701.5212, 6415.996, 32.764034),
    new mp.Vector3(1697.6099, 6417.883, 32.764034),
    new mp.Vector3(1697.9768, 6418.794, 32.764034),
    //Bizz 14
    new mp.Vector3(2001.9058, 3771.6328, 32.403934),
    new mp.Vector3(2004.2795, 3772.9077, 32.40394),
    new mp.Vector3(2006.6052, 3774.4265, 32.40394),
    new mp.Vector3(2009.6277, 3776.1829, 32.403934),
    new mp.Vector3(2008.8662, 3777.3423, 32.403934),
    new mp.Vector3(2005.8527, 3775.523, 32.40394),
    new mp.Vector3(2003.552, 3774.0486, 32.403934),
    new mp.Vector3(2001.1718, 3772.7617, 32.403934),
    //Bizz 15
    new mp.Vector3(-714.92346, -932.5354, 19.21382),
    new mp.Vector3(-714.94696, -939.3189, 19.20394),
    new mp.Vector3(-716.02344, -939.34656, 19.20773),
    new mp.Vector3(-715.94995, -932.48334, 19.213755),
    new mp.Vector3(-723.5149, -932.47687, 19.213905),
    new mp.Vector3(-723.4378, -939.3022, 19.20352),
    new mp.Vector3(-724.50696, -939.3707, 19.208748),
    new mp.Vector3(-724.5022, -932.59235, 19.213932),
    new mp.Vector3(-732.05963, -932.5169, 19.21393),
    new mp.Vector3(-732.12146, -939.26245, 19.20386),
    new mp.Vector3(-733.1399, -939.3334, 19.208328),
    new mp.Vector3(-733.175, -932.5181, 19.213482)
];
//Smartphone
let showHandy = false;
//Attachments
let registredAttachments = [];
let entityAttachments = [];
//Radio
let showRadio = false;
//Musicsystem
let showMusic = false;
//LSPD
let visionState = 0;
let cuffed = false;
let showArrest = false;
let showTicket = false;
let pdCam = false;
let pdVehicle = null;
let liveposis = [];
let liveposisold = [];
let livemap = false;
//LSRC
let showRecept = false;
//Crouching
const movementClipSet = "move_ped_crouched";
const strafeClipSet = "move_ped_crouched_strafing";
const clipSetSwitchTime = 0.25;
//Misc
let IdleDate = new Date();
let playerID = 0;
let showDl = false;
let vehicleListDl = [];
let list3D = [];
let marker = null;
let marker2 = null;
let ped = null;
let pedTimer = null;
let timeMarker = 0;
let marker2TO = null;
let lastid = 0;
let nokeys = false;
let showSaltyError = false;
let talkstate = -2;
let handsUp = false;
let showCrosshair = false;
let oldCrosshair = 0;
let prices = [];
let voicerp = 1;
let groupprices = [];
let oldCheck = 0;
let maxWeapons = 0;
let oldTint = 0;
let timerCounter = 0;
let globalBool = false;
let oldHealth = 200;
let oldArmor = 0;
let vehiclePos = null;
let vehicleKilometre = 0;
let setCiga = false;
let setJoint = false;
let jointTimer = null;
let sprintTimer = null;
let ticketCooldown = 0;
let waypointHandle = null;
let animationSet = 0;
let death = false;
let lastDamage = -1;
let damageEffect = false;
let weaponDamage = 0;
let policeBlip = null;
let lastclick = (Date.now() / 1000);
let afk = false;
let ping = false;
let level = 1;
let showgangzone = false;
let showcrafting = false;
let hack = false;
let crystalmeth = false;
let secondTimer = 0;
let showwardrobe = false;
let updateTimeout = null;
let speakTimeout = null;
let setteleport = false;

//Natives
const SET_GAMEPLAY_CAM_FOLLOW_PED_THIS_UPDATE = '0x8BBACBF51DA047A8';

//Sounds
mp.game.audio.startAudioScene("FBI_HEIST_H5_MUTE_AMBIENCE_SCENE");
mp.game.invoke(0xB4F90FAF7670B16F, false);
mp.game.invoke(0x218DD44AAAC964FF, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", true, 0);
mp.game.invoke(0x218DD44AAAC964FF, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", true, 0);
mp.game.invoke(0x218DD44AAAC964FF, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", true, 0);
mp.game.invoke(0xBDA07E5950085E46, 0, false, false);
mp.game.invoke(0x1D6650420CEC9D3B, "AZ_DISTANT_SASQUATCH", 0, 0);
mp.game.audio.setAudioFlag("LoadMPData", true);
mp.game.audio.setAudioFlag("DisableFlightMusic", true);
mp.game.audio.setAudioFlag("PoliceScannerDisabled", true);
mp.game.audio.setStaticEmitterEnabled('LOS_SANTOS_VANILLA_UNICORN_01_STAGE', false);
mp.game.audio.setStaticEmitterEnabled('LOS_SANTOS_VANILLA_UNICORN_02_MAIN_ROOM', false);
mp.game.audio.setStaticEmitterEnabled('LOS_SANTOS_VANILLA_UNICORN_03_BACK_ROOM', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_Tequilala_Exterior_Emitter', false);
mp.game.audio.setStaticEmitterEnabled('SE_bkr_biker_dlc_int_01_BAR', false);
mp.game.audio.setStaticEmitterEnabled('SE_bkr_biker_dlc_int_01_GRG', false);
mp.game.audio.setStaticEmitterEnabled('SE_bkr_biker_dlc_int_01_REC', false);
mp.game.audio.setStaticEmitterEnabled('SE_bkr_biker_dlc_int_02_GRG', false);
mp.game.audio.setStaticEmitterEnabled('SE_bkr_biker_dlc_int_02_REC', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_Cash_Warehouse_Radio', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_Crack_Warehouse_Radio', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_FakeID_Warehouse_Radio', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_Meth_Warehouse_Radio', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_Biker_Weed_Warehouse_Radio', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_GR_MOC_Radio_01', false);
mp.game.audio.setStaticEmitterEnabled('SE_DLC_APT_Yacht_Bar', false);
mp.game.audio.setStaticEmitterEnabled('se_vw_dlc_casino_exterior_terrace_bar', false);
mp.game.audio.setStaticEmitterEnabled('DLC_BTL_Nightclub_Queue_SCL', false);
mp.game.audio.setStaticEmitterEnabled('DLC_BTL_Nightclub_SCL', false);
mp.game.audio.setStaticEmitterEnabled('SE_ba_dlc_club_exterior', false);

//Trailersync
mp.game.vehicle.setExperimentalAttachmentSyncEnabled(true);

//Hud
mp.browsers.new("package://web/index.html")
//Chat
mp.gui.chat.show(false); //Disabled default RageMP Chat
chat.markAsChat();
let checkChat = mp.storage.data.showChat;
if (typeof checkChat == "undefined") {
    mp.storage.data.showChat = 1;
    mp.storage.flush();
    checkChat = 1;
}
//Minimap Zoom
mp.game.ui.setRadarZoom(1100);
//Nametags
mp.nametags.enabled = false;
//Cash
mp.game.ui.displayCash(true);
//Nitro + Watter effect
mp.game.streaming.requestNamedPtfxAsset("core");
//Enginesystem
mp.players.local.setConfigFlag(429, true);
mp.game.vehicle.defaultEngineBehaviour = false;
//Flee
mp.game.player.setAllRandomPedsFlee(true);
//Console clear
mp.console.clear();
mp.console.logInfo('Willkommen auf dem Nemesus World Roleplay Server - https://nemesus-world.de!', true, true);
mp.console.logInfo('Der Gamemode wurde von https://nemesus.de entwickelt!', true, true);
//Vehicle control
const bones = ['door_dside_f', 'door_pside_f', 'door_dside_r', 'door_pside_r', 'bonnet', 'boot'];
const names = ['Türe', 'Türe', 'Türe', 'Türe', 'Motorhaube', 'Kofferraum'];
let target = null;

const loadClipSet = (clipSetName) => {
    mp.game.streaming.requestClipSet(clipSetName);
    while (!mp.game.streaming.hasClipSetLoaded(clipSetName)) mp.game.wait(0);
};

loadClipSet(movementClipSet);
loadClipSet(strafeClipSet);

//Default values
localPlayer.setMoney(0);

//Register attachments
RegisterAttachment("handCiga", "prop_amb_ciggy_01", 57005, new mp.Vector3(0.17, 0.03, -0.01), new mp.Vector3(-21.00, 99.00, 227.00));
RegisterAttachment("vehicleCiga", "prop_amb_ciggy_01", 47419, new mp.Vector3(0.015, -0.009, 0.003), new mp.Vector3(55.0, 0.0, 110.0));
RegisterAttachment("handJoint", "p_amb_joint_01", 57005, new mp.Vector3(0.17, 0.03, -0.01), new mp.Vector3(-21.00, 99.00, 227.00));
RegisterAttachment("vehicleJoint", "p_amb_joint_01", 47419, new mp.Vector3(0.015, -0.009, 0.003), new mp.Vector3(55.0, 0.0, 110.0));
RegisterAttachment("smartphone", "prop_npc_phone_02", 28422, new mp.Vector3(0.0, 0.0, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
RegisterAttachment("fishingRod", "prop_fishing_rod_01", 60309, new mp.Vector3(0.0, 0.0, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
RegisterAttachment("garbageBag", "hei_prop_heist_binbag", 57005, new mp.Vector3(0.12, 0.0, 0.00), new mp.Vector3(25.0, 270.0, 0.0));
RegisterAttachment("moneyBag", "prop_security_case_01", 57005, new mp.Vector3(0.12, 0.0, 0.00), new mp.Vector3(90.0, -90.0, 0.0));
RegisterAttachment("glowStick", "ba_prop_battle_glowstick_01", 28422, new mp.Vector3(0.0700, 0.1400, 0.0), new mp.Vector3(-80.0, 20.0, 0.0));
RegisterAttachment("horseStick", "ba_prop_battle_hobby_horse", 28422, new mp.Vector3(0.0, 0.0, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
RegisterAttachment("runStock", "prop_cs_walking_stick", 57005, new mp.Vector3(0.15, 0.0, -0.00), new mp.Vector3(0.0, 266.0, 0.0));
RegisterAttachment("tablet", "prop_cs_tablet", 60309, new mp.Vector3(0.03, 0.002, -0.0), new mp.Vector3(10.0, 160.0, 0.0));
RegisterAttachment("pickaxe", "prop_tool_pickaxe", 57005, new mp.Vector3(0.09, 0.03, -0.02), new mp.Vector3(-78.0, 13.0, 28.0));
RegisterAttachment("filmcamera", "prop_v_cam_01", 28422, new mp.Vector3(0.0, 0.0, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
RegisterAttachment("microphone", "p_ing_microphonel_01", 60309, new mp.Vector3(0.055, 0.05, 0.0), new mp.Vector3(240.0, 0.0, 0.0));


//Watercheck
const getWaterByRaycast = (range = 5.0) => {
    let water = null;
    let pos = localPlayer.getOffsetFromInWorldCoords(range, range, 0.0);
    if (pos) {
        let getGroundZ = mp.game.gameplay.getGroundZFor3dCoord(pos.x, pos.y, pos.z + 5, parseFloat(0), false);
        water = mp.game.water.getWaterHeight(pos.x, pos.y, getGroundZ, getGroundZ);
    }
    return water;
}

//Render
mp.events.add('render', (nametags) => {
    //HUD
    mp.game.ui.hideHudComponentThisFrame(1);
    mp.game.ui.hideHudComponentThisFrame(2);
    mp.game.ui.hideHudComponentThisFrame(3);
    mp.game.ui.hideHudComponentThisFrame(4);
    mp.game.ui.hideHudComponentThisFrame(5);
    mp.game.ui.hideHudComponentThisFrame(6);
    mp.game.ui.hideHudComponentThisFrame(7);
    mp.game.ui.hideHudComponentThisFrame(9);
    mp.game.ui.hideHudComponentThisFrame(10);
    mp.game.ui.hideHudComponentThisFrame(11);
    mp.game.ui.hideHudComponentThisFrame(12);
    mp.game.ui.hideHudComponentThisFrame(16);
    mp.game.ui.hideHudComponentThisFrame(17);
    mp.game.ui.hideHudComponentThisFrame(18);
    mp.game.ui.hideHudComponentThisFrame(20);
    mp.game.ui.hideHudComponentThisFrame(22);
    mp.game.graphics.pushScaleformMovieFunction(1, 'SETUP_HEALTH_ARMOUR');
    mp.game.graphics.pushScaleformMovieFunctionParameterInt(3);
    mp.game.graphics.popScaleformMovieFunctionVoid();
    mp.game.controls.disableControlAction(2, 37, true);
    mp.game.controls.disableControlAction(2, 14, true);
    mp.game.controls.disableControlAction(2, 15, true);
    mp.game.controls.disableControlAction(2, 16, true);
    mp.game.controls.disableControlAction(2, 17, true);
    mp.game.controls.disableControlAction(2, 99, true);
    mp.game.controls.disableControlAction(2, 100, true);

    mp.game.gameplay.setFadeOutAfterDeath(false);

    //Health regeneration
    mp.game.player.setHealthRechargeMultiplier(0.0);

    //Damage
    mp.players.local.setSuffersCriticalHits(false); //Headshots

    //Nametags
    if (nametagSystem == 0) {
        UpdateNameTags1(nametags);
    }
    else {
        UpdateNameTags2(nametags);
    }

    //Idle Cam
    const dif = new Date().getTime() - IdleDate.getTime();
    const seconds = dif / 1000;

    if (Math.abs(seconds) > 29.5) {
        mp.game.invoke(`0xF4F2C0D4EE209E20`);
        IdleDate = new Date();
    }

    //Saltychat
    if (showSaltyError == true || triggerAntiCheat == true) {
        for (let i = 0; i < 33; i++) {
            if (i == 24) continue;
            mp.game.controls.disableAllControlActions(i);
        }
    }

    //Disabled controls
    disabledControlPressed();

    //Death controls
    if (death == true) {
        mp.game.controls.disableControlAction(32, 1, true);
        mp.game.controls.disableControlAction(32, 2, true);
        mp.game.controls.disableControlAction(32, 4, true);
        mp.game.controls.disableControlAction(32, 6, true);
    }

    //Menüs (ESC to abort)
    if (showBank == true || showCenterMenu == true || showCarSetting == true || showCityhall == true || clothMenu == true || clothMenu2 == true || clothMenu3 == true || clothMenu4 == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || nokeys == true || showHandy == true || showTuning == true || showDealer == true || showTab == true || startRange == true || editFurniture == true || showFurniture == true ||
        modusFurniture == true || modusFurniture2 == true || InteriorSwitch == true || startLockpicking == true || barberMenu == true || tattooShop == true || showSped == true || showPerso == true || showLics == true || showMdc == true || showArrest == true || showRadio == true || showMusic == true || showTicket == true || showRecept == true || cctvShow == true || afk == true || ping == true || showgangzone == true || showcrafting == true || hack == true) {
        mp.game.controls.disableControlAction(32, 200, true);
    }

    //Livestream
    if (livestream == 1 && !localPlayer.vehicle) {
        mp.game.controls.disableControlAction(0, 25, true);
        mp.game.controls.disableControlAction(0, 44, true);
        mp.game.controls.disableControlAction(0, 37, true);
        mp.game.graphics.drawScaleformMovieFullscreen(livestreamscaleform, 255, 255, 255, 255, false);
        zoomvalue = (1.0 / (livestreamfov_max - livestreamfov_min)) * (livestreamfov - livestreamfov_min);
        if (new_z != -1) {
            localPlayer.setRotation(0, 0, new_z, 2, true);
        }
        CheckInputRotation(livestreamCam, zoomvalue);

        camPitch = livestreamCam.getRot(2);

        if (camPitch.x < -70.0)
            camPitch.x = -70.0
        else if (camPitch.x > 42.0)
            camPitch.x = 42.0

        camPitch.x = (camPitch.x + 70.0) / 112.0

        if (camPitch.y < -180.0)
            camPitch.y = -180.0
        else if (camPitch.y > 180.0)
            camPitch.y = 180.0

        camPitch.y = (camPitch.y + 180.0) / 360.0

        mp.game.invoke(0xD5BB4025AE449A4E, localPlayer, "Pitch", camPitch.x);
        mp.game.invoke(0xD5BB4025AE449A4E, localPlayer, "Heading", camPitch.y * -1.0 + 1.0);

        mp.game.player.disableFiring(true);
    }

    //3D Text
    if (showDl == false) {
        list3D.forEach(function (vehicle) {
            if (vehicle != null) {
                if (vehicle.hasVariable("Vehicle:Text3D")) {
                    distance = distanceVector(localPlayer.position, vehicle.position);
                    if (distance <= 22.5) {
                        mp.game.graphics.drawText(`${vehicle.getVariable('Vehicle:Text3D')}`, [vehicle.position.x, vehicle.position.y, vehicle.position.z + 0.325], {
                            font: 0,
                            color: [255, 255, 255, 185],
                            scale: [0.325, 0.325],
                            outline: true,
                            centre: true
                        });

                    }
                }
            }
        });
    }

    if (vehiclesWithNitro.length > 0) {
        vehiclesWithNitro.forEach((v) => {
            try {
                if (mp.game.streaming.hasNamedPtfxAssetLoaded("core")) {
                    let heading = v.getHeading();
                    let pitch = v.getPitch();
                    exhausts.forEach((element) => {
                        let boneIndex = mp.game.invoke('0xFB71170B7E76ACBA', v.handle, element);
                        if (boneIndex >= 0) {
                            let boneCoords = v.getWorldPositionOfBone(boneIndex);
                            mp.game.graphics.setPtfxAssetNextCall("core");
                            if (mp.game.controls.isControlPressed(0, 71)) {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                    0.0, pitch, heading - 88, 1, false, false, false);
                            } else {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                    0.0, pitch, heading - 88, 0.4, false, false, false);
                            }

                        }
                    });
                } else {
                    mp.game.streaming.requestNamedPtfxAsset("core");
                }
            } catch { }
        });
    }

    //DL
    if (showDl == true) {
        vehicleListDl.forEach(function (vehicle) {
            if (vehicle != null) {
                distance = distanceVector(localPlayer.position, vehicle.position);
                if (distance <= 22.5) {
                    vehiclename = vehicle.getVariable('Vehicle:Name');
                    vehiclename = vehiclename.charAt(0).toUpperCase() + vehiclename.slice(1).toLowerCase();
                    mp.game.graphics.drawText(`~b~${vehiclename} [${vehicle.remoteId}]\n${vehicle.position.x.toFixed(2)}, ${vehicle.position.y.toFixed(2)}, ${vehicle.position.z.toFixed(2)}\n${vehicle.getRotation(5).x.toFixed(2)}, ${vehicle.getRotation(5).y.toFixed(2)}, ${vehicle.getRotation(5).z.toFixed(2)}\n${parseInt((100 / 1000) * vehicle.getHealth())}%
                `, [vehicle.position.x, vehicle.position.y, vehicle.position.z + 0.25], {
                        font: 0,
                        color: [255, 255, 255, 185],
                        scale: [0.25, 0.25],
                        outline: true,
                        centre: true
                    });
                }
            }
        });
    }

    //Deathsystem
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!death && spawned && (getPlayerHealth(localPlayer)-100) < 10) {
        hideMenus();
        mp.events.callRemote('Server:SetDeath', null, 7);
    }

    //Möbelmodus
    if (furniture && moebelModus == true) {
        furniture.forEach(function (element) {
            var tempposi = element.position.split(',').join('.');
            var posi = tempposi.split('|');
            distance2 = distanceVector(localPlayer.position, new mp.Vector3(parseFloat(posi[0]), parseFloat(posi[1]), parseFloat(posi[2])));
            if (distance2 <= 25.5 || localPlayer.dimension > 0) {
                if (parseInt(posi[6]) == localPlayer.dimension) {
                    mp.game.graphics.drawText("" + element.name + " [ID: " + element.id + "]\n" + parseFloat(posi[0]).toFixed(2) + ", " + parseFloat(posi[1]).toFixed(2) + ", " + parseFloat(posi[2]).toFixed(2) + "\n" + parseFloat(posi[3]).toFixed(2) + ", " + parseFloat(posi[4]).toFixed(2) + ", " + parseFloat(posi[5]).toFixed(2) + "\n~b~[E] = Bearbeitungsmodus", [parseFloat(posi[0]), parseFloat(posi[1]), parseFloat(posi[2]) + 0.85], {
                        font: 0,
                        color: [255, 255, 255, 185],
                        scale: [0.25, 0.25],
                        outline: true,
                        centre: false
                    });
                }
            }
        })
    }

    if (localPlayer.vehicle) {
        let vehiclename = localPlayer.vehicle.getVariable('Vehicle:Name');
        if (vehiclename && vehiclename.toLowerCase() == "iak_wheelchair") {
            mp.players.local.setHelmet(false);
        }
    }

    if (!localPlayer.vehicle && localPlayer.dimension == 0) {
        //Getränkeautomat
        if (CheckForSoda() > 0) {
            mp.game.graphics.drawText('~b~Getränkeautomat: ~w~Benutze Taste ~b~[F] ~w~um dir ein Getränk für ' + prices[0] + '$ zu ziehen!', [0.65, 0.9], {
                font: 4,
                color: [255, 255, 255, 215],
                scale: [0.45, 0.45],
                outline: true
            });
            //Pfandflaschen suchen
        } else if (CheckForTrash() > -1) {
            mp.game.graphics.drawText('~b~Mülltonne: ~w~Benutze Taste ~b~[F] ~w~um nach Pfandpflaschen zu suchen!', [0.65, 0.9], {
                font: 4,
                color: [255, 255, 255, 215],
                scale: [0.45, 0.45],
                outline: true
            });
            //ATM
        } else if (CheckForATM() > -1 && !showBank && (!localPlayer.hasVariable("Player:AnimData") || localPlayer.getVariable("Player:AnimData") == 0)) {
            mp.game.graphics.drawText('~b~Bankautomat: ~w~Benutze Taste ~b~[F] ~w~um auf dein Konto zuzugreifen!', [0.65, 0.9], {
                font: 4,
                color: [255, 255, 255, 215],
                scale: [0.45, 0.45],
                outline: true
            });
        }
    }

    if (!mp.players.local.vehicle && !mp.gui.cursor.visible) {
        const raycast = getLocalTargetVehicle();

        if (raycast && raycast.entity.getDoorLockStatus() == 1 && raycast.entity.doors && raycast.entity.getClass() !== 13 && raycast.entity.getClass() !== 8 && !mp.game.player.isFreeAiming() && mp.game.gameplay.getDistanceBetweenCoords(raycast.entity.position.x, raycast.entity.position.y, raycast.entity.position.z, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, false) < 3.5) {

            target = getClosestBone(raycast);
            if (!target) return;

            if (mp.game.gameplay.getDistanceBetweenCoords(target.raycast.position.x, target.raycast.position.y, target.raycast.position.z, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, false) <= 1.55) {
                drawTarget3d(target.raycast.position);
                mp.game.graphics.drawText(`${names[target.id]} ${target.locked ? 'abschliessen' : 'öffnen'}`, [target.raycast.position.x, target.raycast.position.y, target.raycast.position.z - 0.125], {
                    font: 0,
                    color: [255, 255, 255, 255],
                    scale: [0.2, 0.2],
                    centre: true
                });
            }
        }
    }

    //Cleaning
    if (cleaningTask == true) {
        mp.game.graphics.setPtfxAssetNextCall("core");
        mp.game.graphics.startParticleFxNonLoopedAtCoord("water_splash_obj_in", localPlayer.vehicle.position.x, localPlayer.vehicle.position.y, localPlayer.vehicle.position.z, 0, 0, 0, 1, false, false, false);
    }

    //Spectatecam
    if (specTarget && specWaiting == 0) {
        if (mp.players.exists(specTarget) && specTarget.dimension == localPlayer.dimension && distanceVector(localPlayer.position, specTarget.position) <= 515.5) {
            mp.game.invoke(SET_GAMEPLAY_CAM_FOLLOW_PED_THIS_UPDATE, specTarget.handle);
        } else {
            specWaiting = 1;
            setTimeout(() => {
                mp.events.callRemote('Server:StartSpec', specName, 2);
                specWaiting = 0;
            }, 415);
        }
    }

    //Cuffed
    if (cuffed == true) {
        mp.game.controls.disableControlAction(2, 24, true)
        mp.game.controls.disableControlAction(2, 25, true)
        mp.game.controls.disableControlAction(2, 71, true)
        mp.game.controls.disableControlAction(2, 72, true)
    }

    //PDCam
    if (pdCam == true && localPlayer.vehicle != null) {
        pdPointing = pointingAt(1000, true);
        if (pdPointing && pdPointing.entity.type == 'vehicle' && pdPointing.entity != localPlayer.vehicle && pdPointing.entity != pdVehicle) {
            pdVehicle = pdPointing.entity;
        } else {
            if (pdVehicle != null) {
                pdVehicle = null;
            }
        }
        if (pdVehicle != null) {
            let speed = pdVehicle.getSpeed() * 3.6;
            dist = distanceVector(localPlayer.vehicle.position, pdVehicle.position);
            if (speed >= 0) {
                if (dist <= 42) {
                    pdVehiclePos = mp.game.graphics.world3dToScreen2d(pdVehicle.position);
                    mp.game.graphics.drawRect(pdVehiclePos.x, pdVehiclePos.y, 0.015, 0.015, 255, 0, 0, 125);
                    mp.game.graphics.drawText(`Geschwindigkeit: ${parseInt(speed)} KM/H\nNummernschild: ${pdVehicle.getNumberPlateText()}`, [pdVehicle.position.x, pdVehicle.position.y, pdVehicle.position.z + 2.45], {
                        font: 4,
                        color: [255, 0, 0, 255],
                        scale: [0.465, 0.465],
                        outline: true,
                        centre: true
                    });
                } else {
                    pdVehicle = null;
                }
            }
        }
    }

    //Marker
    if (timeMarker && timeMarker > 0) {
        if (marker2 != null) {
            marker2.destroy();
            marker2 = null;
        }
        if (!localPlayer.vehicle) {
            marker2 = mp.markers.new(25, new mp.Vector3(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z - 0.75), timeMarker, {
                color: [63, 103, 145, 185],
                visible: true,
                dimension: 0
            });
        } else {
            marker2 = mp.markers.new(25, new mp.Vector3(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z - 0.55), timeMarker, {
                color: [63, 103, 145, 185],
                visible: true,
                dimension: 0
            });
        }
    }
});

//Prices
mp.events.add("Client:SyncThings", (pricesCsv, animationhotkeys, chair, gprices, level, name = 'n/A', voicerp, nametag) => {
    prices = pricesCsv.split(',');
    crosshair = chair;
    groupprices = gprices;
    if (animationhotkeys && animationhotkeys.length > 3) {
        try {
            animations = JSON.parse(animationhotkeys);
        } catch {
            animations = animationhotkeys;
        }
    }
    level = level;
    voicerp = voicerp;
    nametagSystem = nametag;
    hudWindow.execute(`gui.menu.setvoicerp('${voicerp}');`);
    hudWindow.execute(`gui.hud.setvoicerp('${voicerp}');`);
    hudWindow.execute(`gui.speedometer.setvoicerp('${voicerp}');`);
    mp.discord.update('Nemesus-World.de (Nemesus.de)', 'Spielt als ' + name);
})

//Livestream
mp.events.add("Client:ToggleFilmCamera", (player, onoff) => {
    mp.game.audio.playSoundFrontend(-1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);

    mp.events.call('Client:HideHud');

    livestream = onoff;
    nokeys = livestream == 1 ? true : false;
    livestreamPlayer = onoff == 1 ? player : -1;

    if (onoff == 1) {
        livestreamCam = mp.cameras.new('livestreamCam', new mp.Vector3(localPlayer.position), new mp.Vector3(0, 0, 0), livestreamfov);
        livestreamCam.attachTo(localPlayer.handle, 0.0, 0.0, 1.0, true);
        livestreamCam.setRot(2.0, 1.0, localPlayer.getHeading(), 2);
        livestreamCam.setFov(livestreamfov);
        livestreamCam.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 0, true, false);
        mp.game.graphics.setTimecycleModifier("default");
        mp.game.graphics.setTimecycleModifierStrength(0.3);
        mp.game.cam.setCamEffect(1);
        livestreamscaleform = mp.game.graphics.requestScaleformMovie('security_camera');

        while (!mp.game.graphics.hasScaleformMovieLoaded(livestreamscaleform)) mp.game.wait(0);

        mp.game.graphics.callScaleformMovieMethod(livestreamscaleform, "SET_CAM_LOGO");
        mp.game.graphics.popScaleformMovieFunctionVoid();

        mp.game.ui.displayHud(nokeys);

        if (nokeys == true) {
            enableDisableRadar(false);
        } else {
            enableDisableRadar(true);
        }
    }
})

//Nametags
mp.events.add("Client:UpdateFriends", (names) => {
    nameTagList = names;
})

//Speedometer
mp.events.add("Client:ShowSpeedometer", () => {
    if (hudWindow != null) {
        var vehiclename = 'n/A';
        showSpeedo = !showSpeedo;
        if (localPlayer.vehicle) {
            vehiclename = localPlayer.vehicle.getVariable('Vehicle:Name');
            if (vehiclename.toLowerCase() == "iak_wheelchair") {
                return;
            }
        }
        let maxspeed = 999;
        if (localPlayer.vehicle) {
            maxspeed = localPlayer.vehicle.getVariable('Vehicle:MaxSpeed');
            if (maxspeed == 1) {
                maxspeed = mp.game.vehicle.getVehicleModelMaxSpeed(localPlayer.vehicle.model) * 3.6;
            }
        }
        hudWindow.execute(`gui.speedometer.showSpeedometer('${maxspeed + getSpeedBonus(localPlayer.vehicle)}');`)
    }
})

//Spectate
mp.events.add('Client:StartSpectate', (targetId, targetName) => {
    maxDistance = 315;

    localPlayer.freezePosition(true);

    var interval = setInterval(() => {
        mp.players.forEach(p => {
            if (p.remoteId === targetId) {
                specTarget = p;
                specName = targetName;
                specWaiting = 0;
            }
        });
        if (specTarget && interval) {
            clearInterval(interval);
        }
    }, 15);

    setTimeout(() => {
        if (interval) clearInterval(interval);
    }, 4721);
})

mp.events.add('Client:StopSpectate', (modus) => {
    if (specTarget != null) {

        specTarget = null;
        specName = '';
        specWaiting = 0;

        if (modus == 1) {
            localPlayer.freezePosition(false);
            mp.events.callRemote('Server:StopSpectate');
        }

        if (modus == 2) {
            localPlayer.freezePosition(false);
        }

        maxDistance = 25 * 4;
    }
});

//Doorhash
mp.events.add("Client:GetDoorHash", () => {
    objPointing = pointingAt(1000, true);
    modelHash = 'Keinen Hash gefunden!';
    if (objPointing) {
        modelHash = mp.game.invoke('0x9F47B058362C84B5', objPointing);
        if (modelHash) {
            hudWindow.execute(`gui.inventory.copyToClipboard('${modelHash}');`)
            hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Hash gefunden (${modelHash}) und in die Zwischenablage kopiert!','success','top-left',5150);`)
            return;
        }
    }
    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Keinen Hash gefunden!','error','top-left',2150);`)
});

//DrugEvent
mp.events.add("Client:DrugEvent", () => {
    if (jointTimer != null) {
        clearTimeout(jointTimer);
        jointTimer = null;
    }
    mp.game.graphics.setTimecycleModifier("spectator6");
    mp.game.graphics.setTimecycleModifierStrength(0.2);
    mp.game.cam.setCamEffect(1);
    mp.game.graphics.startScreenEffect("ChopVision", 10000001, true);
    mp.game.cam.setGameplayCamShakeAmplitude(2.75);
    jointTimer = setTimeout(() => {
        jointTimer = null;
        mp.game.cam.setCamEffect(0);
        mp.game.graphics.stopScreenEffect("ChopVision");
        mp.game.invoke("0x0F07E7745A236711");
        mp.game.invoke("0x31B73D1EA9F01DA2");
        mp.game.cam.setGameplayCamShakeAmplitude(0.0);
    }, 65000);
})

mp.events.add("Client:CrystalMeth", () => {
    if (sprintTimer != null) {
        clearTimeout(sprintTimer);
        sprintTimer = null;
    }
    crystalmeth = true;
    sprinttimer = setTimeout(() => {
        crystalmeth = false;
    }, 300000);
})

mp.events.add("Client:SprintMultiplier", (multiplier) => {
    if (sprintTimer != null) {
        clearTimeout(sprintTimer);
        sprintTimer = null;
    }
    mp.game.player.setRunSprintMultiplierFor(multiplier);
    mp.game.player.setSwimMultiplierFor(multiplier);
    mp.game.player.restoreStamina(100);
    sprintTimer = setTimeout(() => {
        sprintTimer = null;
        mp.game.player.setRunSprintMultiplierFor(1.0);
        mp.game.player.setSwimMultiplierFor(1.0);
        mp.game.player.restoreStamina(100);
    }, 300000);
})

mp.events.add("Client:Sonic", (multiplier) => {
    mp.game.player.setRunSprintMultiplierFor(multiplier);
    mp.game.player.setSwimMultiplierFor(multiplier);
    mp.game.player.restoreStamina(100);
})

//Notifications
mp.events.add("Client:StopAllNotifications", () => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.stopAllNotifications();`)
    }
})

mp.events.add("Client:SendNotificationWithoutButton", (title, status, position, timer) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || afk == true || ping == true) return;
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('${title}','${status}','${position}','${timer}');`)
    }
})

mp.events.add("Client:SendNotificationWithoutButton2", (title, status, position, timer) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || afk == true || ping == true) return;
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton2('${title}','${status}','${position}','${timer}');`)
    }
})

mp.events.add("Client:sendNotificationWithTimer", (title, text, timer) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || afk == true || ping == true) return;
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.sendNotificationWithTimer('${title}','${text}','${timer}');`)
    }
})

//Waiting
mp.events.add("Client:Waiting", (set) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.loading.showLoading2();`)
    }
    if (set == 1) {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        triggerAntiCheat = true;
    } else {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        triggerAntiCheat = false;
    }
})

//Lockpicking
mp.events.add("Client:StartLockpicking", (time, action, text) => {
    if (hudWindow != null) {
        lastProgress = action;
        let closestVeh = getClosestVehicle(localPlayer.position);
        if (action == 'vehicle' || action == 'mecha1' || action == 'mecha2') {
            if (closestVeh && closestVeh.distance < 3.55) {
                vDoor = closestVeh.vehicle.getWorldPositionOfBone(closestVeh.vehicle.getBoneIndexByName("seat_dside_f"));
                if (vDoor) {
                    dist = distanceVector(localPlayer.position, vDoor);
                } else {
                    dist = distanceVector(localPlayer.position, closestVeh);
                }
                if (dist <= 1.45) {
                    startLockpicking = true;
                }
            }
        } else if (action == 'mecha7') {
            if (closestVeh && closestVeh.distance < 6.55) {
                bonnet = closestVeh.vehicle.getWorldPositionOfBone(closestVeh.vehicle.getBoneIndexByName("bonnet"));
                dist = distanceVector(localPlayer.position, bonnet);
                if (dist <= 2.15) {
                    startLockpicking = true;
                }
            }
        } else if (action == 'fishing') {
            waterCheck = getWaterByRaycast(5.0);
            if (waterCheck) {
                startLockpicking = true;
            }
        } else {
            startLockpicking = true;
        }
        if (startLockpicking == true) {
            if (action == 'mecha1') {
                closestVeh.vehicle.freezePosition(true);
                if (closestVeh && closestVeh.distance < 6.55) {
                    mp.events.callRemote('Server:VehicleJack');
                    var countInterval = 0;
                    var intervalMecha = setInterval(() => {
                        countInterval = countInterval + 1;
                        if (countInterval >= 15) {
                            clearInterval(intervalMecha);
                            intervalMecha = null;
                            return;
                        }
                        closestVeh.vehicle.setCoordsNoOffset(closestVeh.vehicle.position.x, closestVeh.vehicle.position.y, closestVeh.vehicle.position.z + 0.035, true, true, true);
                    }, 1009);
                }
            } else if (action == 'mecha2') {
                mp.events.callRemote('Server:VehicleJack');
                closestVeh.vehicle.freezePosition(true);
                if (closestVeh && closestVeh.distance < 6.55) {
                    var countInterval = 0;
                    var intervalMecha = setInterval(() => {
                        countInterval = countInterval + 1;
                        if (countInterval >= 11) {
                            clearInterval(intervalMecha);
                            intervalMecha = null;
                            return;
                        }
                        closestVeh.vehicle.setCoordsNoOffset(closestVeh.vehicle.position.x, closestVeh.vehicle.position.y, closestVeh.vehicle.position.z - 0.035, true, true, true);
                    }, 1013);
                }
            }
        } else {
            hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du kannst hier nichts machen!','error','top-left',2500);`)
            return;
        }
    }
    startLockpicking = true;
    localPlayer.freezePosition(true);
    mp.events.callRemote('Server:StartLockpicking', action);
    nokeys = true;
    hudWindow.execute(`gui.progressbar.startProgressbar('${time}','${action}','${text}');`)
})

mp.events.add("Client:FinishProgress", (action) => {
    if (hudWindow != null) {
        startLockpicking = false;
        localPlayer.freezePosition(false);
        if (action != 'cleaning' && action != 'milking' && action != 'tomato') {
            mp.events.callRemote('Server:FinishProgress', action);
        } else {
            if (action == 'cleaning') {
                mp.events.call('Client:ShowSpeedometer');
                mp.events.call('Client:PlaySoundPeep2');
                localPlayer.vehicle.freezePosition(false);
                cleaningTask = false;

                if (cleanerHandle != null) {
                    cleanerHandle.destroy();
                    cleanerHandle = null;
                }
                if (cleanerHandle2 != null) {
                    cleanerHandle2.destroy();
                    cleanerHandle2 = null;
                }
                if (cleanerHandle3 != null) {
                    cleanerHandle3.destroy();
                    cleanerHandle3 = null;
                }
                if (cleanerHandle4 != null) {
                    cleanerHandle4.destroy();
                    cleanerHandle4 = null;
                }
                cleanerCount++;
                if (cleanerCount < 10) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Reinigungspunkt gesäubert!','success','top-left',2150);`)

                    let randValue = getRandomInt(cleanerPositions.length);

                    cleanerHandle = createObject("prop_rub_litter_03c", new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z - 0.22), new mp.Vector3(0.0, 0.0, 0.0), localPlayer.dimension);
                    cleanerHandle2 = mp.checkpoints.new(49, new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z - 0.5), 1.15, {
                        color: [255, 0, 0, 255],
                        visible: true,
                        dimension: localPlayer.dimension
                    });
                    cleanerHandle3 = mp.blips.new(1, new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z), {
                        name: 'Reinigungspunkt',
                        color: 75,
                        shortRange: false,
                        dimension: localPlayer.dimension
                    });
                } else {
                    if (Date.now() / 1000 < cleanerTime && cleanerCount > 5) {
                        mp.events.callRemote('Server:CleanerFinish', 0);
                        setTimeout(function () {
                            callAntiCheat("Teleport to Checkpoint Cheat", "Kanalreiniger", false);
                        }, 1250);
                        return;
                    } else {
                        mp.events.callRemote('Server:CleanerFinish', 1);
                    }
                }
            } else {
                if (action == 'tomato') {
                    mp.events.callRemote('Server:StopAnimation');
                    if (farmerCount < tomatoPosition.length - 1) {
                        if (farmerHandle != null) {
                            farmerHandle.destroy();
                            farmerHandle = null;
                        }
                        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Tomatenstrauch geerntet!','success','top-left',1750);`)
                        mp.game.ui.setNewWaypoint(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y);
                        farmerHandle2 = mp.markers.new(1, new mp.Vector3(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y, tomatoPosition[farmerCount].z - 0.8), 1.25, {
                            color: [255, 0, 0, 255],
                            visible: true,
                            dimension: 0
                        });
                        farmerHandle = mp.colshapes.newSphere(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y, tomatoPosition[farmerCount].z - 0.5, 1.15);
                    } else {
                        if (Date.now() / 1000 < farmerTime && farmerCount > 3) {
                            mp.events.callRemote('Server:CleanerFinish', 0);
                            setTimeout(function () {
                                callAntiCheat("Teleport to Checkpoint Cheat", "Landwirt", false);
                            }, 1250);
                            return;
                        } else {
                            mp.events.callRemote('Server:FarmerFinish', 3);
                        }
                    }
                } else {
                    mp.events.callRemote('Server:StopAnimation');

                    if (farmerHandle != null) {
                        farmerHandle.destroy();
                        farmerHandle = null;
                    }
                    if (farmerHandle2 != null) {
                        farmerHandle2.destroy();
                        farmerHandle2 = null;
                    }

                    farmerCount++;
                    if (farmerCount >= 6) {
                        if (Date.now() / 1000 < farmerTime && farmerCount > 3) {
                            mp.events.callRemote('Server:CleanerFinish', 0);
                            setTimeout(function () {
                                callAntiCheat("Teleport to Checkpoint Cheat", "Landwirt", false);
                            }, 1250);
                            return;
                        } else {
                            mp.events.callRemote('Server:FarmerFinish', 1);
                        }
                    } else {
                        let randValue = getRandomInt(3);
                        if (randValue <= 1) {
                            mp.events.call("Client:PlaySound", "kuh.wav", 0);
                        }
                        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Kuh erfolgreich gemolken!','success','top-left',2150);`)
                        farmerHandle = mp.blips.new(1, new mp.Vector3(cowPosition[farmerCount].x, cowPosition[farmerCount].y, cowPosition[farmerCount].z), {
                            name: 'Kuh',
                            color: 75,
                            shortRange: false,
                            dimension: 0
                        });
                        farmerHandle2 = mp.markers.new(1, new mp.Vector3(cowPosition[farmerCount].x, cowPosition[farmerCount].y, cowPosition[farmerCount].z - 0.5), 0.5, {
                            color: [255, 0, 0, 255],
                            visible: true,
                            dimension: 0
                        });
                    }
                }
            }
        }
        hudWindow.execute(`gui.progressbar.stopProgressbar('');`)
        nokeys = false;
    }
})

//Play sounds
mp.events.add("Client:PlaySound", (name, lower) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.playSound('${name}','${lower}');`)
    }
})

mp.events.add("Client:StopSound", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.stopSound();`)
    }
})

//PrepareCharacterLoad
mp.events.add("Client:PrepareCharacterLoad", () => {
    mp.events.call('Client:PlaySoundSuccessNormal');
    mp.events.call('Client:PlayerFreeze', false);
    mp.events.call('Client:Waiting', 1);
    mp.events.call('Client:SetCameraBehindPlayer', false);
    mp.events.call('Client:ShowHud', localPlayer.remoteId);
})

//Other sounds
mp.events.add("Client:PlaySoundSuccess", () => {
    mp.game.audio.playSoundFrontend(-1, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", true);
})

mp.events.add("Client:PlaySoundSuccessExtra", () => {
    mp.game.audio.playSoundFrontend(-1, "EVENT_START_TEXT", "GTAO_FM_Events_Soundset", true);
})

mp.events.add("Client:PlaySoundSuccessNormal", () => {
    mp.game.audio.playSoundFrontend(-1, "CHECKPOINT_NORMAL", "HUD_MINI_GAME_SOUNDSET", true);
})

mp.events.add("Client:PlaySoundPeep", () => {
    mp.game.audio.playSoundFrontend(-1, "ATM_WINDOW", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
})

mp.events.add("Client:PlaySoundPeep2", () => {
    mp.game.audio.playSoundFrontend(-1, "BOSS_MESSAGE_ORANGE", "GTAO_BOSS_GOONS_FM_SOUNDSET", true);
})

//Inputs
mp.events.add("Client:CallInput", (title, text, type, placeholder, maxlength, flag) => {
    if (hudWindow != null) {
        nokeys = true;
        if (flag.toLowerCase() != 'newpin') {
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        }
        hudWindow.execute(`gui.hud.showInputMenu('${title}','${text}','${type}','${placeholder}','${maxlength}','${flag}');`)
    }
})

mp.events.add("Client:OnInput", (input, flag, confirmed) => {
    if (hudWindow != null) {
        setTimeout(function () {
            nokeys = false;
        }, 150);
        if (flag.toLowerCase() != 'newpin') {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
        mp.events.callRemote('Server:OnInput', input, flag, confirmed);
    }
})

mp.events.add("Client:CallInput2", (title, text, flag, button1, button2) => {
    if (hudWindow != null) {
        nokeys = true;
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        hudWindow.execute(`gui.hud.showInputMenu2('${title}','${text}','${flag}','${button1}','${button2}');`)
    }
})

mp.events.add("Client:OnInput2", (input, flag) => {
    if (hudWindow != null) {
        setTimeout(function () {
            nokeys = false;
        }, 150);
        if (flag.toLowerCase() != 'buyvehicle' && flag.toLowerCase() != 'buyvehicle2') {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
        mp.events.callRemote('Server:OnInput2', input, flag);
    }
})

//InHouse
mp.events.addDataHandler("Player:InHouse", (entity, value) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    if (value > -1) {
        enableDisableRadar(false);
    } else {
        enableDisableRadar(true);
    }
});

//Adminsettings
mp.events.addDataHandler("Player:Adminsettings", (entity, value) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    if (entity.type === "player" && mp.players.exists(entity) && 0 !== entity.handle && entity.dimension == localPlayer.dimension) {
        let adminSettings = value.split(',');
        if (adminSettings) {
            if (adminSettings[2] == '1') {
                entity.setAlpha(254);
            } else {
                entity.setAlpha(255);
            }
            if (adminSettings[0] == '1') {
                entity.setInvincible(true);
                if (entity != localPlayer) {
                    localPlayer.setNoCollision(entity.handle, true);
                    entity.setNoCollision(localPlayer.handle, true);
                }
            } else {
                entity.setInvincible(false);
                if (entity != localPlayer) {
                    localPlayer.setNoCollision(entity.handle, false);
                    entity.setNoCollision(localPlayer.handle, false);
                }
            }
            if (adminSettings[1] == '1' || adminSettings[1] == '2') {
                if (entity != localPlayer || adminSettings[1] == '2') {
                    entity.setAlpha(0);
                } else {
                    entity.setAlpha(125);
                }
            } else {
                entity.resetAlpha();
            }
        }
    }
});

//Smartphone
mp.events.add("Client:ShowSmartphone", (json, json2, json3, json4, capacity, hide, prepaid, premium) => {
    if (hudWindow != null) {
        if (hide == 0) {
            mp.events.call('Client:UpdateHud3');
            showHandy = !showHandy;
        }
        let faction = localPlayer.getVariable('Player:Faction');
        hudWindow.execute(`gui.smartphone.showSmartphone('${json}','${json2}','${json3}','${json4}','${capacity}','${hide}','${premium}','${prepaid}','${faction}');`)
        if (showHandy == true) {
            if (hide == 0) {
                mp.events.call("Client:SetSmartphoneObj");
                mp.gui.cursor.show(true, true);
                showHideChat(false);
            }
        } else {
            mp.gui.cursor.show(false, false);
            mp.events.callRemote('Server:HideSmartphone');
            showHideChat(true);
        }
    }
})

mp.events.add("Client:SaveSmartphone", (json, json2, phonenumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SaveSmartphone', json, json2, phonenumber);
    }
})

mp.events.add("Client:UpdateSmartphone", (json, sound) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.updateSmartphone('${json}','${sound}');`)
    }
})

mp.events.add("Client:SmartphoneInfoMessage", (msg) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.sendInfoMessage('${msg}');`)
    }
})

mp.events.add("Client:SmartphoneMessageGPS", () => {
    if (hudWindow != null) {
        let zone = mp.game.gxt.get(mp.game.zone.getNameOfZone(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z));
        let text = 'Standort: ' + zone + ' - (' + localPlayer.position.x.toFixed(2) + ', ' + localPlayer.position.y.toFixed(2) + ', ' + localPlayer.position.z.toFixed(2) + ')';
        hudWindow.execute(`gui.smartphone.sendGPSMessage('${text}');`);
    }
})

mp.events.add("Client:SmartphoneMessage", (timestamp, to, from, text) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SmartphoneMessage', timestamp, to, from, text);
    }
})

mp.events.add("Client:StartSmartphoneWeather", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartSmartphoneWeather');
    }
})

mp.events.add("Client:SendDispatch", (text, name, to, phonenumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SendDispatch', text, name, to, phonenumber, true);
    }
})

mp.events.add("Client:SetStatus", (status) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SetStatus', status);
    }
})

mp.events.add("Client:LoadCapa", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadCapa');
    }
})

mp.events.add("Client:SetCapa", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.setCapa();`)
    }
})

mp.events.add("Client:ShowWeather", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.setWeatherInfo('${json}');`)
    }
})

mp.events.add("Client:ShowSmartphoneBanking", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:ShowSmartphoneBanking');
    }
})

mp.events.add("Client:ShowBanking", (json, defaultbank) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showBanking2('${json}','${defaultbank}');`)
    }
})

mp.events.add("Client:LoadLifeInvader", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadLifeInvader', 1);
    }
})

mp.events.add("Client:ShowLifeInvader", (json, show, infaction, name, online, price) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showLifeInvader('${json}','${show}','${infaction}','${name}','${online}','${price}');`)
    }
})

mp.events.add("Client:CreateAd", (text) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:CreateAd', text);
    }
})

mp.events.add("Client:ClaimAd", (adnumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:ClaimAd', adnumber);
    }
})

mp.events.add("Client:AcceptAd", (text, adnumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:AcceptAd', text, adnumber);
    }
})

mp.events.add("Client:DeleteAd", (adnumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:DeleteAd', adnumber);
    }
})

mp.events.add("Client:LoadService", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadService');
    }
})

mp.events.add("Client:ShowService", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showService('${json}');`)
    }
})

mp.events.add("Client:LoadInvoices", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadInvoices', true);
    }
})

mp.events.add("Client:ShowInvoices", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showInvoices('${json}');`)
    }
})

mp.events.add("Client:CreateTaxiPosition", () => {
    street = mp.game.pathfind.getStreetNameAtCoord(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, 0, 0);
    streetn = mp.game.ui.getStreetNameFromHashKey(street.streetName);
    mp.events.callRemote('Server:CreateTaxiPosition', streetn);
})

mp.events.add("Client:UpdateKilometreTaxi", (first) => {
    if (localPlayer.vehicle) {
        mp.events.callRemote('Server:UpdateKilometreTaxi', parseFloat(vehicleKilometre), first);
    }
})

mp.events.add("Client:LoadTaxi", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadTaxi');
    }
})

mp.events.add("Client:ShowTaxi", (json, charname, taxidrivers, istaxi) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showTaxi('${json}','${charname}','${taxidrivers}','${istaxi}');`)
    }
})

mp.events.add("Client:UpdateTaxiJobs", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.updateTaxiJobs('${json}');`)
    }
})

mp.events.add("Client:CreateTaxi", (msg) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:CreateTaxi', msg);
    }
})

mp.events.add("Client:DeclineTaxi", (taxiid) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:DeclineTaxi', taxiid);
    }
})

mp.events.add("Client:AcceptTaxi", (taxiid) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:AcceptTaxi', taxiid);
    }
})

mp.events.add("Client:PayInvoice", (id) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:PayInvoice', id);
    }
})

mp.events.add("Client:AcceptInvoice", (id) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:AcceptInvoice', id, false);
    }
})

mp.events.add("Client:LoadTwitter", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:LoadTwitter');
    }
})

mp.events.add("Client:ShowTwitter", (json, charname) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showTwitter('${json}','${charname}');`)
    }
})

mp.events.add("Client:CreateTwitter", (from, msg, timestamp) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:CreateTwitter', from, msg, timestamp);
    }
})

mp.events.add("Client:SmartphoneGetCall", (number1, number2, hidden, json, emergency) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.getCall('${number1}','${number2}','${hidden}','${json}','${emergency}');`)
    }
})

mp.events.add("Client:SmartPhoneCall", (number1, number2, hidden) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SmartphoneCall', number1, number2, hidden);
    }
})

mp.events.add("Client:EndCall", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.endCall();`)
    }
})

mp.events.add("Client:MuteMicro", (micro) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:MuteMicro', micro);
    }
})

mp.events.add("Client:CloseHandy", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:CloseHandy',);
    }
})

mp.events.add("Client:EndCallServer", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:EndCall');
    }
})

mp.events.add("Client:AcceptCall", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:AcceptCall');
    }
})

mp.events.add("Client:DeclineCall", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:DeclineCall');
    }
})

mp.events.add("Client:ShowSmartphoneBankFiles", (banknumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:ShowSmartphoneBankFiles', banknumber);
    }
})

mp.events.add("Client:GetSmartphoneBankFiles", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showBanksettings2('${json}');`)
    }
})

mp.events.add("Client:ShowSmartphoneBankSettings", (banknumber) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:ShowSmartphoneBankSettings', banknumber);
    }
})

mp.events.add("Client:GetSmartphoneBankSettings", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.smartphone.showBankfiles2('${json}');`)
    }
})

mp.events.add("Client:SetSmartphoneObj", () => {
    setTimeout(function () {
        mp.events.callRemote('Server:AddRemoveAttachments', 'smartphone', true);
    }, 420);
});

mp.events.add("Client:UnSetSmartphoneObj", () => {
    mp.events.callRemote('Server:AddRemoveAttachments', 'smartphone', false);
});

//Click
mp.events.add('click', (x, y, upOrDown, leftOrRight, relativeX, relativeY, worldPosition, hitEntity) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || showSaltyError == true || triggerAntiCheat == true) return;
    lastclick = (Date.now() / 1000);
})

//Wardrobe
mp.events.add("Client:ShowWardrobe", (json) => {
    if (hudWindow != null) {
        showwardrobe = !showwardrobe;
        nokeys = showwardrobe;
        mp.gui.cursor.show(showwardrobe, showwardrobe);
        mp.events.call('Client:UpdateHud3');
        if (showwardrobe) {
            showHideChat(false);
            hudWindow.execute(`gui.hud.showWardrobe('${json}');`)
        } else {
            showHideChat(true);
            hudWindow.execute(`gui.hud.hideWardrobe();`)
        }
    }
});

mp.events.add("Client:WardrobeAktion", (aktion, name) => {
    mp.events.callRemote('Server:WardrobeAktion', aktion, name);
});

mp.events.add("Client:UpdateWardrobe", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateWardrobe('${json}');`)
    }
});

//SetAFK
mp.events.add("Client:SetAFK", () => {
    if (death == true || editFurniture == true || ping == true) return;
    nokeys = true;
    afk = true;
    localPlayer.freezePosition(true);
    if (localPlayer.vehicle) {
        localPlayer.vehicle.freezePosition(true);
    }
    showHideChat(false);
    hideMenus(true);
    mp.events.callRemote('Server:SetAFK');
    mp.events.call('Client:ShowHud');
    mp.events.call('Client:StopAllNotifications');
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.setafk();`)
    }
})

//SetPing
mp.events.add("Client:SetPing", () => {
    if (death == true || editFurniture == true || afk == true) return;
    nokeys = true;
    ping = true;
    localPlayer.freezePosition(true);
    if (localPlayer.vehicle) {
        localPlayer.vehicle.freezePosition(true);
    }
    showHideChat(false);
    hideMenus(true);
    mp.events.call('Client:ShowHud');
    mp.events.call('Client:StopAllNotifications');
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.setping();`)
    }
})

//Gov
mp.events.add("Client:PrepareHack", () => {
    let hackStatus = 0;
    if (!localPlayer.vehicle && localPlayer.dimension == 0) {
        //Getränkeautomat
        if (CheckForSoda() > 0) {
            hackStatus = 1;
        }
        //ATM
        else if (CheckForATM() > -1) {
            hackStatus = 2;
        }
    }
    mp.events.callRemote('Server:PrepareHack', hackStatus);
})

mp.events.add("Client:StartHack", (secs) => {
    hack = true;
    nokeys = true;
    localPlayer.freezePosition(true);
    showHideChat(false);
    mp.events.call('Client:ShowHud');
    mp.events.call('Client:StopAllNotifications');
    if (hudWindow != null) {
        mp.game.audio.playSoundFrontend(-1, "Bomb_Disarmed", "GTAO_SPEED_CONVOY_SOUNDSET", true);
        hudWindow.execute(`gui.menu.startHack('${secs}');`)
    }
})

mp.events.add("Client:PlayHackBeep", () => {
    mp.game.audio.playSoundFrontend(-1, "Beep_Red", "DLC_HEIST_HACKING_SNAKE_SOUNDS", true);
})

mp.events.add("Client:StopHack", (set) => {
    if (hack == false) return;
    hack = false;
    nokeys = false;
    localPlayer.freezePosition(false);
    showHideChat(true);
    mp.events.call('Client:ShowHud');
    mp.events.callRemote('Server:StopHack', set);
})

mp.events.add("Client:StopHack2", () => {
    if (hack == false) return;
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.stopHack(-1);`)
    }
})

//Tase
mp.events.add("Client:Tase", () => {
    mp.game.gameplay.shootSingleBulletBetweenCoords(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, 1, true, 'WEAPON_STUNGUN', 0, true, false, 100.0);
})

//Gov
mp.events.add("Client:ShowGovMenu", (p0, p1, p2) => {
    showGov = !showGov;
    nokeys = showGov;
    showHideChat(showGov);
    mp.gui.cursor.show(showGov, showGov);
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showGov('${p0}','${p1}','${p2}');`)
    }
})

mp.events.add("Client:SaveGov", (csv, modus) => {
    showGov = false;
    nokeys = showGov;
    showHideChat(true);
    mp.gui.cursor.show(showGov, showGov);
    mp.events.callRemote('Server:SaveGov', csv, modus);
})

//Cityhall
mp.events.add("Client:ShowStadthalle", () => {
    showCityhall = !showCityhall;
    if (showCityhall == true) {
        mp.gui.cursor.show(true, true);
    }
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showStadthalle('${groupprices}');`)
    }
})

mp.events.add("Client:HideStadthalle", () => {
    showCityhall = false;
    hudWindow.execute(`gui.menu.hideStadthalle();`)
    mp.gui.cursor.show(false, false);
})

//Ticketsystem
mp.events.add("Client:TicketSystem", (setting, text) => {
    mp.events.callRemote('Server:TicketSystem', setting, text);
})

mp.events.add("Client:UpdateTicketCount", (ticketCount) => {
    hudWindow.execute(`gui.menu.getTicketCount('${ticketCount}');`)
})

mp.events.add("Client:LoadAllTickets", () => {
    mp.events.callRemote('Server:LoadAllTickets');
})

mp.events.add("Client:GetAllTickets", (getTickets) => {
    hudWindow.execute(`gui.menu.getAllTickets('${getTickets}');`)
})

mp.events.add("Client:LoadTicketAnswers", (ticketid) => {
    mp.events.callRemote('Server:LoadTicketAnswers', ticketid);
})

mp.events.add("Client:GetAllAnswers", (answers, oocname) => {
    hudWindow.execute(`gui.menu.getTicket('${answers}','${oocname}');`)
})

mp.events.add("Client:UpdateTicket", (ticketid) => {
    mp.events.callRemote('Server:UpdateTicket', ticketid);
})

mp.events.add("Client:TicketUpdate", (answers, ticket) => {
    hudWindow.execute(`gui.menu.ticketUpdate('${answers}','${ticket}');`)
})

//Groups
mp.events.add("Client:GroupSettings", (settings, json) => {
    if (hudWindow != null) {
        showCityhall = false;
        if (settings == 'leave') {
            showMenu = false;
        }
        mp.events.callRemote('Server:GroupSettings', settings, json);
    }
})

//VehicleSettings
mp.events.add("Client:VehicleSettings", (input, number, premium) => {
    if (hudWindow != null) {
        showCityhall = false;
        if (input == 'showui') {
            mp.events.call('Client:UpdateHud3');
            showCarSetting = true;
            mp.gui.cursor.show(true, true);
            hudWindow.execute(`gui.menu.showCarSetting('${number}','${premium}');`)
            return;
        }
        if (input == 'hide') {
            mp.events.call('Client:UpdateHud3');
            localPlayer.freezePosition(false);
            showCarSetting = false;
            hudWindow.execute(`gui.menu.showCarSetting('null','0');`)
            mp.gui.cursor.show(false, false);
        }
        mp.events.callRemote('Server:VehicleSettings', input, number);
    }
})

//Menu
mp.events.add("Client:HideMenu", () => {
    if (hudWindow != null) {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        showMenu = false;
    }
})

mp.events.add("Client:ShowCharacterStats", (json1, json2, factionname, rangname, jobname, json3) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showCharacterStats('${json1}','${json2}','${factionname}','${rangname}','${jobname}','${json3}');`)
    }
})

mp.events.add("Client:ShowFAQ", (csv, percent) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showFAQ('${csv}','${percent}');`)
    }
})

mp.events.add("Client:ShowPaydays", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showPaydays('${json}');`)
    }
})

mp.events.add("Client:ShowPaydayText", (text) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showPaydayText('${text}');`)
    }
})

mp.events.add("Client:ShowCars", (json, set) => {
    if (hudWindow != null) {
        if (set == 1) {
            let admin = localPlayer.getVariable('Player:Adminlevel');
            let adminduty = localPlayer.getVariable('Player:AdminLogin');
            let group = localPlayer.getVariable('Player:Group');
            let grouprang = localPlayer.getVariable('Player:GroupRang');
            let faction = localPlayer.getVariable('Player:Faction');
            let factionrang = localPlayer.getVariable('Player:FactionRang');
            let job = localPlayer.getVariable('Player:Job');
            showMenu = !showMenu;
            hudWindow.execute(`gui.menu.showMenu('${admin}','${adminduty}','${group}','${grouprang}','${faction}','${factionrang}','${job}');`)
            if (ticketCooldown == 0 || (Date.now() / 1000) > ticketCooldown) {
                ticketCooldown = (Date.now() / 1000) + (60 * 2);
                mp.events.callRemote('Server:CountTickets');
            }
            mp.gui.cursor.show(true, true);
        }
        hudWindow.execute(`gui.menu.showCars('${json}');`)
    }
})

mp.events.add("Client:StartStats", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartStats', null);
    }
})

mp.events.add("Client:GetPaydayText", (id) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SelectPaydayText', id);
    }
})

mp.events.add("Client:StartPaydays", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartPaydays');
    }
})

mp.events.add("Client:StartFAQ", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartFAQ');
    }
})

mp.events.add("Client:ShowCoins", (coins) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showCoins('${coins}');`)
    }
})

mp.events.add("Client:GetCoins", () => {
    if (hudWindow != null) {
        if (hudWindow != null) {
            hudWindow.execute(`gui.menu.showFactionStats('${json}','${json2}','${count}','${count2}','${leadername}','${rangs}','${mychar}');`)
        }
    }
})

mp.events.add("Client:StartCars", (input) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartCars', input, 0);
    }
})

mp.events.add("Client:SetCarRang", (id, rang, owner) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SetCarRang', id, rang, owner);
    }
})

mp.events.add("Client:SetCarName", (id, name, owner) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SetCarName', id, name, owner);
    }
})

mp.events.add("Client:StartSettings", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartSettings');
    }
})

mp.events.add("Client:ShowSettings", (newCrosshair, autologin, armor, anims) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showSettingsMenu('${newCrosshair}','${autologin}','${armor}','${anims}');`)
    }
})

mp.events.add("Client:UpdateAnimations", (animationshotkeys) => {
    mp.events.callRemote('Server:UpdateAnimations', animationshotkeys);
    try {
        animations = JSON.parse(animationshotkeys);
    } catch {
        animations = animationshotkeys;
    }
})

mp.events.add("Client:SelectSetting", (setting, settingsvalue) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:SelectSetting', setting, settingsvalue);
    }
})

//Logs
mp.events.add("Client:ShowLog", (json, name) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showLog('${json}','${name}');`)
    }
})

//Factions
mp.events.add("Client:StartFaction", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartFaction');
    }
})

mp.events.add("Client:ShowFactionStats", (json, json2, count, count2, leadername, rangs, mychar) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showFactionStats('${json}','${json2}','${count}','${count2}','${leadername}','${rangs}','${mychar}');`)
    }
})

mp.events.add("Client:UpdateFactionStats", (json, json2, count, count2, leadername, rangs, mychar) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.updateFactionStats('${json}','${json2}','${count}','${count2}','${leadername}','${rangs}','${mychar}');`)
    }
})

mp.events.add("Client:UpdateFactionRang", () => {
    if (hudWindow != null) {
        let factionrang = localPlayer.getVariable('Player:FactionRang');
        let faction = localPlayer.getVariable('Player:Faction');
        hudWindow.execute(`gui.menu.updateFactionRang('${factionrang}','${faction}');`)
    }
})

mp.events.add("Client:FactionSettings", (settings, json) => {
    if (hudWindow != null) {
        showCityhall = false;
        if (settings == 'leave') {
            showMenu = false;
        }
        mp.events.callRemote('Server:FactionSettings', settings, json);
    }
})

mp.events.add("Client:ShowFactionSettings", (json, json2, budget, update) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showFactionSettings('${json}','${json2}','${budget}','${update}');`)
    }
})

//Factions
//MDC Blip
mp.events.add("Client:SetMDCBlip", (posx, posy, posz) => {
    if (policeBlip) {
        policeBlip.destroy();
        policeBlip = null;
    } else {
        policeBlip = mp.blips.new(38, new mp.Vector3(posx, posy, posz), {
            name: 'Leitstellen Markierung',
            color: 1,
            shortRange: false,
            dimension: 0
        });
    }
})

//MDC
mp.events.add("Client:MDCSettings", (flag, data) => {
    if (hudWindow != null) {
        if (flag == "showfahndungen") {
            hudWindow.execute(`gui.mdc.showfahndungen('${data}');`)
        } else if (flag == "showkommentare") {
            hudWindow.execute(`gui.mdc.showkommentare('${data}');`)
        } else if (flag == "showplayer") {
            hudWindow.execute(`gui.mdc.showplayer('${data}');`)
        } else if (flag == "showpolicefile") {
            hudWindow.execute(`gui.mdc.showpolicefile('${data}');`)
        } else if (flag == "showusers") {
            hudWindow.execute(`gui.mdc.showusers('${data}');`)
        } else if (flag == "showcars") {
            hudWindow.execute(`gui.mdc.showcars('${data}');`)
        } else if (flag == "showhouses") {
            hudWindow.execute(`gui.mdc.showhouses('${data}');`)
        } else if (flag == "showbizz") {
            hudWindow.execute(`gui.mdc.showbizz('${data}');`)
        } else if (flag == "showinvoice") {
            hudWindow.execute(`gui.mdc.showinvoice('${data}');`)
        } else if (flag == "showpersonal") {
            hudWindow.execute(`gui.mdc.showpersonal('${data}');`)
        } else if (flag == "updateeditor") {
            hudWindow.execute(`gui.mdc.updateeditor('${data}');`)
        } else if (flag == "showhandy") {
            hudWindow.execute(`gui.mdc.showhandy('${data}');`)
        } else if (flag == "showdispatches") {
            hudWindow.execute(`gui.mdc.showdispatches('${data}');`)
        } else if (flag == "resetdispatches") {
            hudWindow.execute(`gui.mdc.resetDispatchCooldown();`)
        } else if (flag == "showfirmen") {
            hudWindow.execute(`gui.mdc.showfirmen('${data}');`)
        } else if (flag == "showweapons") {
            hudWindow.execute(`gui.mdc.showweapons('${data}');`)
        } else if (flag == "showdocuments") {
            hudWindow.execute(`gui.mdc.showdocuments('${data}');`)
        } else if (flag == "showweapon") {
            hudWindow.execute(`gui.mdc.showweapon('${data}');`)
        } else if (flag == "showweapon2") {
            hudWindow.execute(`gui.mdc.showweapon2('${data}');`)
        } else if (flag == "showblitzer") {
            hudWindow.execute(`gui.mdc.showblitzer('${data}');`)
        } else if (flag == "showplate") {
            hudWindow.execute(`gui.mdc.showplate('${data}');`)
        } else if (flag == "showcctv") {
            hudWindow.execute(`gui.mdc.showcctv('${data}');`)
        } else if (flag == "showarrested") {
            hudWindow.execute(`gui.mdc.showarrested('${data}');`)
        } else if (flag == "setupdatelivemap") {
            liveposis = JSON.parse(data);
            if (liveposis) {
                for (let i = 0; i < liveposisold.length; i++) {
                    if (liveposisold[i]) {
                        liveposisold[i].destroy();
                        liveposisold[i] = null;
                    }
                }
                var count = 0;
                let faction = localPlayer.getVariable('Player:Faction');
                liveposis.forEach(function (item, index, array) {
                    count++;
                    let posi = item.Screen.split(',');
                    if (item.Closed == 1) {
                        liveposisold[count] = mp.blips.new(672, new mp.Vector3(parseFloat(posi[0]), parseFloat(posi[1]), parseFloat(posi[2])), {
                            name: item.Name,
                            scale: 0.875,
                            color: 1,
                            shortRange: false,
                        });
                    } else {
                        liveposisold[count] = mp.blips.new(1, new mp.Vector3(parseFloat(posi[0]), parseFloat(posi[1]), parseFloat(posi[2])), {
                            name: item.Name,
                            scale: 0.875,
                            color: 1,
                            shortRange: false,
                        });
                    }
                });
            }
        } else if (flag == "setlivemap") {
            if (!livemap) {
                livemap = true;
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Livemap aktiviert, diese aktualisiert alle 45 Sekunden!','success','top-left',2500);`)
                livemapinterval = mp.events.callRemote('Server:MDCSettings', 'updatelivemap', 'n/a');
                setInterval(function () {
                    livemapinterval = mp.events.callRemote('Server:MDCSettings', 'updatelivemap', 'n/a');
                }, 44797);
            } else {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Livemap deaktiviert!','success','top-left',2500);`)
                if (livemapinterval != null) {
                    clearInterval(livemapinterval);
                }
                for (let i = 0; i < liveposisold.length; i++) {
                    if (liveposisold[i]) {
                        liveposisold[i].destroy();
                        liveposisold[i] = null;
                    }
                }
                livemap = false;
            }
        } else if (flag == "hideshow") {
            hudWindow.execute(`gui.mdc.showHideMDC();`)
            if (cctvShow == true) {
                mp.gui.cursor.show(false, false);
            } else {
                nokeys = true;
                mp.gui.cursor.show(true, true);
            }
        } else if (flag == "showmdcclient") {
            let factionrang = localPlayer.getVariable('Player:FactionRang');
            let faction = localPlayer.getVariable('Player:Faction');
            showMdc = !showMdc;
            hudWindow.execute(`gui.smartphone.showSmartphone2();`)
            hudWindow.execute(`gui.mdc.showTheMDC('${data}','${factionrang}','${faction}');`)
            if (showMdc == true) {
                showHandy = false;
                nokeys = true;
                mp.game.ui.displayHud(false);
                showHideChat(false);
                enableDisableRadar(false);
            } else {
                showHandy = true;
                nokeys = false;
                mp.game.ui.displayHud(true);
                showHideChat(false);
                enableDisableRadar(true);
            }
        } else if (flag == "closedzone") {
            let zone = mp.game.gxt.get(mp.game.zone.getNameOfZone(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z));
            mp.events.callRemote('Server:MDCSettings', 'closedzone', '' + zone + ',' + data);
        } else {
            mp.events.callRemote('Server:MDCSettings', flag, data);
        }
    }
})

//Groups
mp.events.add("Client:StartGroup", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartGroup');
    }
})

mp.events.add("Client:UpdateGroupRang", () => {
    if (hudWindow != null) {
        let grouprang = localPlayer.getVariable('Player:GroupRang');
        let group = localPlayer.getVariable('Player:Group');
        hudWindow.execute(`gui.menu.updateGroupRang('${grouprang}','${group}');`)
    }
})

mp.events.add("Client:ShowGroupStats", (json, json2, count, count2, leadername, status, rangs, mychar) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showGroupStats('${json}','${json2}','${count}','${count2}','${leadername}','${status}','${rangs}','${mychar}');`)
    }
})

mp.events.add("Client:UpdateGroupStats", (json, json2, count, count2, leadername, status, rangs, mychar) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.updateGroupStats('${json}','${json2}','${count}','${count2}','${leadername}','${status}','${rangs}','${mychar}');`)
    }
})

mp.events.add("Client:ShowGroupSettings", (json, update, provision) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showGroupSettings('${json}','${update}','${provision}');`)
    }
})

//Geworben
mp.events.add("Client:SetGeworben", (name) => {
    mp.events.callRemote('Server:SetGeworben', name);
})

//Shop
mp.events.add("Client:GetShop", (id) => {
    mp.events.callRemote('Server:GetShop', id);
})

//Business
mp.events.add("Client:StartBizz", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartBizz', false);
    }
})

mp.events.add("Client:ShowBizzStats", (json1, charactername, set) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showBizzStats('${json1}','${charactername}','${set}');`)
    }
})

mp.events.add("Client:BizzSettings", (setting, value) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:BizzSettings', setting, value);
    }
})

//House
mp.events.add("Client:SetHouseStreet", (houseid) => {
    if (hudWindow != null) {
        street = mp.game.pathfind.getStreetNameAtCoord(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, 0, 0);
        streetn = mp.game.ui.getStreetNameFromHashKey(street.streetName);
        mp.events.callRemote('Server:SetHouseStreet', houseid, streetn);
    }
})

mp.events.add("Client:StartHouse", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:StartHouse', false);
    }
})

mp.events.add("Client:ShowHouseStats", (json1, classify, charactername, tenants) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.showHouseStats('${json1}','${classify}','${charactername}','${tenants}');`)
    }
})

mp.events.add("Client:UpdateHouseStats", (json1, classify, charactername, tenants) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.menu.updateHouseStats('${json1}','${classify}','${charactername}','${tenants}');`)
    }
})

mp.events.add("Client:SwitchHouseInterior", (text1, text2, text3, text4, header, header1, header2) => {
    if (hudWindow != null) {
        mp.events.call('Client:UpdateHud3');
        InteriorSwitch = !InteriorSwitch;
        hudWindow.execute(`gui.hud.showInfobox('${text1}','${text2}','${text3}','${text4}','${header}','${header1}','${header2}','null');`)
    }
})

mp.events.add("Client:HouseSettings", (setting, value) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:HouseSettings', setting, value);
        if (setting.includes('garage')) {
            if (showCenterMenu == true) {
                setTimeout(function () {
                    showCenterMenu = false;
                    hudWindow.execute(`gui.menu.showCenterMenu('n/A','n/A','n/A');`)
                    mp.events.call('Client:UpdateHud3');
                    mp.game.ui.displayHud(true);
                    showHideChat(true);
                    enableDisableRadar(true);
                    mp.gui.cursor.show(false, false);
                    if (centerHeader == "Führungszeugnis" || centerHeader == "Fahrzeugdiagnose" || centerHeader == "Untersuchung") {
                        mp.events.callRemote('Server:StopAnimation');
                    }
                }, 150);
            }
        }
    }
})

mp.events.add("Client:StartMoebel", () => {
    if (hudWindow != null) {
        showMenu = false;
        mp.events.callRemote('Server:StartMoebel', true);
    }
})

//Furniture
mp.events.add("Client:ShowMoebel", (json1, json2, json3, count, limit) => {
    if (hudWindow != null) {
        editFurniture = false;
        showFurniture = !showFurniture;
        if (showFurniture == true) {
            setTimeout(function () {
                mp.gui.cursor.show(true, true);
                mp.game.ui.displayHud(false);
                showHideChat(false);
                enableDisableRadar(false);
            }, 150);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
        if (json3) {
            furniture = JSON.parse(json3);
        }
        hudWindow.execute(`gui.furniture.showMoebel('${json1}','${json2}','${json3}','${count}','${limit}');`);
    }
})

mp.events.add("Client:UpdateMoebelList", (json) => {
    if (hudWindow != null) {
        if (json) {
            furniture = JSON.parse(json);
        }
    }
})

mp.events.add("Client:FurnitureSettings", (setting, value) => {
    if (hudWindow != null) {
        if (setting != 'moebelmodus') {
            mp.events.callRemote('Server:FurnitureSettings', setting, value, false);
        }
        if (setting == 'editmoebel' && editFurniture == false) {
            editFurniture = true;
            modusFurniture = false;
            modusFurniture2 = false;
        }
        if (setting == 'buy' || setting == 'move') {
            editFurniture = true;
            modusFurniture = false;
            modusFurniture2 = false;
        }
        if (setting == 'close' || setting == 'delete') {
            showFurniture = false;
            editFurniture = false;
            modusFurniture = false;
            modusFurniture2 = false;
        }
        if (setting == 'hide') {
            if (value == 1) {
                mp.events.call('Client:UpdateHud3');
            }
            showFurniture = false;
            editFurniture = false;
            modusFurniture = false;
            modusFurniture2 = false;
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
        if (setting == 'moebelmodus') {
            moebelModus = !moebelModus;
            mp.events.callRemote('Server:SetMoebelModus', moebelModus);
            if (moebelModus == true) {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Möbelmodus aktiviert!','success','top-left',2500);`)
            } else {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Möbelmodus deaktiviert!','success','top-left',2500);`)
            }
        }
    }
})

mp.events.add("Client:SetFurnitureThings", () => {
    showFurniture = true;
    editFurniture = true;
});

//DL
mp.events.add("Client:SetDL", () => {
    showDl = !showDl;
    vehicleListDl = [];
    if (showDl == true) {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Fahrzeuginformationen aktiviert!','success','top-left',2500);`)
        mp.vehicles.forEachInStreamRange(veh => {
            vehicleListDl.push(veh);
        });
    } else {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Fahrzeuginformationen deaktiviert!','success','top-left',2500);`)
    }
});

//Barbershop
mp.events.add("Client:ShowBarberShop", (oldhair, oldcolor, oldbeard, oldcolor2, highlight, gender, multiplier, bizzid, overlay0, overlaycolor0, overlay1, overlaycolor1, overlay2, overlaycolor2) => {
    if (hudWindow != null) {
        barberMenu = !barberMenu;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showBarberMenu('${oldhair}','${oldcolor}','${oldbeard}','${oldcolor2}','${highlight}','${gender}','${multiplier}','${bizzid}','${overlay0}','${overlaycolor0}','${overlay1}','${overlaycolor1}','${overlay2}','${overlaycolor2}');`)
        if (barberMenu == true) {
            setTimeout(() => {
                mp.events.call('Client:CharacterCameraOn');
                mp.events.call('Client:CharacterCamera', 6);
            }, 500);
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.events.call('Client:CharacterCameraOff');
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

//Juwelier
mp.events.add("Client:ShowJuweMenu", (getarray1, getarray2, gender, multiplier) => {
    if (hudWindow != null) {
        clothMenu2 = !clothMenu2;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showJuweMenu('${getarray1}','${getarray2}','${gender}','${multiplier}');`)
        if (clothMenu2 == true) {
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

//Maskenhändler
mp.events.add("Client:ShowMaskMenu", (getarray1, getarray2, gender) => {
    if (hudWindow != null) {
        nokeys = !nokeys;
        clothMenu3 = !clothMenu3;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showMaskMenu('${getarray1}','${getarray2}','${gender}');`)
        if (clothMenu3 == true) {
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

//Outfits
mp.events.add("Client:SetDrunk", (setdrunk) => {
    if (setJoin == true) return;
    if (setdrunk) {
        mp.game.cam.setGameplayCamShakeAmplitude(2.75);
    } else {
        mp.game.cam.setGameplayCamShakeAmplitude(0.0);
    }
});

//Outfits
mp.events.add("Client:Createoutfit", (outfitname, json1, json2) => {
    mp.events.callRemote('Server:CreateOutfit', outfitname, json1, json2);
});

mp.events.add("Client:Deleteoutfit", (id) => {
    mp.events.callRemote('Server:Deleteoutfit', id);
});

mp.events.add("Client:UpdateOutfits", (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateOutfits('${json}');`)
    }
});

//Fraktionskleidung
mp.events.add("Client:ShowFactionClothing", (getarray1, getarray2, gender, faction, json) => {
    if (hudWindow != null) {
        nokeys = !nokeys;
        clothMenu4 = !clothMenu4;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showFactionClothMenu('${getarray1}','${getarray2}','${gender}','${faction}','${json}');`)
        if (clothMenu4 == true) {
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

//Clothshop
mp.events.add("Client:ShowClothMenu", (getarray1, getarray2, gender, multiplier) => {
    if (hudWindow != null) {
        clothMenu = !clothMenu;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showClothMenu('${getarray1}','${getarray2}','${gender}','${multiplier}');`)
        if (clothMenu == true) {
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

mp.events.add("Client:GetMaxClothColor", (cloth, drawable, gender = 0) => {
    let maxcolor = 0;
    let faction = localPlayer.getVariable('Player:Faction');
    if (!faction) {
        faction = 0;
    }
    if (hudWindow != null) {
        if (cloth == 'Schuh') {
            maxcolor = localPlayer.getNumberOfTextureVariations(6, drawable);
            if (gender == 1) {
                if (drawable == 25 || drawable == 97) {
                    maxcolor = 1;
                }
            } else if (gender == 2) {
                if (drawable == 25 || drawable == 101) {
                    maxcolor = 1;
                }
            }
        } else if (cloth == 'Hosen') {
            maxcolor = localPlayer.getNumberOfTextureVariations(4, drawable);
            if (gender == 1) {
                if (drawable == 121 || drawable == 35) {
                    maxcolor = 1;
                } else if (drawable == 149) {
                    maxcolor = 3;
                } else if (drawable == 135) {
                    maxcolor = 2;
                } else if (drawable == 150)
                    maxcolor = 1;
            }
        } else if (gender == 2) {
            if (drawable == 127 || drawable == 34 || drawable == 30 || drawable == 135) {
                maxcolor = 1;
            }
        } else if (cloth == 'Torso') {
            maxcolor = localPlayer.getNumberOfTextureVariations(3, drawable);
        } else if (cloth == 'Oberbekleidung') {
            maxcolor = localPlayer.getNumberOfTextureVariations(11, drawable);
            if (gender == 1) {
                if (drawable == 320 || drawable == 328) {
                    maxcolor = 1;
                } else if (drawable == 417) {
                    maxcolor = 2;
                } else if (gender == 2) {
                    if (drawable == 440 || drawable == 441 || drawable == 442) {
                        maxcolor = 15;
                    } else if (drawable == 443 || drawable == 446 || drawable == 444 || drawable == 331 || drawable == 46) {
                        maxcolor = 1;
                    } else if (drawable == 445) {
                        maxcolor = 2;
                    } else if ((drawable == 502 || drawable == 503) && faction == 3) {
                        maxcolor = 1;
                    }
                }
            }
        } else if (cloth == 'T-Shirt') {
            maxcolor = localPlayer.getNumberOfTextureVariations(8, drawable);
            if (gender == 1) {
                if (drawable == 212 || drawable == 213) {
                    maxcolor = 1;
                } else if (gender == 2) {
                    if (drawable == 253 || drawable == 254) {
                        maxcolor = 1;
                    }
                }
            }
        } else if (cloth == 'Rucksack') {
            maxcolor = localPlayer.getNumberOfTextureVariations(5, drawable);
        } else if (cloth == 'Maske') {
            maxcolor = localPlayer.getNumberOfTextureVariations(1, drawable);
        } else if (cloth == 'Brillen') {
            maxcolor = localPlayer.getNumberOfPropTextureVariations(1, drawable);
        } else if (cloth == 'Kopfbedeckung') {
            maxcolor = localPlayer.getNumberOfPropTextureVariations(0, drawable);
            if (gender == 1) {
                if (drawable == 142 || drawable == 143 || drawable == 149 || drawable == 15) {
                    maxcolor = 1;
                } else if (drawable == 123 || drawable == 124) {
                    maxcolor = 16;
                } else if (drawable == 177) {
                    maxcolor = 5;
                } else if (drawable == 189) {
                    maxcolor = 4;
                }
            } else if (gender == 2) {
                if (drawable == 141 || drawable == 142 || drawable == 45 || drawable == 148 || drawable == 114) {
                    maxcolor = 1;
                }
                if (drawable == 122 || drawable == 123) {
                    maxcolor = 16;
                }
                if (drawable == 178) {
                    maxcolor = 5;
                }
                if (drawable == 188) {
                    maxcolor = 2;
                }
            }
        } else if (cloth == 'Ohrring') {
            maxcolor = localPlayer.getNumberOfPropTextureVariations(2, drawable);
        } else if (cloth == 'Armbanduhr') {
            maxcolor = localPlayer.getNumberOfPropTextureVariations(6, drawable);
        } else if (cloth == 'Armring') {
            maxcolor = localPlayer.getNumberOfPropTextureVariations(7, drawable);
        }
        if (drawable == -1 || drawable == 255 || maxcolor == 0) {
            maxcolor = 1;
        }
        setTimeout(() => {
            hudWindow.execute(`gui.hud.setMaxClothColor('${(maxcolor - 1)}');`)
        }, 65);
    }
});

//Tattoo shop
mp.events.add("Client:ShowTattoShop", (json, gender, multiplier) => {
    if (hudWindow != null) {
        tattooShop = !tattooShop;
        showCursorTemp = !showCursorTemp;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showTattoo('${json}','${gender}','${multiplier}');`)
        if (tattooShop == true) {
            localPlayer.setComponentVariation(11, 15, 0, 0);
            localPlayer.setComponentVariation(3, 15, 0, 0);
            localPlayer.setComponentVariation(8, 15, 0, 0);
            localPlayer.setComponentVariation(4, 14, 0, 0);
            setTimeout(() => {
                mp.events.call('Client:CharacterCameraOn');
                mp.events.call('Client:CharacterCamera', 0);
            }, 500);
            localPlayer.freezePosition(true);
            mp.gui.cursor.show(true, true);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.events.call('Client:CharacterCameraOff');
            localPlayer.freezePosition(false);
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
        }
    }
});

mp.events.add("Client:BuyTattooAfter", (oldTattosTemp) => {
    hudWindow.execute(`gui.hud.updateTattoos('${oldTattosTemp}');`)
    var oldTattoos = JSON.parse(oldTattosTemp);
    for (let i = 0; i < oldTattoos.length; i++) {
        mp.players.local.setDecoration(mp.game.joaat(oldTattoos[i].dlcname), mp.game.joaat(oldTattoos[i].name));
    }
});

//Infobox
mp.events.add("Client:ShowInfobox", (text1, text2, text3, text4, header, header1, header2) => {
    if (hudWindow != null) {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        let header3 = '';
        let header4 = '';
        if (header == 'Möbelaufbau') {
            header3 = 'Modus: Position - X'
            if (speedFurniture <= 1) {
                header4 = 'Geschwindigkeit: Langsam (Taste: G)';
            } else if (speedFurniture == 2) {
                header4 = 'Geschwindigkeit: Mittel (Taste: G)';
            } else if (speedFurniture >= 3) {
                header4 = 'Geschwindigkeit: Schnell (Taste: G)';
            } else {
                header4 = 'Geschwindigkeit: Langsam (Taste: G)';
            }
        }
        hudWindow.execute(`gui.hud.showInfobox('${text1}','${text2}','${text3}','${text4}','${header}','${header1}','${header2}','${header3}','${header4}','${0}');`)
    }
})

mp.events.add("Client:UpdateInfoBox", (text1, text2, text3, text4, header, header1, header2, header4) => {
    if (hudWindow != null) {
        let header3 = '';
        let header4new = '';
        if (header == 'Möbelaufbau') {
            if (modusFurniture == false) {
                if (modusFurniture2 == false) {
                    header3 = 'Modus: Position - X';
                } else {
                    header3 = 'Modus: Position - Y';
                }
            } else {
                if (modusFurniture2 == false) {
                    header3 = 'Modus: Rotation - X';
                } else {
                    header3 = 'Modus: Rotation - Y';
                }
            }
            if (speedFurniture <= 1) {
                header4new = 'Geschwindigkeit: Langsam (Taste: G)';
            } else if (speedFurniture == 2) {
                header4new = 'Geschwindigkeit: Mittel (Taste: G)';
            } else if (speedFurniture >= 3) {
                header4new = 'Geschwindigkeit: Schnell (Taste: G)';
            } else {
                header4new = 'Geschwindigkeit: Langsam (Taste: G)';
            }
        } else {
            header4new = header4;
        }
        hudWindow.execute(`gui.hud.updateInfobox('${text1}','${text2}','${text3}','${text4}','${header}','${header1}','${header2}','${header3}','${header4new}','${0}');`)
    }
})

mp.events.add('Client:SetCameraBehindPlayer', () => {
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
})

//Busdriver
mp.events.add('Client:SetBusDriverCP', (posX, posY, posZ) => {
    if (busHandle != null) {
        busHandle.destroy();
        busHandle = null;
    }
    mp.game.ui.setNewWaypoint(posX, posY);
    busHandle = mp.checkpoints.new(49, new mp.Vector3(posX, posY, posZ - 0.5), 2.25, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: 0
    });
})

mp.events.add('Client:RemoveBusDriverCP', () => {
    if (busHandle != null) {
        busHandle.destroy();
        busHandle = null;
    }
    mp.events.call('Client:RemoveWaypoint');
    mp.events.call('Client:PressedEscape');
})

//Garbage
mp.events.add('Client:RemoveGarbageDriverCP', () => {
    if (garbageHandle != null) {
        garbageHandle.destroy();
        garbageHandle = null;
    }
    if (garbageHandle2 != null) {
        garbageHandle2.destroy();
        garbageHandle2 = null;
    }
    mp.events.callRemote('Server:AddRemoveAttachments', 'garbageBag', false);
    mp.events.call('Client:RemoveWaypoint');
    mp.events.call('Client:PressedEscape');
})

mp.events.add('Client:SetGarbageDriverCP', (playercheck, posX, posY, posZ) => {
    if (garbageHandle != null) {
        garbageHandle.destroy();
        garbageHandle = null;
    }
    if (garbageHandle2 && garbageHandle2 != null) {
        garbageHandle2.destroy();
        garbageHandle2 = null;
    }
    garbageHandle2 = mp.blips.new(318, new mp.Vector3(posX, posY, posZ), {
        name: 'Mülltonne',
        color: 1,
        shortRange: false,
    });
    if (playercheck != 0) {
        garbageHandle = mp.checkpoints.new(49, new mp.Vector3(posX, posY, posZ - 1.0), 1.5, {
            color: [255, 0, 0, 255],
            visible: true,
            dimension: 0,
            scale: 1.2
        });
    }
})

//TextToSpeech
mp.events.add('Player:ShowBusPlan', (routeName, stationName, allStations, busDriver) => {
    if (hudWindow != null && showBusPlan == false) {
        showBusPlan = true;
        nokeys = true;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showBusPlan('${routeName}','${stationName}','${allStations}','${busDriver}');`)
    }
})

//TextToSpeech
mp.events.add('Client:TextToSpeech', (text) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.textToSpeech('${text}');`)
    }
})

//Kanalreiniger
mp.events.add('Client:ShowCleaner', (x, y, z) => {
    if (cleanerHandle4 == null) {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Der nächste zu reinigende Kanal wurde auf der Karte markiert!','success','top-left',5250);`);
        cleanerHandle4 = mp.checkpoints.new(49, new mp.Vector3(x, y, z - 1.25), 1.75, {
            color: [255, 0, 0, 255],
            visible: true,
            dimension: 0
        });
        mp.game.ui.setNewWaypoint(x, y);
    } else {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Position gelöscht!','success','top-left',2250);`);
        cleanerHandle4.destroy();
        cleanerHandle4 = null;
        mp.game.ui.setNewWaypoint(localPlayer.x, localPlayer.y);
    }
})

mp.events.add('Client:StartCleaner', (dim) => {
    cleanerCount = 0;

    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Säuberungsdrohne eingelassen, mit der Taste [STRG] kannst du die verschmutzten Stellen reinigen!','success','top-left',5250);`);

    let randValue = getRandomInt(cleanerPositions.length);

    if (cleanerHandle != null) {
        cleanerHandle.destroy();
        cleanerHandle = null;
    }
    if (cleanerHandle2 != null) {
        cleanerHandle2.destroy();
        cleanerHandle2 = null;
    }
    if (cleanerHandle3 != null) {
        cleanerHandle3.destroy();
        cleanerHandle3 = null;
    }

    localPlayer.vehicle.setMaxSpeed(30 / 3.6);

    localPlayer.vehicle.setCoordsNoOffset(localPlayer.vehicle.position.x, localPlayer.vehicle.position.y, localPlayer.vehicle.position.z - 0.15, true, true, true);

    mp.events.call('Client:RemoveWaypoint');
    cleanerHandle = createObject("prop_rub_litter_03c", new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z - 0.22), new mp.Vector3(0.0, 0.0, 0.0), parseInt(dim));
    cleanerHandle2 = mp.checkpoints.new(49, new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z - 0.5), 1.15, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: parseInt(dim)
    });
    cleanerHandle3 = mp.blips.new(1, new mp.Vector3(cleanerPositions[randValue].x, cleanerPositions[randValue].y, cleanerPositions[randValue].z), {
        name: 'Reinigungspunkt',
        color: 75,
        shortRange: false,
        dimension: parseInt(dim)
    });

    cleanerTime = Date.now() / 1000 + (10);
})

//Beach Garbage
mp.events.add('Client:StopBeachGarbage', () => {
    mp.game.ui.setNewWaypoint(localPlayer.position.x, localPlayer.position.y);

    farmerCount = 0;
    farmerStatus = 0;

    if (farmerHandle != null) {
        farmerHandle.destroy();
        farmerHandle = null;
    }

    if (farmerHandle2 != null) {
        farmerHandle2.destroy();
        farmerHandle2 = null;
    }

    farmerTime = 0;
})

mp.events.add('Client:StartBeachGarbage', () => {
    farmerCount = 0;
    farmerStatus = 1337;

    if (farmerHandle != null) {
        farmerHandle.destroy();
        farmerHandle = null;
    }

    if (farmerHandle2 != null) {
        farmerHandle2.destroy();
        farmerHandle2 = null;
    }

    let randValue = getRandomInt(beachGarbagePosition.length);

    mp.game.ui.setNewWaypoint(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y);

    farmerHandle = mp.checkpoints.new(49, new mp.Vector3(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y, beachGarbagePosition[randValue].z - 0.5), 1.5, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: 0
    });

    farmerHandle2 = createObject("prop_rub_litter_03c", new mp.Vector3(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y, beachGarbagePosition[randValue].z - 0.57), new mp.Vector3(0.0, 0.0, 0.0), 0);

    farmerTime = Date.now() / 1000 + (55);
})

//Farmer
mp.events.add('Client:StopFarmer', () => {
    if (farmerStatus > 0) {
        farmerCount = 0;
        farmerStatus = 0;
        farmerTime = 0;

        if (farmerHandle != null) {
            farmerHandle.destroy();
            farmerHandle = null;
        }

        if (farmerHandle2 != null) {
            farmerHandle2.destroy();
            farmerHandle2 = null;
        }
        if (farmerHandles) {
            for (let i = 0; i < farmerHandles.length; i++) {
                if (farmerHandles[i]) {
                    farmerHandles[i].destroy();
                    farmerHandles[i] = null;
                }
            }
        }
    }
})

mp.events.add('Client:StartFarmerCow', () => {
    farmerCount = 0;
    farmerStatus = 1;

    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Bitte geh jetzt zum Kuhstall und melke die Kühe, benutze Taste [E] um die Kühe zu melken!','success','top-left',3750);`);

    shuffle(cowPosition);

    if (farmerHandle != null) {
        farmerHandle.destroy();
        farmerHandle = null;
    }

    if (farmerHandle2 != null) {
        farmerHandle2.destroy();
        farmerHandle2 = null;
    }

    farmerHandle = mp.blips.new(1, new mp.Vector3(cowPosition[farmerCount].x, cowPosition[farmerCount].y, cowPosition[farmerCount].z), {
        name: 'Kuh',
        color: 75,
        shortRange: false,
        dimension: 0
    });

    farmerHandle2 = mp.markers.new(1, new mp.Vector3(cowPosition[farmerCount].x, cowPosition[farmerCount].y, cowPosition[farmerCount].z - 0.5), 0.5, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: 0
    });

    farmerTime = Date.now() / 1000 + (20);
})

mp.events.add('Client:StartFarmerTomato', () => {
    farmerCount = 0;
    farmerStatus = 4;

    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Bitte ernte jetzt die Tomatenpflanzen!','success','top-left',3750);`);

    if (farmerHandle != null) {
        farmerHandle.destroy();
        farmerHandle = null;
    }

    if (farmerHandle2 != null) {
        farmerHandle2.destroy();
        farmerHandle2 = null;
    }

    mp.game.ui.setNewWaypoint(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y);

    farmerHandle = mp.colshapes.newSphere(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y, tomatoPosition[farmerCount].z - 0.5, 1.15);

    farmerHandle2 = mp.markers.new(1, new mp.Vector3(tomatoPosition[farmerCount].x, tomatoPosition[farmerCount].y, tomatoPosition[farmerCount].z - 0.8), 1.25, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: 0
    });

    farmerTime = Date.now() / 1000 + (25);
})

mp.events.add('Client:StartFarmerWheat', () => {
    farmerCount = 0;
    farmerStatus = 2;

    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Im ersten Schritt musst du das Weizen sähen!','success','top-left',3750);`);

    if (farmerHandle != null) {
        farmerHandle.destroy();
        farmerHandle = null;
    }

    if (farmerHandle2 != null) {
        farmerHandle2.destroy();
        farmerHandle2 = null;
    }

    mp.game.ui.setNewWaypoint(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y);

    farmerHandle = mp.colshapes.newSphere(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.2, 2);

    farmerHandles[farmerCount] = mp.markers.new(1, new mp.Vector3(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.8), 1.75, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: 0
    });

    farmerTime = Date.now() / 1000 + (45);
})

//Prison
mp.events.add('Client:StartPrison', (count, dim) => {
    prisonCount = count;
    prisonCount2 = 0;

    let randValue = getRandomInt(prisonPosition.length);

    if (prisonHandle != null) {
        prisonHandle.destroy();
        prisonHandle = null;
    }

    mp.game.ui.setNewWaypoint(prisonPosition[randValue].x, prisonPosition[randValue].y);
    prisonHandle = mp.checkpoints.new(49, prisonPosition[randValue], 4.5, {
        color: [255, 0, 0, 255],
        visible: true,
        dimension: parseInt(dim)
    });

    prisonTime = Date.now() / 1000 + (12);
})

mp.events.add('Client:StopPrison', () => {
    prisonTime = 0;
    prisonCount = 0;
    prisonCount2 = 0;
    if (prisonHandle != null) {
        prisonHandle.destroy();
        prisonHandle = null;
    }
    mp.game.ui.setNewWaypoint(parseFloat(localPlayer.position.x), parseFloat(localPlayer.position.y));
    mp.events.callRemote('Server:EndPrison', 1);
})

//Ammuquestions
mp.events.add('Client:StartRange', (targetCount, set, set2) => {
    startRange = true;
    rangePoints = targetCount - 1;
    let randValue = 0;
    if (set != 3) {
        randValue = getRandomInt(13);
        getPoint = randValue;
        rangeHandle = createObject("gr_prop_gr_target_05b", oldTargetPosition[getPoint], new mp.Vector3(0.0, 0.0, -18.30), localPlayer.dimension);
    } else {
        randValue = getRandomInt(9);
        getPoint = randValue;
        rangeHandle = createObject("gr_prop_gr_target_05b", oldTargetPosition2[getPoint], new mp.Vector3(0.0, 0.0, -21.30), localPlayer.dimension);
    }
    rangeStatus = set;
    rangeTime = Date.now() / 1000;
    if (set == 1) {
        mp.events.callRemote('Server:StartRange', set2, 0);
    }
})

mp.events.add('Client:StopRange', () => {
    if (startRange == true) {
        if (rangeHandle != null) {
            rangeHandle.destroy()
            rangeHandle = null;
        }
        rangePoints = 0;
        rangeStatus = 0;
        getPoint = 0;
        startRange = false;
    }
})

mp.events.add("Client:ShowAmmuQuiz", () => {
    if (hudWindow != null) {
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        hudWindow.execute(`gui.rpquiz.startAmmuQuiz();`)
    }
})

mp.events.add("Client:StopAmmuQuiz", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.rpquiz.stopAmmuQuiz();`)
        mp.events.callRemote('Server:StopAmmu');
    }
})

//Driving school
mp.events.add("Client:ShowCarQuiz", (id) => {
    if (hudWindow != null) {
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        if (id == 1) {
            hudWindow.execute(`gui.rpquiz.startCarQuiz();`)
        } else if (id == 2) {
            hudWindow.execute(`gui.rpquiz.startBikeQuiz();`)
        } else if (id == 3) {
            hudWindow.execute(`gui.rpquiz.startTruckerQuiz();`)
        } else if (id == 4) {
            hudWindow.execute(`gui.rpquiz.startBootsQuiz();`)
        } else if (id == 5) {
            hudWindow.execute(`gui.rpquiz.startPlaneQuiz();`)
        }
    }
})

mp.events.add("Client:StopCarQuiz", (id) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.rpquiz.stopCarQuiz('${id}');`)
    }
})

mp.events.add("Client:StartCar", (id) => {
    mp.events.callRemote('Server:StartCar', id);
    mp.events.call('Client:StartCar2', id);
})

mp.events.add("Client:StopCar", () => {
    if (drivingSchoolHandle != null) {
        drivingSchoolHandle.destroy();
        drivingSchoolHandle = null;
    }
    if (drivingSchoolHandle2 != null) {
        drivingSchoolHandle2.destroy();
        drivingSchoolHandle2 = null;
    }
    drivingSchoolCount = 0;
    drivingSchoolPositions = [];
    mp.game.ui.setNewWaypoint(localPlayer.position.x, localPlayer.position.y);
})

mp.events.add("Client:StartCar2", (id) => {
    if (id > -1) {
        if (drivingSchoolHandle != null) {
            drivingSchoolHandle.destroy();
            drivingSchoolHandle = null;
        }

        if (drivingSchoolHandle2 != null) {
            drivingSchoolHandle2.destroy();
            drivingSchoolHandle2 = null;
        }

        drivingSchoolCount = 0;

        if (id == 1) {
            drivingSchoolPositions = drivingSchoolPositions1;
        } else if (id == 2) {
            drivingSchoolPositions = drivingSchoolPositions2;
        } else if (id == 3) {
            drivingSchoolPositions = drivingSchoolPositions3;
        } else if (id == 4) {
            drivingSchoolPositions = drivingSchoolPositions4;
        } else if (id == 5) {
            drivingSchoolPositions = drivingSchoolPositions5;
        }

        if (id != 5) {
            drivingSchoolHandle = mp.checkpoints.new(49, new mp.Vector3(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y, drivingSchoolPositions[drivingSchoolCount].z - 0.5), 1.65, {
                color: [255, 0, 0, 255],
                visible: true,
                dimension: 0
            });
        } else {
            drivingSchoolHandle2 = mp.colshapes.newCircle(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y, 15, 0);
        }

        mp.game.ui.setNewWaypoint(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y);
    }
})

//RP Questions
mp.events.add("Client:ShowRPQuestions", () => {
    if (hudWindow != null) {
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        hudWindow.execute(`gui.rpquiz.startRPQuiz();`)
    }
})

mp.events.add("Client:HideRPQuestions", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.rpquiz.stopRPQuiz();`)
    }
})

mp.events.add("Client:RPQuizFinish", (error) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:RPQuizFinish', error);
    }
})

//Tutorials
mp.events.add("Client:ShowBlackFadeIn", (text, legal) => {
    if (hudWindow != null) {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        hudWindow.execute(`gui.hud.showBlackFadeIn('${text}');`)
        if (legal == 1) {
            hudcam = mp.cameras.new('hudCam', new mp.Vector3(-1387.3477, -812.4736, 23.3596), new mp.Vector3(0, 0, 0), 40);
            hudcam.pointAtCoord(-1377.3195, -816.5130, 19.5074);
        } else {
            hudcam = mp.cameras.new('hudCam', new mp.Vector3(73.97, -3396.195, 11.157), new mp.Vector3(0, 0, 0), 40);
            hudcam.pointAtCoord(151.54, -3323.36, 8.21);
        }
        hudcam.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 0, false, false);

    }
})

mp.events.add("Client:FinishEinreise", (eyecolor, education, skills, appearance, size) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:FinishEinreise', eyecolor, education, skills, appearance, size);
        mp.game.cam.renderScriptCams(false, false, 0, false, false);
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
    }
})

mp.events.add("Client:ShowBlackFadeOut", (legal) => {
    if (hudWindow != null) {
        hudcam.destroy();
        hudcam = null;
        mp.game.cam.renderScriptCams(false, false, 0, false, false);
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        let name = localPlayer.getVariable('Player:CharacterName');
        hudWindow.execute(`gui.hud.showTutorialStadthalle('${legal}','${name}');`)
    }
})


//Take Picture
mp.events.add("Client:TakePicture", (screenname, sound) => {
    setTimeout(() => {
        mp.gui.takeScreenshot("nWorld-Screen.jpg", 0, 60, 100);
        if (sound == 1) {
            mp.game.audio.playSoundFrontend(-1, "Camera_Shoot", "Phone_Soundset_Franklin", true);
        }
    }, 150);
    setTimeout(() => {
        mp.events.call("Client:CharacterScreenshotDone", screenname);
    }, 250);
});

mp.events.add("Client:CharacterScreenshotDone", (screenname) => {
    mp.events.call("Client:ScreenshotTaken", screenname);
})

//Key F2
mp.keys.bind(0x71, true, function () {
    mp.events.call("Client:PressF2");
});

mp.events.add("Client:PressF2", () => {
    if (showSaltyError == true || triggerAntiCheat == true || nokeys == true || showWheel == true || showInventory == true || showFurniture == true || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || clothMenu == true || clothMenu2 == true || showSped == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || afk == true || ping == true || hack == true) return;
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (localPlayer.isTypingInTextChat || !spawned) return;
    if (showMenu == false && (InteriorSwitch == true || showBank == true)) return;
    let admin = localPlayer.getVariable('Player:Adminlevel');
    let adminduty = localPlayer.getVariable('Player:AdminLogin');
    let group = localPlayer.getVariable('Player:Group');
    let grouprang = localPlayer.getVariable('Player:GroupRang');
    let faction = localPlayer.getVariable('Player:Faction');
    let factionrang = localPlayer.getVariable('Player:FactionRang');
    let job = localPlayer.getVariable('Player:Job');
    showMenu = !showMenu;
    mp.events.call('Client:StopAllNotifications');
    hudWindow.execute(`gui.menu.showMenu('${admin}','${adminduty}','${group}','${grouprang}','${faction}','${factionrang}','${job}','${level}');`)
    if (ticketCooldown == 0 || (Date.now() / 1000) > ticketCooldown) {
        ticketCooldown = (Date.now() / 1000) + (60 * 2);
        mp.events.callRemote('Server:CountTickets');
    }
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    showHideChat(false);
    enableDisableRadar(false);
});

//Key F
mp.keys.bind(0x46, true, function () {
    if (pressedF == 0 || (Date.now() / 1000) > pressedF) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showInventory == true || barberMenu == true || tattooShop == true) return;
        pressedF = (Date.now() / 1000) + (2);
        let sodaCheck = CheckForSoda();
        let trashCheck = CheckForTrash();

        let trashCheck2 = false;
        if (sodaCheck == 0 && trashCheck == -1) {
            let atmCheck = CheckForATM();
            mp.events.callRemote('Server:OnPlayerPressF', atmCheck);
        } else {
            if (localPlayer.weapon == mp.game.joaat('weapon_unarmed') || localPlayer.weapon == mp.game.joaat('weapon_knuckle')) {
                if (sodaCheck > 0) {
                    mp.events.callRemote('Server:GetSoda', sodaCheck);
                } else if (trashCheck > -1) {
                    if (containsObject(trashCheck, listTrash)) {
                        trashCheck2 = true;
                    }
                    if (trashCheck2 == false) {
                        listTrash.push(trashCheck);
                    }
                    mp.events.callRemote('Server:GetTrash', trashCheck2);
                }
            }
        }
    }
});

//Key Einfügen
mp.keys.bind(0x2D, true, function () {
    if (pressedEinf == 0 || (Date.now() / 1000) > pressedEinf) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || nokeys == true || death == true || cuffed == true || !spawned || showHandy == true || pointing.active == true || editFurniture == true || startRange == true || showDealer == true || showTab == true || localPlayer.isInAnyVehicle(true) || barberMenu == true || tattooShop == true) return;
        pressedEinf = (Date.now() / 1000) + (1);
        handsUp = !handsUp;
        mp.events.callRemote('Server:SetHandsUp');
    }
});

//Key F3
mp.keys.bind(0x72, true, function () {
    if (pressedF3 == 0 || (Date.now() / 1000) > pressedF3) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
        pressedF3 = (Date.now() / 1000) + (3);
        mp.events.callRemote('Server:OnPlayerPressF3');
    }
});

//Key F4
mp.keys.bind(0x73, true, function () {
    if (pressedF4 == 0 || (Date.now() / 1000) > pressedF4) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
        pressedF4 = (Date.now() / 1000) + (3);
        mp.events.callRemote('Server:OnPlayerPressF4');
    }
});

//Key F5
mp.keys.bind(0x74, true, function () {
    if (pressedF5 == 0 || (Date.now() / 1000) > pressedF5) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || handsUp == true || showTuning == true || specTarget != null || barberMenu == true || tattooShop == true) return;
        mp.events.callRemote('Server:OnPlayerPressF5', 0);
        pressedF5 = (Date.now() / 1000) + (1);
    }
});

//Key F6
mp.keys.bind(0x75, true, function () {
    if (pressedF6 == 0 || (Date.now() / 1000) > pressedF6) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || handsUp == true || showTuning == true || specTarget != null || barberMenu == true || tattooShop == true) return;
        if (localPlayer.isInAnyVehicle(true) && hudWindow != null && nokeys == false && death == false) {
            let faction = localPlayer.getVariable('Player:Faction');
            if (faction == 1 && localPlayer.vehicle.getVariable('Vehicle:Name') != 'stockade' && (localPlayer.vehicle.getPedInSeat(-1) === localPlayer.handle || localPlayer.vehicle.getPedInSeat(0) === localPlayer.handle) && (localPlayer.vehicle.getNumberPlateText().includes("LSPD") || localPlayer.vehicle.getNumberPlateText().includes("S-W-A-T"))) {
                if (localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 15 && localPlayer.vehicle.getClass() != 16) {
                    pdCam = !pdCam;
                    if (pdCam) {
                        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Lasermessgerät aktiviert!','success','top-left',1500);`);
                    } else {
                        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Lasermessgerät deaktiviert!','success','top-left',1500);`);
                    }
                    return;
                }
            }
        }
        mp.events.callRemote('Server:OnPlayerPressF6', nokeys);
        pressedF6 = (Date.now() / 1000) + (2);
    }
});

//Key F7
mp.keys.bind(0x76, true, function () {
    if (pressedF7 == 0 || (Date.now() / 1000) > pressedF7) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || handsUp == true || showTuning == true || specTarget != null || barberMenu == true || tattooShop == true || showHandy == true) return;
        mp.events.callRemote('Server:OnPlayerPressF7', false);
        pressedF7 = (Date.now() / 1000) + (2);
    }
});

//Key F8
mp.keys.bind(0x77, true, function () {
    if (pressedF8 == 0 || (Date.now() / 1000) > pressedF8) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showAmmu == true || showShop == true || startRange == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || handsUp == true || barberMenu == true || tattooShop == true) return;
        if (hudWindow != null) {
            pressedF8 = (Date.now() / 1000) + (2);
            if (localPlayer.vehicle) {
                if (showTuning == false) {
                    mp.events.callRemote('Server:ShowTuningMenu');
                    return;
                } else {
                    hudWindow.execute(`gui.hud.resetTuning('0');`)
                    hudWindow.execute(`gui.hud.syncTuningFunc('0');`)
                    mp.events.call('Client:HideTuningMenu');
                    return;
                }
            } else {
                showMecha = !showMecha;
                mp.events.callRemote('Server:ShowMechaMenu');
                mp.events.callRemote('Server:OnPlayerPressF8', false);
            }
        }
    }
});

//Key F9
mp.keys.bind(0x78, true, function () {
    if (pressedF9 == 0 || (Date.now() / 1000) > pressedF9) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showAmmu == true || showShop == true || startRange == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || handsUp == true || barberMenu == true || tattooShop == true || afk == true || ping == true || hack == true) return;
        if (hudWindow != null) {
            pressedF9 = (Date.now() / 1000) + (1);
        }
    }
});

//Key STRG
mp.keys.bind(0xA2, true, function () {
    if (pressedSTRG == 0 || (Date.now() / 1000) > pressedSTRG) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        let funmodus = localPlayer.getVariable('Player:Funmodus');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true) return;
        if (localPlayer.vehicle && funmodus) {
            let currentvelo = localPlayer.vehicle.getVelocity();
            currentvelo.x = currentvelo.x + 0.1;
            currentvelo.y = currentvelo.y + 0.1;
            currentvelo.z = currentvelo.z + 10.5;
            localPlayer.vehicle.setVelocity(currentvelo.x, currentvelo.y, currentvelo.z);
        } else if (localPlayer.vehicle && cleanerHandle != null) {
            dist = distanceVector(localPlayer.position, cleanerHandle.position);
            if (dist <= 2.15 && cleaningTask == false) {
                if (hudWindow != null) {
                    localPlayer.vehicle.freezePosition(true);
                    mp.events.call('Client:ShowSpeedometer');
                    mp.events.call('Client:StartLockpicking', 5, 'cleaning', 'Reinigung läuft ...');
                    while (!mp.game.streaming.hasNamedPtfxAssetLoaded("core")) {
                        mp.game.streaming.requestNamedPtfxAsset("core");
                        mp.game.wait(0);
                    }
                    cleaningTask = true;
                }
            }
        } else {
            mp.events.callRemote('Server:OnPlayerPressSTRG');
            pressedSTRG = (Date.now() / 1000) + (2);
        }
    }
});

//Key F10
mp.keys.bind(0x79, true, function () {
    if (pressedF10 == 0 || (Date.now() / 1000) > pressedF10) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true) return;
        if (mp.gui.cursor.visible) {
            mp.gui.cursor.show(false, false);
        } else {
            mp.gui.cursor.show(true, true);
        }
        pressedF10 = (Date.now() / 1000) + (1);
    }
});

//Key K
mp.keys.bind(0x4B, true, function () {
    if (pressedK == 0 || (Date.now() / 1000) > pressedK) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || cuffed == true || showSped == true || showFurniture == true || editFurniture == true) return;
        pressedK = (Date.now() / 1000) + (2);
        if (showTuning == true || showAmmu == true || barberMenu == true || tattooShop == true || clothMenu == true || clothMenu2 == true || clothMenu4 == true || showSped == true) {
            showCursorTemp = !showCursorTemp;
            mp.gui.cursor.show(showCursorTemp, showCursorTemp);
            if (barberMenu == true) {
                if (showCursorTemp == false) {
                    mp.events.call('Client:CharacterCameraOff');
                } else {
                    setTimeout(() => {
                        mp.events.call('Client:CharacterCameraOn');
                        mp.events.call('Client:CharacterCamera', 6);
                    }, 500);
                }
            }
            if (tattooShop == true) {
                if (showCursorTemp == false) {
                    mp.events.call('Client:CharacterCameraOff');
                } else {
                    setTimeout(() => {
                        mp.events.call('Client:CharacterCameraOn');
                        mp.events.call('Client:CharacterCamera', 0);
                    }, 500);
                }
            }
            if (clothMenu == true || clothMenu2 == true || clothMenu3 == true || clothMenu4 == true) {
                if (showCursorTemp == false) {
                    mp.events.call('Client:CharacterCameraOff');
                } else {
                    setTimeout(() => {
                        mp.events.call('Client:CharacterCameraOn');
                        mp.events.call('Client:CharacterCamera', 0);
                    }, 500);
                }
            }
            return;
        }
        if (nokeys == true) return;
        if (showWheel == false & !localPlayer.isInAnyVehicle(true)) {
            if ((wheelSelected == 0 || wheelSelected == 2) && showInventory == false && showMenu == false && prisonCount == 0 && showCenterMenu == false && showBank == false && clothMenu == false && clothMenu2 == false && showSped == false && showCarSetting == false && showCityhall == false && showFuel == false && showAmmu == false && showShop == false && showShop2 == false && startRange == false && showDealer == false && showTab == false && showHandy == false && showTuning == false && specTarget == null && nokeys == false && death == false && cuffed == false && barberMenu == false && tattooShop == false) {
                wheelSelected = 2;
                showWheel = true;
                hudWindow.execute(`gui.selectwheel.showWheel(2);`)
                mp.gui.cursor.show(true, true);
                return;
            }
        } else {
            wheelSelected = 0;
            showWheel = false;
            hudWindow.execute(`gui.selectwheel.hideWheel();`)
            mp.gui.cursor.show(false, false);
            return;
        }
    }
});

//Key X
mp.keys.bind(0x58, true, function () {
    if (pressedX == 0 || (Date.now() / 1000) > pressedX) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        pressedX = (Date.now() / 1000) + (2);
        if (showWheel == false) {
            if ((wheelSelected == 0 || wheelSelected == 1) && showInventory == false && showMenu == false && prisonCount == 0 && showCenterMenu == false && showBank == false && clothMenu == false && clothMenu2 == false && showSped == false && showCarSetting == false && showCityhall == false && showFuel == false && showAmmu == false && showShop == false && showShop2 == false && startRange == false && showDealer == false && showTab == false && showHandy == false && showTuning == false && specTarget == null && nokeys == false && death == false && cuffed == false && barberMenu == false && tattooShop == false && showFurniture == false && editFurniture == false) {
                wheelSelected = 1;
                showWheel = true;
                hudWindow.execute(`gui.selectwheel.showWheel(1);`)
                mp.gui.cursor.show(true, true);
                return;
            }
        } else {
            wheelSelected = 0;
            showWheel = false;
            mp.gui.cursor.show(false, false);
            hudWindow.execute(`gui.selectwheel.hideWheel();`)
            return;
        }
    }
});

//Key L
mp.keys.bind(0x4C, true, function () {
    if (pressedL == 0 || (Date.now() / 1000) > pressedL) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true) return;
        if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || showCenterMenu == true || showBank == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedL = (Date.now() / 1000) + (3);
        mp.events.callRemote('Server:OnEntityLock');
    }
});

//Key M
mp.keys.bind(0x4D, true, function () {
    if (pressedM == 0 || (Date.now() / 1000) > pressedM) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || showMenu == true) return;
        if (localPlayer.vehicle && showTuning == false) {
            mp.events.callRemote('Server:VehicleEngine');
        }
        if (moebelModus == true && showMenu == false && !localPlayer.vehicle) {
            if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || showFurniture == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showSped == true || showCityhall == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
            mp.events.callRemote('Server:FurnitureSettings', 'showmenu', 0, false);
        }
        let faction = localPlayer.getVariable('Player:Faction');
        if (showWheel == false && !localPlayer.isInAnyVehicle(true) && (faction == 1 || faction == 2 || faction == 3)) {
            if ((wheelSelected == 0 || wheelSelected == 3) && showInventory == false && showMenu == false && prisonCount == 0 && showCenterMenu == false && showBank == false && clothMenu == false && clothMenu2 == false && showSped == false && showCarSetting == false && showCityhall == false && showFuel == false && showAmmu == false && showShop == false && showShop2 == false && startRange == false && showDealer == false && showTab == false && showHandy == false && showTuning == false && specTarget == null && cuffed == false && nokeys == false && death == false && barberMenu == false && tattooShop == false && showFurniture == false && editFurniture == false) {
                wheelSelected = 3;
                showWheel = true;
                if (faction == 1) {
                    hudWindow.execute(`gui.selectwheel.showWheel(3);`)
                } else if (faction == 2) {
                    hudWindow.execute(`gui.selectwheel.showWheel(4);`)
                } else if (faction == 3) {
                    hudWindow.execute(`gui.selectwheel.showWheel(5);`)
                }
                mp.gui.cursor.show(true, true);
                return;
            }
        } else {
            wheelSelected = 0;
            showWheel = false;
            hudWindow.execute(`gui.selectwheel.hideWheel();`)
            mp.gui.cursor.show(false, false);
            return;
        }
        pressedM = (Date.now() / 1000) + (2);
    }
});

//Key H
mp.keys.bind(0x48, true, function () {
    if (pressedH == 0 || (Date.now() / 1000) > pressedH) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || showCenterMenu == true || showBank == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        if (!localPlayer.vehicle) {
            if (checkChat == 1) {
                checkChat = 0;
                showHideChat(false);
                mp.storage.data.showChat = 0;
            } else {
                checkChat = 1;
                showHideChat(true);
                mp.storage.data.showChat = 1;
            }
            mp.storage.flush();
        }
        pressedH = (Date.now() / 1000) + (1);
    }
});

//Key 0
mp.keys.bind(0x30, true, function () {
    if (pressed0 == 0 || (Date.now() / 1000) > pressed0) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || afk == true || ping == true || hack == true) return;
        switchWeapon(1337);
        pressed0 = (Date.now() / 1000) + (2);
    }
});

//Key 1
mp.keys.bind(0x31, true, function () {
    if (pressed1 == 0 || (Date.now() / 1000) > pressed1) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || afk == true || ping == true || hack == true) return;
        if (!localPlayer.vehicle) {
            switchWeapon(1);
        }
        if (localPlayer.vehicle && !mp.gui.cursor.visible) {
            if (localPlayer.vehicle.getClass() != 8 && localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 15 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 21 && localPlayer.vehicle.getClass() != 22) {
                if (localPlayer.vehicle.getPedInSeat(-1) === localPlayer.handle) {
                    localPlayer.vehicle.windows[0] = !localPlayer.vehicle.windows[0];
                    mp.events.callRemote('Server:VehicleControl', localPlayer.vehicle, JSON.stringify(localPlayer.vehicle.windows), 0);
                } else if (localPlayer.vehicle.getPedInSeat(0) === localPlayer.handle) {
                    localPlayer.vehicle.windows[1] = !localPlayer.vehicle.windows[1];
                    mp.events.callRemote('Server:VehicleControl', localPlayer.vehicle, JSON.stringify(localPlayer.vehicle.windows), 0);
                } else if (localPlayer.vehicle.getPedInSeat(1) === localPlayer.handle) {
                    localPlayer.vehicle.windows[2] = !localPlayer.vehicle.windows[2];
                    mp.events.callRemote('Server:VehicleControl', localPlayer.vehicle, JSON.stringify(localPlayer.vehicle.windows), 0);
                } else if (localPlayer.vehicle.getPedInSeat(2) === localPlayer.handle) {
                    localPlayer.vehicle.windows[3] = !localPlayer.vehicle.windows[3];
                    mp.events.callRemote('Server:VehicleControl', localPlayer.vehicle, JSON.stringify(localPlayer.vehicle.windows), 0);
                }
            }
        }
        pressed1 = (Date.now() / 1000) + (2);
    }
});

//Key 2
mp.keys.bind(0x32, true, function () {
    if (pressed2 == 0 || (Date.now() / 1000) > pressed2) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || afk == true || ping == true || hack == true) return;
        switchWeapon(2);
        pressed2 = (Date.now() / 1000) + (2);
    }
});

//Key 3
mp.keys.bind(0x33, true, function () {
    if (pressed3 == 0 || (Date.now() / 1000) > pressed3) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || afk == true || ping == true || hack == true) return;
        switchWeapon(3);
        pressed3 = (Date.now() / 1000) + (2);
    }
});

//Key 4
mp.keys.bind(0x34, true, function () {
    if (pressed4 == 0 || (Date.now() / 1000) > pressed4) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || afk == true || ping == true || hack == true) return;
        switchWeapon(4);
        pressed4 = (Date.now() / 1000) + (2);
    }
});

//Key Tab
mp.keys.bind(0x09, true, function () {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || prisonCount > 0 || showCenterMenu == true == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || showCarSetting == true || showCityhall == true || showSped == true || showHandy == true || showTuning == true || InteriorSwitch == true || editFurniture == true || showFurniture == true || showBank == true || showWheel == true || showMenu == true || afk == true || ping == true || hack == true) return;
    if (pressedTab == 0 || (Date.now() / 1000) > pressedTab) {
        mp.events.callRemote('Server:ShowTabMenu');
        pressedTab = (Date.now() / 1000) + (20);
    } else {
        if (showTab == true) {
            mp.events.call('Client:UpdateHud3');
            hudWindow.execute(`gui.menu.showTabList('n/A');`)
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            mp.gui.cursor.show(false, false);
            showTab = false;
        } else {
            showTab = true;
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
            mp.gui.cursor.show(true, true);
            mp.events.call('Client:UpdateHud3');
            hudWindow.execute(`gui.menu.showTabList('${tabJson}');`)
        }
    }
});

//Key I
mp.keys.bind(0x49, true, function () {
    if ((pressedI == 0 || (Date.now() / 1000) > pressedI) && showMenu == false) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || specTarget != null) return;
        if (showInventory == false && (InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || death == true || showFuel == true || showCarSetting == true || showCityhall == true || showSped == true || showHandy == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || editFurniture == true || showFurniture == true || barberMenu == true || tattooShop == true || afk == true || ping == true || hack == true)) return;
        pressedI = (Date.now() / 1000) + (1);
        let found = 0;
        let closestVeh = getClosestVehicle(localPlayer.position);
        if (closestVeh) {
            if (closestVeh.distance < 5.0) {
                let closestDoors = JSON.parse(closestVeh.vehicle.getVariable('Vehicle:Doors'));
                let boneIndex = closestVeh.vehicle.getBoneIndexByName('boot');
                let bonePos = closestVeh.vehicle.getWorldPositionOfBone(boneIndex);
                if (closestDoors[5] && mp.game.gameplay.getDistanceBetweenCoords(bonePos.x, bonePos.y, bonePos.z, localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, false) <= 1.85) {
                    found = 1;
                }
                boneIndex = closestVeh.vehicle.getBoneIndexByName('bonnet');
                bonePos = closestVeh.vehicle.getWorldPositionOfBone(boneIndex);
                if (closestDoors[4] && mp.game.gameplay.getDistanceBetweenCoords(bonePos.x, bonePos.y, bonePos.z, localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, false) <= 1.85) {
                    found = 2;
                }
            }
        }
        if (localPlayer.vehicle) {
            mp.events.call('Client:ShowSpeedometer');
        }
        mp.events.callRemote('Server:ShowInventory', found, closestVeh.vehicle);
    }
});

//Key P
mp.keys.bind(0x50, true, function () {
    if (pressedP == 0 || (Date.now() / 1000) > pressedP) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showCityhall == true || showSped == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
        if (setCiga == true) {
            if (localPlayer.vehicle) {
                mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleCiga', false);
            } else {
                mp.events.callRemote('Server:AddRemoveAttachments', 'handCiga', false);
            }
            if (hudWindow != null) {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Zigarette weggeworfen!','success','top-left',1500);`);
            }
            setCiga = false;
        } else if (setJoint == true) {
            if (localPlayer.vehicle) {
                mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleJoint', false);
            } else {
                mp.events.callRemote('Server:AddRemoveAttachments', 'handJoint', false);
            }
            if (hudWindow != null) {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Joint weggeworfen!','success','top-left',1500);`);
            }
            setJoint = false;
        } else {
            if (!localPlayer.isInAnyVehicle(true)) {
                mp.events.callRemote('Server:PlayerPickUp');
            }
        }
        pressedP = (Date.now() / 1000) + (2);
    }
});

//Key B (Key pressed)
mp.keys.bind(0x42, true, function () {
    if (pressedB == 0 || (Date.now() / 1000) > pressedB) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        pressedB = (Date.now() / 1000) + (2);
        if (InteriorSwitch == true) {
            mp.events.callRemote('Server:HouseSettings', 'buyinterior', 0);
        } else if (showFurniture == true && editFurniture == true) {
            editFurniture = false;
            modusFurniture = false;
            modusFurniture2 = false;
            showFurniture = false;
            mp.events.callRemote('Server:FurnitureSettings', 'set', 0, false);
            mp.events.call('Client:ShowInfobox', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null');
        }
    }
});

//Key E
mp.keys.bind(0x45, true, function () {
    if (pressedE == 0 || (Date.now() / 1000) > pressedE) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        let set = false;
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned) return;
        if (nokeys == true || death == true || cuffed == true) return;
        if (farmerStatus == 1) {
            distance = distanceVector(localPlayer.position, cowPosition[farmerCount]);
            if (distance <= 1.55) {
                mp.events.callRemote('Server:CowMilking');
            }
            pressedE = (Date.now() / 1000) + (2);
            return;
        }
        if (showFuel == false) {
            if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
            for (let i = 0; i < fuelPositions.length; i++) {
                distance = distanceVector(localPlayer.position, fuelPositions[i]);
                if (distance <= 4.0) {
                    mp.events.callRemote('Server:ShowFuelStation');
                }
            }
        }
        if (moebelModus == true) {
            if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showCarSetting == true || showSped == true || showCityhall == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
            mp.events.callRemote('Server:FurnitureSettings', 'editmoebelm', 0, false);
            return;
        }
        mp.events.callRemote('Server:OnPlayerPressE');
        if (!localPlayer.vehicle && !mp.gui.cursor.visible) {
            if (target && target.pushTime + 1 >= Date.now() / 1000 && target.veh.doesExist()) {
                target.veh.doors[target.id] = !target.veh.doors[target.id];
                mp.events.callRemote('Server:VehicleControl', target.veh, JSON.stringify(target.veh.doors), 1);
                set = true;
            }
            if (!set) {
                let closestVeh = getClosestVehicle(localPlayer.position);
                let locked = closestVeh.vehicle.getDoorLockStatus();
                if (closestVeh && locked == 1) {
                    if (closestVeh.distance <= 5.0) {
                        let boneIndex = closestVeh.vehicle.getBoneIndexByName('boot');
                        let bonePos = closestVeh.vehicle.getWorldPositionOfBone(boneIndex);
                        if (mp.game.gameplay.getDistanceBetweenCoords(bonePos.x, bonePos.y, bonePos.z, localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, false) <= 1.85) {
                            closestVeh.vehicle.doors[5] = !closestVeh.vehicle.doors[5];
                            mp.events.callRemote('Server:VehicleControl', closestVeh.vehicle, JSON.stringify(closestVeh.vehicle.doors), 1);
                            set = true;
                        }

                        boneIndex = closestVeh.vehicle.getBoneIndexByName('bonnet');
                        bonePos = closestVeh.vehicle.getWorldPositionOfBone(boneIndex);
                        if (mp.game.gameplay.getDistanceBetweenCoords(bonePos.x, bonePos.y, bonePos.z, localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, false) <= 2.15) {
                            closestVeh.vehicle.doors[4] = !closestVeh.vehicle.doors[4];
                            mp.events.callRemote('Server:VehicleControl', closestVeh.vehicle, JSON.stringify(closestVeh.vehicle.doors), 1);
                            set = true;
                        }
                    }
                }
            }
        }
        pressedE = (Date.now() / 1000) + (2);
    }
});

//Key Pfeiltaste rechts
mp.keys.bind(0x27, true, function () {
    if (pressedRight == 0 || (Date.now() / 1000) > pressedRight) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (InteriorSwitch == true) {
            mp.events.callRemote('Server:HouseSettings', 'nextinterior', 0);
            pressedRight = (Date.now() / 1000) + (1);
            return;
        }
        if (showFurniture == true && editFurniture == true) {
            if (modusFurniture == false) {
                if (modusFurniture2 == false) {
                    mp.events.callRemote('Server:FurnitureSettings', 'movingx', 0, false);
                } else {
                    mp.events.callRemote('Server:FurnitureSettings', 'movingy', 0, false);
                }
                return;
            }
        }
    }
});

//Key Pfeiltaste links
mp.keys.bind(0x25, true, function () {
    if (pressedLeft == 0 || (Date.now() / 1000) > pressedLeft) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (InteriorSwitch == true) {
            mp.events.callRemote('Server:HouseSettings', 'lastinterior', 0);
            pressedLeft = (Date.now() / 1000) + (1);
        }
        if (showFurniture == true && editFurniture == true) {
            if (modusFurniture == false) {
                if (modusFurniture2 == false) {
                    mp.events.callRemote('Server:FurnitureSettings', 'movingx2', 0, false);
                } else {
                    mp.events.callRemote('Server:FurnitureSettings', 'movingy2', 0, false);
                }
            }
        }
    }
});

//Key Pfeiltaste unten
mp.keys.bind(0x28, true, function () {
    if (pressedDown == 0 || (Date.now() / 1000) > pressedDown) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (showFurniture == true && editFurniture == true) {
            if (modusFurniture == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'movingz2', 0, false);
            }
        }
    }
});

//Num Keys
mp.keys.bind(0x61, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showCityhall == true || showSped == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[0] && animations[0] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[0], false);
            }
        }
    }
});

mp.keys.bind(0x62, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[1] && animations[1] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[1], false);
            }
        }
    }
});

mp.keys.bind(0x63, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[2] && animations[2] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[2], false);
            }
        }
    }
});

mp.keys.bind(0x64, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[3] && animations[3] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[3], false);
            }
        }
    }
});

mp.keys.bind(0x65, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[4] && animations[4] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[4], false);
            }
        }
    }
});

mp.keys.bind(0x66, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[5] && animations[5] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[5], false);
            }
        }
    }
});

mp.keys.bind(0x67, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[6] && animations[6] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[6], false);
            }
        }
    }
});

mp.keys.bind(0x68, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[7] && animations[7] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[7], false);
            }
        }
    }
});

mp.keys.bind(0x69, true, function () {
    if (pressedNum == 0 || (Date.now() / 1000) > pressedNum) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showCarSetting == true || showSped == true || showCityhall == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showHandy == true) return;
        pressedNum = (Date.now() / 1000) + (1);
        if (animations[8] && animations[8] != 'n/A') {
            if ((localPlayer.hasVariable("Player:AnimData") && localPlayer.getVariable("Player:AnimData") == 0) || !localPlayer.hasVariable("Player:AnimData")) {
                mp.events.callRemote('Server:PlayAnimation', animations[8], false);
            }
        }
    }
});

//Key Pfeiltaste oben
mp.keys.bind(0x26, true, function () {
    if (pressedUp == 0 || (Date.now() / 1000) > pressedUp) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (showFurniture == true && editFurniture == true) {
            if (modusFurniture == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'movingz', 0, false);
            }
        }
    }
});

//Alt Taste links
mp.keys.bind(0x12, true, function () {
    if (pressedAlt == 0 || (Date.now() / 1000) > pressedAlt) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        let funmodus = localPlayer.getVariable('Player:Funmodus');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (showFurniture == true && editFurniture == true) {
            modusFurniture2 = !modusFurniture2;
            mp.events.call('Client:UpdateInfoBox', 'null', 'null', 'null', 'null', 'Möbelaufbau', 'null', 'null', 'null');
        }
        if (localPlayer.vehicle && funmodus) {
            let currentvelo = localPlayer.vehicle.getVelocity();
            currentvelo.x = currentvelo.x * 2.25;
            currentvelo.y = currentvelo.y * 2.25;
            localPlayer.vehicle.setVelocity(currentvelo.x, currentvelo.y, currentvelo.z);
        }
    }
});

//Alt Taste Rechts
mp.keys.bind(0xA5, true, function () {
    if (pressedAlt == 0 || (Date.now() / 1000) > pressedAlt) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true) return;
        if (!localPlayer.isInAnyVehicle(true)) {
            if (showWheel == true || showInventory == true || showMenu == true || InteriorSwitch == true || showFurniture == true || prisonCount > 0 || showCenterMenu == true || showBank == true || showCarSetting == true || showSped == true || showCityhall == true || showFuel == true || showAmmu == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true || showMenu == true) return;
            mp.events.callRemote('Server:CrouchPlayer');
        }
        pressedAlt = (Date.now() / 1000) + (1);
    }
});

//Shift Taste
mp.keys.bind(0xA0, true, function () {
    if (pressedShift == 0 || (Date.now() / 1000) > pressedShift) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true) return;
        if (showFurniture == true && editFurniture == true) {
            modusFurniture = !modusFurniture;
            mp.events.call('Client:UpdateInfoBox', 'null', 'null', 'null', 'null', 'Möbelaufbau', 'null', 'null', 'null');
        }
        if (localPlayer.vehicle.getVariable('Vehicle:Speedlimit') == 0 && activateNitro == 0 && localPlayer.vehicle && localPlayer.vehicle.getMod(40) >= 0 && localPlayer.vehicle.getPedInSeat(-1) === localPlayer.handle) {
            if (nitroCd == 0 || (Date.now() / 1000) > nitroCd) {
                activateNitro = 1;
                mp.events.callRemote('Server:NitroSetStatus', activateNitro);
                mp.game.graphics.startScreenEffect("RaceTurbo", 0, false);
                let currentvelo = localPlayer.vehicle.getVelocity();
                currentvelo.x = currentvelo.x * 1.35;
                currentvelo.y = currentvelo.y * 1.35;
                localPlayer.vehicle.setVelocity(currentvelo.x, currentvelo.y, currentvelo.z);
                nitroCd = (Date.now() / 1000) + (30);
                nitroTimer = setTimeout(function () {
                    activateNitro = 0;
                    mp.events.callRemote('Server:NitroSetStatus', activateNitro);
                    vehiclesWithNitro = vehiclesWithNitro.filter(function (element) {
                        element != localPlayer.vehicle;
                    });
                }, 10250);
            }
        }
        pressedShift = (Date.now() / 1000) + (1);
    }
});

mp.keys.bind(0xA0, false, function () {
    if (activateNitro == 1 && localPlayer.isInAnyVehicle(true)) {
        activateNitro = 0;
        mp.events.callRemote('Server:NitroSetStatus', activateNitro);
        vehiclesWithNitro = vehiclesWithNitro.filter(function (element) {
            element != localPlayer.vehicle;
        });
        if (nitroTimer != null) {
            clearTimeout(nitroTimer);
            nitroTimer = null;
        }
        setVehicleSpeed(localPlayer.vehicle);
    }
});

//G Taste
mp.keys.bind(0x47, true, function () {
    if (pressedG == 0 || (Date.now() / 1000) > pressedG) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || arrested == true || nokeys == true || death == true || cuffed == true) return;
        if (showFurniture == true && editFurniture == true) {
            if (speedFurniture == 1) {
                speedFurniture = 2;
            } else if (speedFurniture == 2) {
                speedFurniture = 3;
            } else if (speedFurniture == 3) {
                speedFurniture = 1;
            }
            mp.events.call('Client:UpdateInfoBox', 'null', 'null', 'null', 'null', 'Möbelaufbau', 'null', 'null', 'null');
            mp.events.callRemote('Server:FurnitureSettings', 'changeSpeed', 0, false);
        }
        if (localPlayer.isInAnyVehicle(true) && localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 8 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 21) {
            var vehiclename = localPlayer.vehicle.getVariable('Vehicle:Name');
            if (vehiclename.toLowerCase() == "trash") return;
            let belt = localPlayer.getConfigFlag(32, true);
            localPlayer.setConfigFlag(32, !belt);
            let title = "Erfolgreich angeschnallt!";
            let status = "success";
            let position = "top-end";
            let timer = 2500;
            if (belt) {
                if (hudWindow != null) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('${title}','${status}','${position}','${timer}');`)
                }
            } else {
                title = "Erfolgreich abgeschnallt!";
                if (hudWindow != null) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('${title}','${status}','${position}','${timer}');`)
                }
            }
            return;
        }
        if (!localPlayer.isInAnyVehicle(true)) {
            mp.events.callRemote('Server:DrugPlantInteract', 0);
        }
    }
    pressedG = (Date.now() / 1000) + (1);
});

//ESC Taste
mp.keys.bind(0x1B, true, function () {
    mp.events.call("Client:PressedEscape");
    pressedESC = (Date.now() / 1000) + (1);
});

mp.events.add("Client:PressedEscape", () => {
    if (pressedESC == 0 || (Date.now() / 1000) > pressedESC) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || editFurniture == true) return;
        if (startLockpicking == true && lastProgress) {
            if (!lastProgress.includes('mecha') && !lastProgress.includes('cleaning') && !lastProgress.includes('milking') && !lastProgress.includes('tomato')) {
                setTimeout(function () {
                    mp.events.call('Client:FinishProgress', 'failed');
                }, 150);
            }
        }
        if (showBusPlan == true) {
            setTimeout(function () {
                showBusPlan = false;
                nokeys = false;
                mp.events.call('Client:UpdateHud3');
                hudWindow.execute(`gui.hud.showBusPlan('n/A','n/A','n/A','0');`)
            }, 150);
        }
        if (showPerso == true) {
            setTimeout(function () {
                mp.events.call('Client:UpdateHud3');
                showPerso = !showPerso;
                nokeys = !nokeys;
                hudWindow.execute(`gui.hud.showPerso('-1');`)
            }, 150);
        }
        if (showLics == true) {
            setTimeout(function () {
                mp.events.call('Client:UpdateHud3');
                showLics = !showLics;
                nokeys = !nokeys;
                hudWindow.execute(`gui.hud.showLics('-1','-1');`)
            }, 150);
        }
        if (clothMenu == true || clothMenu2 == true || clothMenu3 == true) {
            setTimeout(function () {
                mp.events.callRemote('Server:HideClothMenu', true, false);
            }, 150);
        }
        if (clothMenu4 == true) {
            setTimeout(function () {
                mp.events.callRemote('Server:HideClothMenu', true, false);
            }, 150);
        }
        if (showMdc == true && cctvShow == false) {
            setTimeout(function () {
                mp.events.call('Client:MDCSettings', 'showmdc', 'off');
            }, 150);
        }
        if (showArrest == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowArrest', 'n/A');
            }, 150);
        }
        if (showRadio == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowRadioSystem', '');
            }, 150);
        }
        if (showMusic == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowMusicStation');
            }, 150);
        }
        if (showTicket == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowTicket', 'n/A');
            }, 150);
        }
        if (showRecept == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowRecept', 'n/A');
            }, 150);
        }
        if (cctvShow == true) {
            setTimeout(function () {
                mp.events.call('Client:StartStopCCTV');
            }, 150);
        }
        if (showGov == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowGovMenu', 0, 0, 0);
            }, 150);
        }
        if (hack == true) {
            setTimeout(function () {
                mp.events.call('Client:StopHack2');
            }, 150);
        }
        if (showwardrobe == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowWardrobe', 'null');
            }, 150);
        }
        if (afk == true) {
            setTimeout(function () {
                afk = false;
                nokeys = false;
                lastclick = (Date.now() / 1000);
                localPlayer.freezePosition(false);
                if (localPlayer.vehicle) {
                    localPlayer.vehicle.freezePosition(false);
                }
                mp.events.callRemote('Server:SetAFK');
                mp.events.call('Client:ShowHud');
                showHideChat(true);
                if (hudWindow != null) {
                    hudWindow.execute(`gui.menu.setafk();`)
                }
            }, 150);
        }
        if (ping == true) {
            setTimeout(function () {
                ping = false;
                nokeys = false;
                lastclick = (Date.now() / 1000);
                localPlayer.freezePosition(false);
                if (localPlayer.vehicle) {
                    localPlayer.vehicle.freezePosition(false);
                }
                mp.events.call('Client:ShowHud');
                showHideChat(true);
                if (hudWindow != null) {
                    hudWindow.execute(`gui.menu.setping();`)
                }
            }, 150);
        }
        if (showgangzone == true) {
            setTimeout(function () {
                mp.events.call('Client:HideGangzone');
            }, 150);
        }
        if (showcrafting == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowCraft', 0);
            }, 150);
        }
        if (nokeys == true) return;
        if (showBank == true) {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            hudWindow.execute(`gui.bank.hideBankMenu();`)
            setTimeout(function () {
                mp.events.callRemote('Server:StopAnimation');
                showBank = false;
            }, 150);
        }
        if (showCityhall == true) {
            if (hudWindow != null) {
                hudWindow.execute(`gui.menu.hideStadthalle();`)
            }
            setTimeout(function () {
                showCityhall = false;
                mp.gui.cursor.show(false, false);
            }, 150);
        }
        if (showFurniture == true) {
            setTimeout(function () {
                hudWindow.execute(`gui.furniture.hideMenu();`)
            }, 150);
        }
        if (showSped == true) {
            setTimeout(function () {
                mp.events.call('Client:HideSpedition');
            }, 150);
        }
        if (showFuel == true) {
            setTimeout(function () {
                mp.events.call('Client:GetFuel', -1, 0, 0);
            }, 150);
        }
        if (showShop2 == true) {
            setTimeout(function () {
                mp.events.call('Client:ShowShop2', 'n/A', 'n/A', 'n/A', 0, 1, 1, false);
            }, 150);
        }
        if (showAmmu == true) {
            if (showAmmuPolice == false) {
                setTimeout(function () {
                    mp.events.call('Client:HideAmmu', 1);
                }, 150);
            } else {
                setTimeout(function () {
                    mp.events.call('Client:HideAmmuPolice', 1);
                }, 150);
            }
        }
        if (showShop == true) {
            setTimeout(function () {
                let retShop = 'n/A';
                globalBool = false;
                if (lastShop != '24/7 Laden' && lastShop != 'Snackpoint' && lastShop != 'Apotheke' && lastShop != 'Bar' && lastShop != 'Waffendealer' && lastShop != 'Big. D' && lastShop != 'n/A') {
                    globalBool = true;
                    retShop = lastShop;
                }
                if (globalBool == true) {
                    mp.events.call('Client:ShowShop', null, 0, 'n/A', 0);
                } else {
                    mp.events.call('Client:ShowShop', null, 0, 'n/A', 1);
                }
                if (globalBool == true) {
                    setTimeout(function () {
                        mp.events.callRemote('Server:ShowPreShop', retShop, 0, 1, 0);
                    }, 15);
                }
                globalBool = false;
            }, 115);
        }
        if (showTuning == true && hudWindow != null) {
            setTimeout(function () {
                hudWindow.execute(`gui.hud.resetTuning('0');`)
                hudWindow.execute(`gui.hud.syncTuningFunc('0');`)
                mp.events.call('Client:HideTuningMenu');
            }, 225);
        }
        if (showHandy == true) {
            setTimeout(function () {
                showHandy = false;
                mp.events.call('Client:UpdateHud3');
                hudWindow.execute(`gui.smartphone.hideSmartphone();`)
                mp.events.callRemote('Server:HideSmartphone');
                mp.gui.cursor.show(false, false);
                showHideChat(true);
            }, 150);
        }
        if (showTab == true) {
            setTimeout(function () {
                mp.events.call('Client:UpdateHud3');
                hudWindow.execute(`gui.menu.showTabList('n/A');`)
                mp.game.ui.displayHud(true);
                showHideChat(true);
                enableDisableRadar(true);
                showTab = false;
                mp.gui.cursor.show(false, false);
            }, 150);
        }
        if (showCenterMenu == true) {
            setTimeout(function () {
                showCenterMenu = false;
                hudWindow.execute(`gui.menu.showCenterMenu('n/A','n/A','n/A');`)
                mp.events.call('Client:UpdateHud3');
                mp.game.ui.displayHud(true);
                showHideChat(true);
                enableDisableRadar(true);
                mp.gui.cursor.show(false, false);
                if (centerHeader == "Führungszeugnis" || centerHeader == "Fahrzeugdiagnose" || centerHeader == "Untersuchung") {
                    mp.events.callRemote('Server:StopAnimation');
                }
            }, 150);
        }
        if (showDealer == true) {
            setTimeout(function () {
                mp.events.call('Client:DealerShipSettings', 'abort', 'n/A');
            }, 150);
        }
        if (showCarSetting == true) {
            setTimeout(function () {
                mp.events.call('Client:VehicleSettings', 'hide', '0');
                mp.events.call('Client:ShowStadthalle');
            }, 150);
        }
        if (showMecha == true) {
            showMecha = false;
        }
        if (barberMenu == true) {
            setTimeout(function () {
                hudWindow.execute(`gui.hud.abortHair();`)
            }, 150);
        }
        if (tattooShop == true) {
            setTimeout(function () {
                hudWindow.execute(`gui.hud.abortTattoo();`)
            }, 150);
        }
        if (showWheel == true) {
            setTimeout(function () {
                hudWindow.execute(`gui.selectwheel.hideWheel();`)
                wheelSelected = 0;
                showWheel = false;
                mp.gui.cursor.show(false, false);
            }, 150);
        }
    }
});

//Key Backspace
mp.keys.bind(0x08, true, function () {
    if (pressedBS == 0 || (Date.now() / 1000) > pressedBS) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || showHandy == true) return;
        if (InteriorSwitch == true) {
            mp.events.callRemote('Server:HouseSettings', 'abortinterior', 0);
        }
        if (showFurniture == true && editFurniture == true) {
            editFurniture = false;
            modusFurniture = false;
            modusFurniture2 = false;
            showFurniture = false;
            mp.events.callRemote('Server:FurnitureSettings', 'abortmoebel', 0, false);
            mp.events.call('Client:ShowInfobox', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null');
        }
        if (localPlayer.hasVariable("Player:AnimData")) {
            value = localPlayer.getVariable("Player:AnimData");
            if (value != "0") {
                mp.events.callRemote('Server:StopCommandAnimation');
            }
        }
        pressedBS = (Date.now() / 1000) + (2);
    }
});

//Key Space
mp.keys.bind(0x20, true, function () {
    if (pressedSpace == 0 || (Date.now() / 1000) > pressedSpace) {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || cuffed == true || showHandy == true) return;
        //Drone
        if (localPlayer.vehicle != null && localPlayer.vehicle.getVariable('Vehicle:Name') == 'rcmavic') {
            mp.game.audio.playSoundFrontend(
                -1,
                "SELECT",
                "HUD_FRONTEND_DEFAULT_SOUNDSET",
                false
            );
            if (visionState == 0) {
                visionState = 1;
                mp.game.graphics.setSeethrough(false);
                mp.game.graphics.setNightvision(true);
                return;
            } else if (visionState == 1) {
                visionState = 2;
                mp.game.graphics.setSeethrough(true);
                mp.game.graphics.setNightvision(false);
                return;
            } else if (visionState == 2) {
                visionState = 0;
                mp.game.graphics.setSeethrough(false);
                mp.game.graphics.setNightvision(false);
                return;
            }
        }
        //Rappeling
        const maxSpeed = 10.0;
        const minHeight = 15.0;
        const maxHeight = 45.0;
        const maxAngle = 15.0;
        if (localPlayer.vehicle != null && (localPlayer.vehicle.getVariable('Vehicle:Name') == 'maverick' || localPlayer.vehicle.getVariable('Vehicle:Name') == 'polmav' || localPlayer.vehicle.getVariable('Vehicle:Name') == 'annihilator')) {
            if (localPlayer.vehicle.getPedInSeat(-1) === localPlayer.handle || localPlayer.vehicle.getPedInSeat(0) === localPlayer.handle) {
                return;
            } else {
                if (!mp.game.invoke("0x4E417C547182C84D", localPlayer.vehicle.handle)) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du kannst dich von diesem Helikopter nicht abseilen!','error','top-left',2350);`);
                    return;
                }
                if (localPlayer.vehicle.getSpeed() > maxSpeed) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Der Helikopter ist zu schnell!','error','top-left',2350);`);
                    return;
                }
                const taskStatus = localPlayer.getScriptTaskStatus(-275944640);
                if (taskStatus === 0 || taskStatus === 1) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du seilst dich gerade schon ab!','error','top-left',2350);`);
                    return;
                }
                const curHeight = localPlayer.vehicle.getHeightAboveGround();
                if (curHeight < minHeight || curHeight > maxHeight) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Der Helikopter ist zu hoch/tief!','error','top-left',2350);`);
                    return;
                }
                if (!localPlayer.vehicle.isUpright(maxAngle) || localPlayer.vehicle.isUpsidedown()) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Der Helikopter muss stabil sein, damit du dich abseilen kannst!','error','top-left',2350);`);
                    return;
                }
                localPlayer.clearTasks();
                localPlayer.taskRappelFromHeli(10.0);
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Abseilvorgang gestartet ...','success','top-left',2350);`);
            }
        }
        pressedSpace = (Date.now() / 1000) + (2);
    }
});

//SpeakAnim
mp.events.add("Client:SpeakAnim", () => {
    mp.players.local.playFacialAnim("mic_chatter", "mp_facial");
    if (speakTimeout != null) {
        clearTimeout(speakTimeout);
        speakTimeout = null;
    }
    speakTimeout = setTimeout(function () {
        mp.players.local.playFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
        speakTimeout = null;
    }, 2500);
});

//ShowTab
mp.events.add("Client:StartTabMenu", (json) => {
    if (hudWindow != null) {
        mp.events.call('Client:UpdateHud3');
        showTab = !showTab;
        tabJson = json;
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        mp.gui.cursor.show(true, true);
        hudWindow.execute(`gui.menu.showTabList('${json}');`)
    }
});

//Reloadhud
mp.events.add("Client:ReloadHud", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.reloadHud();`)
        hudWindow.execute(`gui.speedometer.reloadHud();`)
    }
})

//Hidehud
mp.events.add("Client:HideHud", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.hideHud();`)
        hudWindow.execute(`gui.speedometer.hideHud();`)
    }
})

mp.events.add("Client:ResetTabCD", () => {
    pressedTab = (Date.now() / 1000) - (1);
})

//Showhud
mp.events.add("Client:ShowHud", (id) => {
    if (hudWindow != null) {
        //HUD 1
        let health = localPlayer.getVariable('Player:Health');
        let armor = localPlayer.getVariable('Player:Armor');
        let needs = localPlayer.getVariable('Player:Needs').split(',');
        hudWindow.execute(`gui.hud.showHud('${health-100}','${needs[1]}','${needs[0]}','${armor}');`)
        //HUD 2
        if (id) {
            playerID = id;
        }
        let zone = mp.game.gxt.get(mp.game.zone.getNameOfZone(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z));
        hudWindow.execute(`gui.speedometer.showHud2('${playerID}','${zone}');`)
    }
});

mp.events.add("Client:UpdateHud3", () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.speedometer.updateHud3();`)
    }
});

mp.events.addDataHandler("Player:Health", (entity, value, oldValue) => {
    antiCheatWait = (Date.now() / 1000) + (3);
    oldHealth = value;
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateBar(1,'${(getPlayerHealth-100)}');`)
    }
});

mp.events.addDataHandler("Player:Armor", (entity, value, oldValue) => {
    oldArmor = parseInt(value);
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateBar(2, '${value}');`)
    }
    antiCheatWait = (Date.now() / 1000) + (2);
});

mp.events.addDataHandler("Player:Needs", (entity, value, oldValue) => {
    let needs = value.split(',');
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateBar(3, '${needs[0]}');`)
        hudWindow.execute(`gui.hud.updateBar(4, '${needs[1]}');`)
    }
});

mp.events.addDataHandler("Player:Money", (entity, value, oldValue) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.updateMoney('${value}');`)
    }
});

mp.events.addDataHandler("Player:Funmodus", (entity, value, oldValue) => {
    if (localPlayer.vehicle) {
        setVehicleSpeed(localPlayer.vehicle);
    }
});

mp.events.addDataHandler("Player:FollowStatus", (entity, value, oldValue) => {
    let entity2 = null;
    if (mp.players.exists(entity) && 0 !== entity.handle) {
        mp.players.forEach(p => {
            if (p.remoteId === entity.getVariable('Player:Follow')) {
                entity2 = p;
            }
        });
        setTimeout(function () {
            if (entity2 && mp.players.exists(entity2)) {
                if (value == 1) {
                    entity2.attachTo(entity.handle,
                        11816,
                        0.35, 0.35, 0.08, 0, 0, 0,
                        false, false, false, false, 2, true);
                } else if (value == 2) {
                    entity2.attachTo(entity.handle,
                        0,
                        0.27, 0.15, 0.63, 0.5, 0.5, 180,
                        false, false, false, false, 2, false);
                } else {
                    entity2.detach(false, false);
                }
            }
        }, 115);
    }
});

//Inventory
mp.events.add("Client:ShowInventory", (json, maxweight, toggle, json2, weight2, text2) => {
    if (hudWindow != null) {
        if (toggle == true) {
            showHideChat(false);
            enableDisableRadar(false);
            mp.gui.cursor.show(true, true);
        } else {
            showHideChat(true);
            enableDisableRadar(true);
            mp.gui.cursor.show(false, false);
        }
        if (showInventory == false) {
            inventory = JSON.parse(json);
            maxWeapons = 0;
            for (i = 0; i < inventory.length; i++) {
                if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("schutzweste") && inventory[i].props.split(',')[1] == 1) {
                    inventory[i].misc = parseInt(inventory[i].props.split(',')[0]);
                    maxWeapons++;
                }
            }
        }
        showInventory = !showInventory;
        hudWindow.execute(`gui.inventory.showInventory('${json}','${maxweight}','${toggle}','${json2}','${weight2}','${text2}');`)
    }
});

mp.events.add("Client:UseInventory", (use, itemid, amount, selection) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:UseInventory', use, itemid, amount, selection);
    }
});

mp.events.add("Client:UpdateInventory", (json, json2) => {
    if (hudWindow != null) {
        wait = true;
        inventory = JSON.parse(json);
        maxWeapons = 0;
        for (i = 0; i < inventory.length; i++) {
            if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("schutzweste") && inventory[i].props.split(',')[1] == 1) {
                inventory[i].misc = parseInt(inventory[i].props.split(',')[0]);
                maxWeapons++;
            }
        }
        hudWindow.execute(`gui.inventory.updateInventory('${json}','${json2}');`)
        wait = false;
    }
});

//UpdatePosition
mp.events.add("Client:UpdatePosition", (x, y, z) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    wait2 = 1;
    oldposition = new mp.Vector3(x, y, z);
    if (updateTimeout != null) {
        clearTimeout(updateTimeout);
    }
    updateTimeout = setTimeout(function () {
        wait2 = 0;
        updateTimeout = null;
    }, 1650);
});

//SelectWheel
mp.events.add("Client:UseWheel", (use, amount) => {
    if (hudWindow != null) {
        if (use != 'open') {
            if (use == 'setcloth') {
                mp.events.callRemote('Server:SetCloth', amount);
            } else if (use == 'faction') {
                mp.events.callRemote('Server:FactionSettings', 'wheel', '' + amount);
            } else {
                mp.events.callRemote('Server:SelectWheel', use, amount);
            }
        } else {
            if (localPlayer.vehicle) {
                mp.events.call('Client:HideWheel2');
                mp.events.callRemote('Server:SelectWheel', use, amount);
            }
        }
    }
});

mp.events.add("Client:Interact", () => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:Interact');
    }
});

mp.events.add("Client:HideWheel2", () => {
    if (hudWindow != null) {
        wheelSelected = 0;
        mp.gui.cursor.show(false, false);
        showWheel = false;
        localPlayer.freezePosition(false);
    }
});

//Cuff
mp.events.add("Client:SetCuff", (status) => {
    cuffed = status;
    localPlayer.setEnableHandcuffs(status);
});

//Arrested
mp.events.add("Client:SetArrested", (status) => {
    arrested = status;
});

//Second timer
setInterval(() => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || showSaltyError == true || triggerAntiCheat == true) return;
    //Kilometre
    if (localPlayer.vehicle && vehiclePos) {
        vehicleKilometre += parseFloat(distanceVector(vehiclePos, localPlayer.vehicle.position) / 1500);
        vehiclePos = localPlayer.vehicle.position;
    }
    //HUD 2
    let zone = mp.game.gxt.get(mp.game.zone.getNameOfZone(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z));
    if (!zone) {
        zone = 'n/A';
    }
    if (hudWindow != null) {
        hudWindow.execute(`gui.speedometer.updateHud2('${zone}');`)
    }
    //Deathanim
    if (death == true && !localPlayer.vehicle) {
        if (!localPlayer.isPlayingAnim('dead', 'dead_a', 3) && !localPlayer.isPlayingAnim('dead', 'dead_f', 3)) {
            mp.events.callRemote('Server:PlayDeathAnim', true);
        }
    }
    //Handyanim
    if (showHandy && !localPlayer.vehicle) {
        if (!localPlayer.isPlayingAnim('cellphone@', 'cellphone_call_listen_base', 3) && !localPlayer.isPlayingAnim('cellphone@', 'cellphone_text_in', 3)) {
            mp.events.callRemote('Server:PlayPhoneAnim', true);
        }
    }
    //Livestream
    if (livestream == 1 && !localPlayer.vehicle) {
        if (!localPlayer.isPlayingAnim('missfinale_c2mcs_1', 'fin_c2_mcs_1_camman', 3)) {
            mp.events.callRemote('Server:OnPlayerPressF7', false);
        }
    }
    //Crosshair
    if (localPlayer.getVariable('Player:Spawned')) {
        if (localPlayer.weapon != mp.game.joaat('weapon_unarmed') && localPlayer.weapon != mp.game.joaat('weapon_knuckle') && localPlayer.weapon != mp.game.joaat('weapon_sniperrifle') && localPlayer.weapon != mp.game.joaat('weapon_heavysniper') && localPlayer.weapon != mp.game.joaat('weaponweapon_heavysniper_mk2') &&
            localPlayer.weapon != mp.game.joaat('weapon_marksmanrifle') && localPlayer.weapon != mp.game.joaat('weapon_marksmanrifle_mk2') && pointing.active == false && handsUp == false && showAmmu == false && showShop == false && showShop2 == false && showDealer == false && showTab == false && showSped == false && showCityhall == false && showCarSetting == false && showCenterMenu == false && barberMenu == false && tattooShop == false && startLockpicking == false && showWheel == false && !localPlayer.isInAnyVehicle(true)) {
            let chair = crosshair;
            if (chair != oldCrosshair && showCrosshair == true) {
                showCrosshair = false;
            }

            if (showCrosshair == false) {
                showCrosshair = true;
                if (hudWindow != null) {
                    hudWindow.execute(`gui.speedometer.showCrosshair('${chair}');`)
                    oldCrosshair = chair;
                }
            }
        } else {
            if (showCrosshair == true) {
                showCrosshair = false;
                if (hudWindow != null) {
                    hudWindow.execute(`gui.speedometer.hideCrosshair();`)
                }
            }
        }
    }
    //Anticheat
    antiCheatCheck();
    //Ped flee
    let distance = distanceVector(localPlayer.position, new mp.Vector3(1714.7175, -571.90814, 144.50644));
    if (distance <= 105) {
        mp.players.forEachInStreamRange(p => {
            ped = getClosestPed(p.position);
            if (ped.ped && ped.ped.hasVariable("Ped:Death")) {
                ped.ped.taskSmartFlee(p.handle, 37.5, -1, true, true);
            }
        });
    }
    //Drohne
    if (localPlayer.vehicle != null && localPlayer.vehicle.getVariable('Vehicle:Name') == 'rcmavic' && localPlayer.vehicle.getHealth() < 250) {
        mp.game.graphics.setSeethrough(false);
        mp.game.graphics.setNightvision(false);
        visionState = 0;
        mp.events.callRemote('Server:FactionSettings', 'destroydrone', 'n/A');
    }
    //Timemarker
    if (marker2TO > 0) {
        marker2TO--;
        if (marker2TO == 0) {
            timeMarker = 0;
            if (marker2 != null) {
                marker2.destroy();
                marker2 = null;
            }
        }
    }
    //Kokain
    if (sprintTimer != null) {
        mp.game.player.restoreStamina(100);
    }
    //AFK Check
    if (lastclick > 0 && (Date.now() / 1000) > (lastclick + (60 * 25)) && !death && spawned && afk == false && ping == false && editFurniture == false && showSaltyError == false) {
        mp.events.call('Client:SetAFK');
    }
    //Five Seconds
    secondTimer++;
    if (secondTimer >= 5) {
        if (localPlayer.weapon && mp.game.player.isFreeAiming()) {
            let entity = pointingAt(1000, true);
            if (entity && 0 !== entity.entity.handle && entity.entity.type == 'ped') {
                distance = distanceVector(entity.position, localPlayer.position);
                if (distance <= 4.35) {
                    mp.events.callRemote('Server:CheckRob');
                }
            }
        }
        secondTimer = 0;
    }
}, 1087);

//Half second timer
setInterval(() => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || showSaltyError == true || triggerAntiCheat == true) return;
    //Weapons
    if (inventory) {
        updateWeapons();
        timerCounter++;
        if (timerCounter >= 3) {
            checkForZeroWeapons();
            timerCounter = 0;
        }
    }
    //Update health/armor
    updateHealthArmor();
    //Damage effect
    if (death == false) {
        if ((getPlayerHealth(localPlayer)-100) <= 15 && (getPlayerHealth(localPlayer)-100) > 1) {
            if (damageEffect == false) {
                damageEffect = true;
                mp.game.graphics.startScreenEffect("DeathFailMPDark", -1, false);
            }
        } else {
            if (damageEffect == true) {
                mp.game.graphics.stopScreenEffect("DeathFailMPDark");
            }
        }
    }
    //Speedometer + Kilometre
    if (localPlayer.vehicle) {
        updateSpeedometer();
    }
    //Furniture
    if (showFurniture == true && editFurniture == true && modusFurniture == true) {
        if (mp.keys.isDown(37) === true) {
            if (modusFurniture2 == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingx2', 0, false);
            } else {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingy2', 0, false);
            }
        } else if (mp.keys.isDown(39) === true) {
            if (modusFurniture2 == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingx', 0, false);
            } else {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingy', 0, false);
            }
        } else if (mp.keys.isDown(40) === true) {
            if (modusFurniture2 == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingz', 0, false);
            }
        } else if (mp.keys.isDown(38) === true) {
            if (modusFurniture2 == false) {
                mp.events.callRemote('Server:FurnitureSettings', 'rotatingz2', 0, false);
            }
        }
    }
}, 521);

//Playerquit
mp.events.add("playerQuit", playerQuitHandler);

function playerQuitHandler(player, exitType, reason) {
    if (player == localPlayer) {
        //Timecycle
        if (death == true) {
            mp.game.invoke("0x0F07E7745A236711");
            mp.game.invoke("0x31B73D1EA9F01DA2");
        }

        //Damage effect
        mp.game.graphics.stopScreenEffect("DeathFailMPDark");

        //MDC Blip
        if (policeBlip) {
            policeBlip.destroy();
            policeBlip = null;
        }

        //Waypoint
        if (waypointHandle != null) {
            waypointHandle.destroy();
            waypointHandle = null;
        }

        //Prison
        if (prisonCount > 0) {
            mp.events.callRemote('Server:UpdatePrison', prisonCount);
            prisonTime = 0;
            prisonCount = 0;
            prisonCount2 = 0;
            if (prisonHandle != null) {
                prisonHandle.destroy();
                prisonHandle = null;
            }
        }

        //Livemap
        if (livemap == true) {
            if (livemapinterval != null) {
                clearInterval(livemapinterval);
            }
            livemap = false;
            for (let i = 0; i < liveposisold.length; i++) {
                if (liveposisold[i]) {
                    liveposisold[i].destroy();
                    liveposisold[i] = null;
                }
            }
        }

        //Farmer
        if (farmerStatus > 0) {
            farmerCount = 0;
            farmerStatus = 0;
            farmerTime = 0;

            if (farmerHandle != null) {
                farmerHandle.destroy();
                farmerHandle = null;
            }

            if (farmerHandle2 != null) {
                farmerHandle2.destroy();
                farmerHandle2 = null;
            }
            if (farmerHandles) {
                for (let i = 0; i < farmerHandles.length; i++) {
                    if (farmerHandles[i]) {
                        farmerHandles[i].destroy();
                        farmerHandles[i] = null;
                    }
                }
            }
        }

        //Cleaning
        if (cleanerHandle != null) {
            cleanerHandle.destroy();
            cleanerHandle = null;
        }
        if (cleanerHandle2 != null) {
            cleanerHandle2.destroy();
            cleanerHandle2 = null;
        }
        if (cleanerHandle3 != null) {
            cleanerHandle3.destroy();
            cleanerHandle3 = null;
        }
        if (cleanerHandle4 != null) {
            cleanerHandle4.destroy();
            cleanerHandle4 = null;
        }
        cleanerCount = 0;
        cleaningTask = false;
        cleanerTime = 0;

        //Garbage
        if (garbageHandle != null) {
            garbageHandle.destroy();
            garbageHandle = null;
        }

        if (garbageHandle2 && garbageHandle2 != null) {
            garbageHandle2.destroy();
            garbageHandle2 = null;
        }

        //Busdriver
        if (busHandle != null) {
            busHandle.destroy();
            busHandle = null;
        }

        //Fuelsystem
        if (showFuel == true) {
            mp.events.call('Client:GetFuel', -1, 0, 0);
        }

        //Ammunation
        if (startRange == true) {
            if (rangeHandle != null) {
                rangeHandle.destroy()
                rangeHandle = null;
            }
            rangePoints = 0;
            rangeStatus = 0;
            getPoint = 0;
            startRange = false;
        }

        //Hide hud
        mp.events.call('Client:HideHud');

        //Handsup
        handsUp = false;

        //Spectate
        mp.events.call('Client:StopSpectate', 1);

        //Destroy Hudwindow
        if (hudWindow != null) {
            hudWindow.destroy();
            hudWindow = null;
        }

        //Gangzones
        mp.events.call('Client:RemoveGangzones');

        //Radio
        animationSet = 0;

        //Death
        death = false;

        //AFK
        afk = false;

        //Ping
        ping = false;
    }
}

//Playerleavevehicle
mp.events.add("playerLeaveVehicle", (vehicle, seat) => {
    if (vehicle) {
        if (drivingSchoolHandle != null && (vehicle.getVariable('Vehicle:Name').toLowerCase() == 'sentinel' || vehicle.getVariable('Vehicle:Name').toLowerCase() == 'akuma' || vehicle.getVariable('Vehicle:Name').toLowerCase() == 'pounder' || vehicle.getVariable('Vehicle:Name').toLowerCase() == 'dinghy' || vehicle.getVariable('Vehicle:Name').toLowerCase() == 'havok')) {
            hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Was machst du? Komm zurück ins Fahrzeug!','info','top-left',4350);`);
        }
        //Nitro
        nitroCd = 0;
        if (activateNitro == 1) {
            activateNitro = 0;
            mp.events.callRemote('Server:NitroSetStatus', activateNitro);
            vehiclesWithNitro = vehiclesWithNitro.filter(function (element) {
                element != vehicle;
            });
            if (nitroTimer != null) {
                clearTimeout(nitroTimer);
                nitroTimer = null;
            }
            setVehicleSpeed(vehicle);
        }
        //Kilometre
        mp.events.callRemote('Server:UpdateKilometre', parseFloat(vehicleKilometre), vehicle.remoteId);
        vehiclePos = null;
        vehicleKilometre = 0;
        //Lockpicking
        if (startLockpicking == true && lastProgress) {
            if (!lastProgress.lastProgresscontains('mecha') && !lastProgress.lastProgresscontains('cleaning') && !lastProgress.lastProgresscontains('milking') && !lastProgress.lastProgresscontains('tomato')) {
                mp.events.call('Client:FinishProgress', 'failed');
            }
        }
        //Smoking
        if (setCiga == true) {
            mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleCiga', false);
            setTimeout(function () {
                mp.events.callRemote('Server:AddRemoveAttachments', 'handCiga', true);
            }, 255);
        }
        //Joint
        if (setJoint == true) {
            mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleJoint', false);
            setTimeout(function () {
                mp.events.callRemote('Server:AddRemoveAttachments', 'handJoint', true);
            }, 255);
        }
        //Drone
        if (visionState > 0) {
            mp.game.graphics.setSeethrough(false);
            mp.game.graphics.setNightvision(false);
            visionState = 0;
        }
        //PDCam
        if (pdCam == true) {
            pdCam = false;
        }
    }
})

//playerEnterVehicle
mp.events.add("playerEnterVehicle", (vehicle, seat) => {

    localPlayer.weapon = mp.game.joaat('weapon_unarmed');

    setVehicleSpeed(vehicle);

    vehiclePos = vehicle.position;
    vehicleKilometre = parseFloat(vehicle.getVariable('Vehicle:Kilometre'));
    if (!vehicleKilometre) {
        vehicleKilometre = 0;
    }

    //Anti Trolling Cheat
    if (vehicleTime == 0 || (vehicleTime != 0 && Date.now() / 1000 > vehicleTime)) {
        vehicleTime = Date.now() / 1000 + (2);
        vehicleEntrys = 0;
    }
    vehicleEntrys++;
    if (vehicleEntrys >= 4) {
        callAntiCheat("Trolling Cheat", vehicleEntrys + "/Fahrzeuge", false);
    }

    //Speedlimit
    if (vehicle.hasVariable('Vehicle:Speedlimit') && vehicle.getVariable('Vehicle:Speedlimit') > 0) {
        vehicle.setMaxSpeed(vehicle.getVariable('Vehicle:Speedlimit') / 3.6);
    }

    //Smoking
    if (setCiga == true) {
        mp.events.callRemote('Server:AddRemoveAttachments', 'handCiga', false);
        setTimeout(function () {
            mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleCiga', true);
        }, 255);
    }

    //Joint
    if (setJoint == true) {
        mp.events.callRemote('Server:AddRemoveAttachments', 'handJoint', false);
        setTimeout(function () {
            mp.events.callRemote('Server:AddRemoveAttachments', 'vehicleJoint', true);
        }, 255);
    }

    //Drone
    if (vehicle.getVariable('Vehicle:Name') == 'rcmavic') {
        mp.game.graphics.setSeethrough(false);
        mp.game.graphics.setNightvision(false);
        visionState = 0;
    }
})

//Playerentercolshape
mp.events.add('playerEnterColshape', (shape) => {
    if (drivingSchoolHandle2 != null && localPlayer.vehicle && drivingSchoolHandle2 == shape) {
        EnterDrivingSchoolCP();
    }
    if (farmerStatus >= 2 && farmerHandle != null && farmerHandle == shape && (localPlayer.vehicle || farmerStatus == 4)) {
        var vehiclename = 'n/A';
        if (localPlayer.vehicle) {
            vehiclename = localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase();
        }
        if (vehiclename == 'tractor2' || farmerStatus == 4) {
            farmerCount++;
            if (farmerHandle != null) {
                farmerHandle.destroy();
                farmerHandle = null;
            }
            if (farmerStatus == 2) {
                if (farmerCount < wheatPosition.length - 1) {
                    farmerHandle = mp.colshapes.newSphere(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.2, 2);
                    farmerHandles[farmerCount] = mp.markers.new(1, new mp.Vector3(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.8), 1.75, {
                        color: [255, 0, 0, 255],
                        visible: true,
                        dimension: 0
                    });
                } else {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Das Weizen muss jetzt geerntet werden, vom Anfang!','success','top-left',4350);`);
                    mp.game.ui.setNewWaypoint(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y);
                    farmerCount = 0;
                    farmerStatus = 3;
                    farmerHandle = mp.colshapes.newSphere(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.2, 2);
                    for (let i = 0; i < farmerHandles.length; i++) {
                        if (farmerHandles[i]) {
                            farmerHandles[i].destroy();
                        }
                        farmerHandles[i] = createObject("prop_veg_crop_04", new mp.Vector3(wheatPosition[i].x, wheatPosition[i].y, wheatPosition[i].z - 0.45), new mp.Vector3(0.0, 0.0, 0.0), 0);
                    }
                }
            } else if (farmerStatus == 3) {
                if (farmerCount < wheatPosition.length - 1) {
                    if (farmerHandles[farmerCount - 1] != null) {
                        farmerHandles[farmerCount - 1].destroy();
                        farmerHandles[farmerCount - 1] = null;
                    }
                    farmerHandle = mp.colshapes.newSphere(wheatPosition[farmerCount].x, wheatPosition[farmerCount].y, wheatPosition[farmerCount].z - 0.2, 2);
                } else {
                    if (Date.now() / 1000 < farmerTime && farmerCount > 3) {
                        mp.events.callRemote('Server:CleanerFinish', 0);
                        setTimeout(function () {
                            callAntiCheat("Teleport to Checkpoint Cheat", "Landwirt", false);
                        }, 1250);
                        return;
                    } else {
                        mp.events.callRemote('Server:FarmerFinish', 2);
                    }
                }
            } else {
                if (localPlayer.vehicle) return;
                if (farmerHandle2 != null) {
                    farmerHandle2.destroy();
                    farmerHandle2 = null;
                }
                mp.events.call('Client:StartLockpicking', 4, 'tomato', 'Tomaten werden geerntet ...');
            }
        }
    }
})

//Playerentercheckpoint
mp.events.add('playerEnterCheckpoint', (checkpoint) => {
    if (drivingSchoolHandle != null && localPlayer.vehicle && drivingSchoolHandle == checkpoint) {
        EnterDrivingSchoolCP();
    }
    if (garbageHandle != null && garbageHandle == checkpoint) {
        if (localPlayer.vehicle) return;
        garbageHandle.destroy();
        garbageHandle = null;
        if (garbageHandle2 != null) {
            garbageHandle2.destroy();
            garbageHandle2 = null;
        }
        mp.events.callRemote('Server:AddRemoveAttachments', 'garbageBag', true);
        mp.events.callRemote('Server:NextGarbageStation');
    }
    if (busHandle != null && busHandle == checkpoint && localPlayer.vehicle) {
        var vehiclename = localPlayer.vehicle.getVariable('Vehicle:Name');
        if (vehiclename == 'bus' || vehiclename == 'coach' || vehiclename == 'rentalbus' || vehiclename == 'tourbus') {
            busHandle.destroy();
            busHandle = null;
            mp.events.callRemote('Server:IsAtNextBusStation');
        }
    }
    if (farmerStatus == 1337 && farmerHandle == checkpoint && !localPlayer.vehicle) {
        if (farmerHandle != null) {
            farmerHandle.destroy();
            farmerHandle = null;
        }
        farmerCount++;

        mp.events.callRemote('Server:PlayShortAnimation', 'rcmextreme3', 'idle', 2400);

        setTimeout(function () {
            if (farmerHandle2 != null) {
                farmerHandle2.destroy();
                farmerHandle2 = null;
            }
            if (Date.now() / 1000 < farmerTime && farmerCount > 3) {
                mp.events.callRemote('Server:CleanerFinish', 0);
                setTimeout(function () {
                    callAntiCheat("Teleport to Checkpoint Cheat", "Müll sammeln am Strand", false);
                }, 1250);
                return;
            } else {
                let randValue = getRandomInt(beachGarbagePosition.length);

                if (farmerCount < 10) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Müll aufgesammelt!','success','top-left',4250);`);
                } else {
                    mp.events.call('Client:PlaySoundPeep2');

                    mp.events.callRemote('Server:BeachGarbageFinish');

                    farmerTime = Date.now() / 1000 + (55);

                    farmerCount = 0;
                }
                mp.game.ui.setNewWaypoint(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y);

                farmerHandle = mp.checkpoints.new(49, new mp.Vector3(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y, beachGarbagePosition[randValue].z - 0.5), 1.5, {
                    color: [255, 0, 0, 255],
                    visible: true,
                    dimension: 0
                });

                farmerHandle2 = createObject("prop_rub_litter_03c", new mp.Vector3(beachGarbagePosition[randValue].x, beachGarbagePosition[randValue].y, beachGarbagePosition[randValue].z - 0.57), new mp.Vector3(0.0, 0.0, 0.0), 0);
            }
        }, 2500);
    }
    if (cleanerHandle4 != null && cleanerHandle4 == checkpoint && !localPlayer.vehicle) {
        cleanerHandle4.destroy();
        cleanerHandle4 = null;
        mp.events.callRemote('Server:StartCleaningDrone');
    }
    if (cleanerHandle2 != null && cleanerHandle2 == checkpoint && localPlayer.vehicle) {
        localPlayer.vehicle.setMaxSpeed(30 / 3.6);
        cleanerHandle2.destroy();
        cleanerHandle2 = null;
    }
    if (prisonCount > 0 && prisonHandle != null && prisonHandle == checkpoint) {
        prisonCount--;
        mp.events.callRemote('Server:UpdatePrison', prisonCount);
        prisonHandle.destroy();
        prisonHandle = null;
        prisonCount2++;
        if (prisonCount2 == 10 && prisonCount > 0) {
            if (Date.now() / 1000 < prisonTime) {
                mp.events.callRemote('Server:UpdatePrison', prisonCount);
                prisonTime = 0;
                callAntiCheat("Teleport to Checkpoint Cheat", "Verbleibende CPs: " + prisonCount, false);
                prisonCount = 0;
                prisonCount2 = 0;
                return;
            } else {
                prisonCount2 = 0;
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du musst noch ${prisonCount} Checkpoints ablaufen!','info','top-left',3500);`)
            }
        }
        if (prisonCount > 0) {
            let randValue = getRandomInt(21);
            mp.game.ui.setNewWaypoint(prisonPosition[randValue].x, prisonPosition[randValue].y);
            prisonHandle = mp.checkpoints.new(49, prisonPosition[randValue], 4.5, {
                color: [255, 0, 0, 255],
                visible: true,
                dimension: localPlayer.dimension
            });
        } else {
            prisonTime = 0;
            prisonCount = 0;
            prisonCount2 = 0;
            mp.events.callRemote('Server:EndPrison', 0);
        }
    }
})

//LastDamage
mp.events.add('Client:LastDamage', () => {
    mp.events.callRemote('Server:LastDamage', lastDamage);
});

//GetWeaponDamage
mp.events.add('Client:GetWeaponDamage', () => {
    mp.events.callRemote('Server:GetWeaponDamage', weaponDamage);
});

//IncomingDamage
mp.events.add('incomingDamage', (sourceEntity, sourcePlayer, targetEntity, weapon, boneIndex, damage) => {
    if (damage > 0) {
        mp.events.callRemote('Server:SyncHealth');
        if (death == true) {
            return true;
        }
        if (weapon != mp.game.joaat('weapon_unarmed')) {
            weaponDamage++;
        }
        if (sourceEntity && sourceEntity.remoteId != localPlayer.remoteId) {
            lastDamage = sourceEntity.remoteId;
        } else {
            lastDamage = -1
        }
        if (startLockpicking == true && lastProgress) {
            if (!lastProgress.includes('mecha') && !lastProgress.includes('cleaning') && !lastProgress.includes('milking') && !lastProgress.includes('tomato')) {
                mp.events.call('Client:FinishProgress', 'failed');
            }
        }
        if (hack == true) {
            mp.events.call('Client:StopHack2');
        }
        if (getPlayerHealth(localPlayer) - damage < 100 && death == false) {
            death = true;
            hideMenus();
            mp.events.callRemote('Server:SetDeath', null, 7);
            return true;
        }
        if (death == false) {
            if (crystalmeth == true) {
                if (Math.floor(Math.random() * 11) == 5) {
                    return true;
                }
            }
        }
    }
});

//OutgamingDamage
mp.events.add('outgoingDamage', (sourceEntity, sourcePlayer, targetEntity, weapon, boneIndex, damage) => {
    if (targetEntity && targetEntity.type == 'player') {
        let death = targetEntity.getVariable('Player:Death');
        if (death == true) {
            return true;
        }
    }
});

//Death
mp.events.add("Client:SetDeath", (time) => {
    hideMenus();
    death = true;
    localPlayer.freezePosition(true);
    mp.game.ui.displayHud(false);
    enableDisableRadar(false);
    weaponDamage = 0;
    mp.game.graphics.setTimecycleModifier("MP_death_grade_blend01");
    mp.game.graphics.setTimecycleModifierStrength(0.6);
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.setDeath('${time}');`)
    }
});

mp.events.add("Client:UnsetDeath", () => {
    hideMenus();
    death = false;
    mp.game.graphics.setTimecycleModifierStrength(0.2);
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.hideDeath();`)
    }
    setTimeout(() => {
        localPlayer.freezePosition(false);
        mp.game.ui.displayHud(true);
        enableDisableRadar(true);
        mp.game.invoke("0x0F07E7745A236711");
        mp.game.invoke("0x31B73D1EA9F01DA2");
        localPlayer.resurrect();
    }, 2250);
});

mp.events.add("Client:RespawnPlayer", () => {
    mp.events.callRemote('Server:RespawnPlayer');
});

//Playerspawn
mp.events.add("playerSpawn", (player) => {
    let adminduty = player.getVariable('Player:AdminLogin');
    if (adminduty) {
        player.setInvincible(true);
    } else {
        player.setInvincible(false);
    }
    player.resetAlpha();
    mp.game.graphics.stopScreenEffect("DeathFailMPDark");
});

//Playerdeath
mp.events.add("playerDeath", (player, reason, killer) => {
    if (player == localPlayer) {

        //Damage effect
        mp.game.graphics.stopScreenEffect("DeathFailMPDark");

        //Waypoint
        if (waypointHandle != null) {
            waypointHandle.destroy();
            waypointHandle = null;
        }

        //Hide all menus
        hideMenus();

        //PDCam
        if (pdCam == true) {
            pdCam = false;
        }

        //Livemap
        if (livemap == true) {
            if (livemapinterval != null) {
                clearInterval(livemapinterval);
            }
            livemap = false;
            for (let i = 0; i < liveposisold.length; i++) {
                if (liveposisold[i]) {
                    liveposisold[i].destroy();
                    liveposisold[i] = null;
                }
            }
        }

        //Nametag
        maxDistance = 25 * 4;

        //Handsup
        handsUp = false;

        //Weaponswitch
        oldCheck = 7331;
        localPlayer.weapon = mp.game.joaat('weapon_unarmed');

        //Lastshop
        lastShop = 'n/A';

        //Globalbool
        globalBool = false;

        //Cleaning
        if (cleanerHandle4 != null) {
            cleanerHandle4.destroy();
            cleanerHandle4 = null;
        }
        cleanerCount = 0;
        cleaningTask = false;
        cleanerTime = 0;

        //Farmer
        if (farmerStatus > 0) {
            farmerCount = 0;
            farmerStatus = 0;
            farmerTime = 0;

            if (farmerHandle != null) {
                farmerHandle.destroy();
                farmerHandle = null;
            }

            if (farmerHandle2 != null) {
                farmerHandle2.destroy();
                farmerHandle2 = null;
            }
            if (farmerHandles) {
                for (let i = 0; i < farmerHandles.length; i++) {
                    if (farmerHandles[i]) {
                        farmerHandles[i].destroy();
                        farmerHandles[i] = null;
                    }
                }
            }
        }

        //Ammunation
        if (startRange == true) {
            if (rangeHandle != null) {
                rangeHandle.destroy()
                rangeHandle = null;
            }
            rangePoints = 0;
            rangeStatus = 0;
            getPoint = 0;
            startRange = false;
        }

        //Ciga & Joint
        setCiga = false;
        setJoint = false;

        //Spectate
        mp.events.call('Client:StopSpectate', 1);

        //Radio
        animationSet = 0;
    }
});

//PlayerWeaponShot
mp.events.add('playerWeaponShot', (targetPosition, targetEntity) => {
    let distance = distanceVector(localPlayer.position, new mp.Vector3(1714.7175, -571.90814, 144.50644));
    if (distance <= 125) {
        animalPointing = pointingAt(1000, false);
        if (animalPointing && animalPointing.entity.type == 'ped' && animalPointing.entity.hasVariable('Ped:Death')) {
            let pedDeath = animalPointing.entity.getVariable('Ped:Death');
            if (pedDeath == 0) {
                mp.events.callRemote('Server:UpdatePedStateOfHunting', parseInt(animalPointing.entity.remoteId), 1, animalPointing.entity.getCoords(true));
                return;
            }
        }
    }
    if (startRange == true && rangeHandle != null) {
        let distance = 0;
        if (rangeStatus == 3) {
            distance = distanceVector(oldTargetPosition2[getPoint], targetPosition);
        } else {
            distance = distanceVector(oldTargetPosition[getPoint], targetPosition);
        }
        if (distance <= 1.25) {
            mp.game.audio.playSoundFrontend(-1, "Boss_Message_Orange", "GTAO_Boss_Goons_FM_Soundset", true);
            rangeHandle.destroy()
            rangeHandle = null;
            if (rangePoints > 0) {
                rangePoints--;
                let randValue = 0;
                getPoint = randValue;
                if (rangeStatus != 3) {
                    randValue = getRandomInt(13);
                    getPoint = randValue;
                    rangeHandle = createObject("gr_prop_gr_target_05b", oldTargetPosition[getPoint], new mp.Vector3(0.0, 0.0, -18.30), localPlayer.dimension);
                } else {
                    randValue = getRandomInt(9);
                    getPoint = randValue;
                    rangeHandle = createObject("gr_prop_gr_target_05b", oldTargetPosition2[getPoint], new mp.Vector3(0.0, 0.0, -21.30), localPlayer.dimension);
                }
            } else {
                let secs = Date.now() / 1000 - rangeTime;
		showAmmu = false;
                if (rangeStatus == 1) {
                    mp.events.callRemote('Server:StartRange', 2, secs);
                } else {
                    mp.events.callRemote('Server:StartRange', 3, secs);
                }
            }
        }
    }
})

//Stream in/out
mp.events.add('entityStreamIn', (entity) => {
    try {
        if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.type == 'vehicle') {
            //DL
            if (vehicleListDl) {
                vehicleListDl.push(entity);
            }
            //3D Text
            if (list3D && entity.hasVariable("Vehicle:Text3D")) {
                list3D.push(entity);
            }
            //Vehicle Control
            if (entity.hasVariable('Vehicle:Doors') && entity.hasVariable('Vehicle:Windows')) {
                entity.doors = JSON.parse(entity.getVariable('Vehicle:Doors'));
                entity.windows = JSON.parse(entity.getVariable('Vehicle:Windows'));
                if (entity.doors) {
                    entity.doors.forEach((state, index) => {
                        if (index <= 5) {
                            if (state) entity.setDoorOpen(index, false, false);
                            else entity.setDoorShut(index, false);
                        }
                    })
                }
                if (entity.windows) {
                    entity.windows.forEach((state, index) => {
                        if (index <= 3) {
                            if (state) entity.rollDownWindow(index);
                            else entity.rollUpWindow(index);
                        }
                    })
                }
            }
            //Vehicle Sync
            if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.hasVariable('Vehicle:Sync')) {
                let sync = entity.getVariable('Vehicle:Sync');
                if (sync) {
                    let syncComponents = sync.split(",");
                    if (syncComponents[2] == '1') {
                        entity.setAlarm(true);
                        entity.startAlarm();
                    } else {
                        entity.setAlarm(false);
                        entity.startAlarm();
                    }
                    if (syncComponents[3] == '1') {
                        entity.setTyreBurst(0, false, 1000);
                        entity.setTyreBurst(1, false, 1000);
                        entity.setTyreBurst(4, false, 1000);
                        entity.setTyreBurst(5, false, 1000);
                        entity.setBurnout(false);
                    } else {
                        entity.setBurnout(false);
                    }
                    if (syncComponents[4] == '1') {
                        entity.setHealth(175);
                        entity.setEngineHealth(175);
                        entity.setBodyHealth(175);
                        entity.setDamage(0.32, 0.74, 0.01, 300, 20, true);
                    }
                    if (syncComponents[5]) {
                        if (syncComponents[5].length > 1) {
                            if (syncComponents[5].split('|')[0] == '1') {
                                if (syncComponents[5].split('|')[1] != 0) {
                                    entity.position = new mp.Vector3(entity.position.x, entity.position.y, parseFloat(syncComponents[5].split('|')[1]));
                                    entity.freezePosition(true);
                                } else {
                                    setTimeout(() => {
                                        entity.freezePosition(true);
                                    }, 265);
                                }
                            } else {
                                entity.freezePosition(false);
                            }
                        } else {
                            if (syncComponents[5] == '1') {
                                entity.freezePosition(true);
                                entity.setInvincible(true);
                            } else {
                                entity.freezePosition(false);
                                entity.setInvincible(false);
                            }
                        }
                    }
                }
            }
            //Tuning
            if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.hasVariable('Vehicle:Tuning') && !containsObject(entity, vehicleListTuning)) {
                let tuning = entity.getVariable('Vehicle:Tuning');
                if (tuning) {
                    if (tuning.length > 10) {
                        let arrComponents = tuning.split(',');
                        if (arrComponents) {
                            vehicleListTuning.push(entity);
                            let tuning;
                            let component;
                            for (let i = 0; i < arrComponents.length; i++) {
                                if (arrComponents[i] && i != 66 && i != 67 && i != 68 && i != 69) {
                                    tuning = i;
                                    component = arrComponents[i];
                                    if (isNaN(tuning) || isNaN(component)) return;
                                    if (tuning != 66 && tuning != 67 && tuning != 68 && tuning != 69 && tuning != 55 && tuning != 56 && tuning != 57 && tuning != 53 && tuning < 58) {
                                        if (tuning == 22) {
                                            if (component == -1) {
                                                entity.toggleMod(22, false);
                                                entity.setMod(parseInt(tuning), -1);
                                                mp.game.invoke("0xE41033B25D003A07", entity.handle, 255);
                                            } else {
                                                entity.toggleMod(22, true);
                                                entity.setMod(parseInt(tuning), 0);
                                                mp.game.invoke("0xE41033B25D003A07", entity.handle, parseInt(component));
                                            }
                                        } else {
                                            entity.setMod(parseInt(tuning), parseInt(component));
                                        }
                                    } else if (tuning == 55) {
                                        entity.setWindowTint(parseInt(component));
                                    } else if (tuning == 57) {
                                        if (component > -1) {
                                            entity.toggleMod(20, true);
                                            if (component == 0) {
                                                entity.setTyreSmokeColor(230, 16, 34);
                                            } else if (component == 1) {
                                                entity.setTyreSmokeColor(2, 35, 250);
                                            } else if (component == 2) {
                                                entity.setTyreSmokeColor(44, 196, 10);
                                            } else if (component == 3) {
                                                entity.setTyreSmokeColor(245, 208, 0);
                                            } else if (component == 4) {
                                                entity.setTyreSmokeColor(128, 14, 124);
                                            } else if (component == 5) {
                                                entity.setTyreSmokeColor(240, 115, 48);
                                            } else if (component == 6) {
                                                entity.setTyreSmokeColor(14, 168, 207);
                                            } else if (component == 7) {
                                                entity.setTyreSmokeColor(209, 82, 207);
                                            } else if (component == 8) {
                                                entity.setTyreSmokeColor(95, 158, 160);
                                            } else if (component == 9) {
                                                entity.setTyreSmokeColor(255, 255, 255);
                                            } else if (component == 10) {
                                                entity.setTyreSmokeColor(1, 4, 10);
                                            }
                                        } else {
                                            entity.toggleMod(20, false);
                                            entity.setTyreSmokeColor(0, 0, 0);
                                        }
                                    } else if (tuning >= 58 && tuning <= 63) {
                                        if (tuning == 58) {
                                            entity.setHandling("FINITIALDRIVEFORCE", component);
                                            setVehicleSpeed(entity);
                                        } else if (tuning == 59) {
                                            entity.setHandling("FDRIVEINERTIA", component);
                                        } else if (tuning == 60) {
                                            entity.setEnginePowerMultiplier(component);
                                            entity.setEngineTorqueMultiplier(component);
                                        } else if (tuning == 61) {
                                            entity.setHandling("FBRAKEBIASFRONT", component);
                                        } else if (tuning == 62) {
                                            entity.setHandling("FBRAKEFORCE", component);
                                        } else if (tuning == 63)
                                            entity.setHandling("FDRIVEBIASFRONT", component);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Nitro
            if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.hasVariable('Vehicle:NitroStatus')) {
                let nitro = entity.getVariable('Vehicle:NitroStatus');
                if (nitro == 1) {
                    vehiclesWithNitro.push(entity);
                    setVehicleSpeed(entity);
                }
            }
        }
        if (mp.peds.exists(entity) && 0 !== entity.handle && entity.type == 'ped') {
            if (entity.hasVariable("Ped:Death")) {
                let pedDeath = entity.getVariable("Ped:Death");
                if (pedDeath == 1 && getPlayerHealth(entity) > 0) {
                    entity.setHealth(0);
                    setTimeout(() => {
                        entity.freezePosition(true);
                    }, 455);
                }
                if (pedDeath == 0) {
                    entity.freezePosition(false);
                    entity.setInvincible(true);
                    entity.setProofs(false, false, false, false, false, false, false, false);
                    entity.setAsEnemy(true);
                    entity.setKeepTask(true);
                    entity.setCanBeDamaged(true);
                    entity.setHealth(375);
                    entity.setOnlyDamagedByPlayer(true);
                    entity.setCombatAbility(100);
                    entity.setCombatRange(1);
                    entity.setCombatMovement(2);
                    entity.setCombatAttributes(46, true);
                    entity.setFleeAttributes(0.0, false);
                    entity.taskCombatHatedTargetsAround(4.5, 0);
                }
            } else {
                if (ped != null && entity == ped) {
                    entity.freezePosition(false);
                    entity.setInvincible(true);
                    entity.setProofs(false, false, false, false, false, false, false, false);
                }
            }
        }
        if (mp.players.exists(entity) && 0 !== entity.handle && entity.type == 'player' && entity.remoteId != localPlayer.remoteId) {
            //Grabbing
            if (entity.hasVariable("Player:Adminsettings") && entity.getVariable("Player:FollowStatus" > 0)) {
                var entity2 = null;
                mp.players.forEach(p => {
                    if (p.remoteId === entity.getVariable('Player:Follow')) {
                        entity2 = p;
                    }
                });
                setTimeout(function () {
                    if (entity2 && mp.players.exists(entity2)) {
                        if (value == 1) {
                            entity2.attachTo(entity.handle,
                                11816,
                                0.35, 0.35, 0.08, 0, 0, 0,
                                false, false, false, false, 2, true);
                        } else if (value == 2) {
                            entity2.attachTo(entity.handle,
                                0,
                                0.27, 0.15, 0.63, 0.5, 0.5, 180,
                                false, false, false, false, 2, false);
                        } else {
                            entity2.detach(false, false);
                        }
                    }
                }, 115);
            }
            //Walkingstyle
            if (!entity.hasVariable("Player:Crouching")) {
                entity.resetStrafeClipset();
                setWalkingStyle(entity, entity.getVariable("Player:WalkingStyle"));
            }
            //Crouching
            else {
                entity.resetStrafeClipset();
                entity.setMovementClipset(movementClipSet, clipSetSwitchTime);
                entity.setStrafeClipset(strafeClipSet);
            }
            //Adminsettings
            if (entity.hasVariable("Player:Adminsettings")) {
                let adminSettings = entity.getVariable("Player:Adminsettings").split(',');
                if (adminSettings) {
                    if (adminSettings[2] == '1') {
                        entity.setAlpha(254);
                    } else {
                        entity.setAlpha(255);
                    }
                    if (adminSettings[0] == '1' || adminSettings[0] == '2') {
                        entity.setInvincible(true);
                        if (entity != localPlayer) {
                            localPlayer.setNoCollision(entity.handle, true);
                            entity.setNoCollision(localPlayer.handle, true);
                        }
                    } else {
                        entity.setInvincible(false);
                        if (entity != localPlayer) {
                            localPlayer.setNoCollision(entity.handle, false);
                            entity.setNoCollision(localPlayer.handle, false);
                        }
                    }
                    if (adminSettings[1] == '1' || adminSettings[1] == '2') {
                        if (entity != localPlayer || adminSettings[1] == '2') {
                            entity.setAlpha(0);
                        } else {
                            entity.setAlpha(125);
                        }
                    } else {
                        entity.resetAlpha();
                    }
                }
            }
            //Weapon tint
            var hastint = entity.hasVariable("Player:WeaponTint");
            if (hastint) {
                var tint = entity.getVariable("Player:WeaponTint");
                if (tint && parseInt(tint) > -1) {
                    setTimeout(function () {
                        mp.game.invoke("0x50969B9B89ED5738", entity.handle, entity.weapon >> 0, parseInt(tint) >> 0);
                    }, 275);
                }
            }
            //Weapon Components
            var hascomponents = entity.hasVariable("Player:WeaponComponents");
            if (hascomponents) {
                var components = entity.getVariable("Player:WeaponComponents");
                var componentsArray = [];
                componentsArray = components.split('|');

                listWeaponComponents[entity.remoteId] = components;

                setTimeout(function () {
                    if (components != "|") {
                        for (var i = 0; i < componentsArray.length; i++) {
                            if (componentsArray[i].length > 3) {
                                mp.game.invoke("0xD966D51AA5B28BB9", entity.handle, entity.weapon >> 0, mp.game.joaat(componentsArray[i]) >> 0);
                            }
                        }
                    }
                }, 375);
            }
            //Attachments
            var tempObj = null;
            var hasAttachments = entity.hasVariable("Player:Attachments");
            var value = "0";
            if (hasAttachments) {
                value = entity.getVariable("Player:Attachments");
            }
            let newAttachments = null;
            if (value.length > 1) {
                newAttachments = value.split(',');
            }
            //Old Attachments
            if (entityAttachments) {
                entityAttachments.forEach(function (item, index, array) {
                    if (item && value && item.entityid == entity.remoteId) {
                        if (value.length <= 1 || !newAttachments || (newAttachments && !newAttachments.includes(item.name))) {
                            if (item.object && item.delete == false) {
                                tempObj = item;
                                setTimeout(() => {
                                    if (tempObj.object) {
                                        tempObj.object.detach(false, false);
                                        tempObj.object.destroy();
                                        tempObj.delete = true;
                                    }
                                }, 25);
                            }
                        }
                    }
                });
            }
            entityAttachments = entityAttachments.filter(am => am.delete == false);
            //New Attachments
            if (newAttachments) {
                for (let i = 0; i < newAttachments.length; i++) {
                    if (newAttachments[i] && !containsAttachment(newAttachments[i], entity) && newAttachments[i] != 'n/A') {
                        attachment = GetAttachmentByName(newAttachments[i]);
                        if (!attachment) continue;

                        mp.game.streaming.requestModel(mp.game.joaat(attachment.prop));

                        const object = {
                            name: attachment.name,
                            entityid: entity.remoteId,
                            object: createObject(attachment.prop, new mp.Vector3(entity.position.x, entity.position.y, entity.position.z - 12.5), localPlayer.dimension),
                            delete: false
                        }

                        object.object.setCollision(false, false);

                        setTimeout(function () {
                            object.object.attachTo(entity.handle,
                                entity.getBoneIndex(parseInt(attachment.boneindex)),
                                attachment.position.x, attachment.position.y, attachment.position.z,
                                attachment.rotation.x, attachment.rotation.y, attachment.rotation.z,
                                true, false, false, false, 2, true);

                            entityAttachments.push(object);
                        }, 125);
                    }
                }
            }
        }
    } catch { }
});

mp.events.add('entityStreamOut', (entity) => {
    try {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (!spawned) return;
        if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.type == 'vehicle') {
            //DL
            vehicleListDl = vehicleListDl.filter(function (element) {
                element != entity;
            });
            //3D Text
            if (entity.hasVariable("Vehicle:Text3D")) {
                list3D = list3D.filter(function (element) {
                    element != entity;
                });
            }
            //Tuning
            vehicleListTuning = vehicleListTuning.filter(function (element) {
                element != entity;
            });
            //Nitro
            if (vehiclesWithNitro.length > 0) {
                vehiclesWithNitro = vehiclesWithNitro.filter(function (element) {
                    element != entity;
                });
            }
            setVehicleSpeed(entity);
        }
        if (mp.players.exists(entity) && 0 !== entity.handle && entity.type == 'player' && entity.remoteId != localPlayer.remoteId) {
            //Weaponcomponents
            if (listWeaponComponents[entity.remoteId]) {
                listWeaponComponents[entity.remoteId] = [];
            }
            //Attachments
            var tempObj = null;
            if (entityAttachments) {
                entityAttachments.forEach(function (item, index, array) {
                    if (item && item.entityid == entity.remoteId) {
                        if (item.object && item.delete == false) {
                            tempObj = item;
                            setTimeout(() => {
                                if (tempObj.object) {
                                    tempObj.object.detach(false, false);
                                    if (tempObj.object) {
                                        tempObj.object.destroy();
                                        tempObj.delete = true;
                                    }
                                }
                            }, 25);
                        }
                    }
                });
                entityAttachments = entityAttachments.filter(am => am.delete == false);
            }
        }
    } catch { }
});

//HideCursor
mp.events.add('Client:HideCursor', () => {
    mp.gui.cursor.show(false, false);
    mp.game.ui.displayHud(true);
    showHideChat(true);
    enableDisableRadar(true);
    if (showFurniture == true) {
        showFurniture = false;
    }
});

//ShowCursor
mp.events.add('Client:ShowCursor', () => {
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    showHideChat(false);
    enableDisableRadar(false);
});

//CopyToClipboard
mp.events.add('Client:CopyToClipboard', (text) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.inventory.copyToClipboard('${text}');`)
    }
});

//Bank
mp.events.add('Client:ShowBankMenu', (json, atbank, defaultbank, id) => {
    if (hudWindow != null) {
        showBank = true;
        mp.gui.cursor.show(true, true);
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        hudWindow.execute(`gui.bank.showBankMenu('${json}','${atbank}','${defaultbank}','${id}');`)
    }
});

mp.events.add('Client:HideBankMenu', () => {
    if (hudWindow != null) {
        showBank = false;
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        hudWindow.execute(`gui.bank.hideBankMenu();`)
        mp.events.callRemote('Server:StopAnimation');
    }
});

mp.events.add('Client:UpdateBankMenu', (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.bank.updateBankMenu('${json}');`)
    }
});

mp.events.add('Client:ShowBankFiles', (json, json2) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.bank.showBankFiles('${json}','${json2}');`)
    }
});

mp.events.add('Client:BankSettings', (setting, json) => {
    if (hudWindow != null) {
        showBank = true;
        mp.events.callRemote('Server:BankSettings', setting, json);
    }
});

mp.events.add('Client:ShowStandingOrder', (json) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.bank.showStandingOrder('${json}');`)
    }
});

//Gangzones
mp.events.add('Client:ShowGangzone', () => {
    if (showgangzone == false) {
        mp.events.call('Client:UpdateHud3');
        mp.events.callRemote('Server:GetGangzoneInfos');
    } else {
        mp.events.call('Client:HideGangzone');
    }
});

mp.events.add('Client:HideGangzone', () => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.hideGangzone();`)
        mp.events.call('Client:UpdateHud3');
    }
    nokeys = false;
    showgangzone = false;
    mp.gui.cursor.show(false, false);
});

mp.events.add('Client:ShowGangzoneInfos', (json1, json2, group) => {
    nokeys = true;
    showgangzone = true;
    mp.gui.cursor.show(true, true);
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.showGangzone('${json1}','${json2}','${group}');`)
    }
});

mp.events.add('Client:GetGangzone', () => {
    if (showgangzone == true) {
        mp.events.callRemote('Server:GetGangzone');
    }
});

//Crafting
mp.events.add('Client:StartCraft', (weaponinfo) => {
    mp.events.callRemote('Server:CraftItem', weaponinfo);
});

mp.events.add('Client:ShowCraft', (mats = 0) => {
    showcrafting = !showcrafting;
    nokeys = showcrafting;
    mp.gui.cursor.show(showcrafting, showcrafting);
    mp.events.call('Client:UpdateHud3');
    if (hudWindow != null) {
        hudWindow.execute(`gui.hud.showCraft('${mats}');`)
    }
});

//Jobs
//Allgemein
mp.events.add('Client:JobSettings', (setting, value) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:JobSettings', setting, value);
    }
});

//Spedition
mp.events.add('Client:ShowSpedition', (id, json) => {
    if (hudWindow != null) {
        if (lastid > 0 && showSped == true && id != lastid) return;
        showSped = !showSped;
        mp.events.call('Client:ShowHud');
        if (showSped == true) {
            setTimeout(() => {
                mp.gui.cursor.show(true, true);
            }, 35);
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
        } else {
            mp.gui.cursor.show(false, false);
            mp.game.ui.displayHud(true);
            showHideChatChat(true);
            enableDisableRadar(true);
        }
        if (id == 1) {
            hudWindow.execute(`gui.spediteur.showVehicles('${json}');`)
        } else if (id == 2) {
            hudWindow.execute(`gui.spediteur.showBizzWithNeeds('${json}');`)
        } else if (id == 3) {
            hudWindow.execute(`gui.spediteur.showOrders('${json}');`)
        } else if (id == 4) {
            hudWindow.execute(`gui.spediteur.showTaxiJobs('${json}');`)
        } else if (id == 5) {
            hudWindow.execute(`gui.spediteur.showATMSpots('${json}');`)
        } else if (id == 6) {
            hudWindow.execute(`gui.spediteur.showSecruity('${json}');`)
        }
        lastid = id;
    }
});

mp.events.add('Client:HideSpedition', () => {
    if (hudWindow != null) {
        if (showSped == false) return;
        mp.events.call('Client:ShowHud');
        showSped = false;
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        hudWindow.execute(`gui.spediteur.hideAll();`)
    }
});

mp.events.add("playerCreateWaypoint", (position) => {
    if (cleanerHandle2 != null || cleanerHandle4 != null || farmerStatus == 4 || farmerStatus == 1337 || drivingSchoolHandle != null || drivingSchoolHandle2 != null) return;
    if (prisonCount > 0) return;
    let adminduty = localPlayer.getVariable('Player:AdminLogin');
    let testmodus = localPlayer.getVariable('Player:Testmodus');
    if ((adminduty || testmodus) && !localPlayer.vehicle && setteleport == false) {
        setteleport = true;
        mp.events.call('Client:UpdatePosition', position.x, position.y, position.z);
        localPlayer.position = position;
        localPlayer.freezePosition(true);
        setTimeout(() => {
            let getGroundZ = mp.game.gameplay.getGroundZFor3dCoord(position.x, position.y, position.z + 500, parseFloat(0), false);
            if (getGroundZ <= 0) {
                getGroundZ = position.z;
            }
            localPlayer.freezePosition(false);
            mp.events.callRemote('Server:Teleport', position.x, position.y, getGroundZ + 0.25, false, false);
            setteleport = false;
        }, 2850);
    }
    let faction = localPlayer.getVariable('Player:Faction');
    if (localPlayer.vehicle || faction == 1 || faction == 2) {
        mp.events.callRemote('Server:Teleport', position.x, position.y, position.z, false, true);
    }
});

//Waypoint
mp.events.add('Client:CreateWaypoint', (posx, posy) => {
    mp.game.ui.setNewWaypoint(parseFloat(posx), parseFloat(posy));
});

mp.events.add('Client:RemoveWaypoint', () => {
    mp.game.ui.setNewWaypoint(parseFloat(localPlayer.position.x), parseFloat(localPlayer.position.y));
});

mp.events.add('Client:CreateWaypoint2', (posx, posy, posz, name) => {
    if (waypointHandle != null) {
        waypointHandle.destroy();
        waypointHandle = null;
    }
    waypointHandle = mp.blips.new(1, new mp.Vector3(posx, posy, posz), {
        name: name,
        color: 75,
        shortRange: false,
        dimension: localPlayer.dimension
    });
});

mp.events.add('Client:RemoveWaypoint2', () => {
    if (waypointHandle != null) {
        waypointHandle.destroy();
        waypointHandle = null;
    }
});

//Marker
mp.events.add('Client:DrawMarkerWithTime', (markerRange) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (localPlayer.isTypingInTextChat || !spawned || afk == true || ping == true) return;
    timeMarker = markerRange;
    marker2TO = 2;
    return;
});

mp.events.add('Client:CreatePed', (posx, posy, posz, create) => {
    if (create == 1) {
        if (ped != null) {
            ped.destroy();
            ped = null;
        }
        if (pedTimer != null) {
            clearTimeout(pedTimer);
            pedTimer = null;
        }
        mp.events.call("Client:CreateMarker", posx, posy, posz + 1.25, 3);
        mp.events.call("Client:CreateWaypoint", posx, posy);
        var models = ['ig_abigail', 'csb_anton', 'g_m_m_armgoon_01', 'csb_anton', 'ig_money', 'a_m_y_beachvesp_01', 'u_f_y_bikerchic', 's_m_o_busker_01', 's_m_y_construct_02', 'ig_dale', 'ig_fabien', 'a_f_y_fitness_02', 'a_f_o_genstreet_01', 'cs_gurk', 'a_m_m_indian_01'];
        ped = mp.peds.new(
            mp.game.joaat(models[Math.floor(Math.random() * models.length)]),
            new mp.Vector3(posx, posy, posz),
            Math.floor(Math.random() * 360),
            0
        );
        setTimeout(() => {
            ped.freezePosition(false);
            ped.setInvincible(false);
        }, 315);
    } else if (create == 2) {
        mp.events.call("Client:CreateMarker", posx, posy, posz, 30);
        mp.events.call("Client:CreateWaypoint", posx, posy);
        ped.taskEnterVehicle(mp.players.local.vehicle.handle, 2750, 0, 1, 1, 0);
    } else if (create == 3) {
        mp.events.call("Client:DeleteMarker");
        ped.taskLeaveVehicle(mp.players.local.vehicle.handle, 0);
        setTimeout(() => {
            ped.taskWanderInArea(posx, posy, posz, 95, 0, 1.25);
        }, 955);
        pedTimer = setTimeout(() => {
            if (ped != null) {
                ped.destroy();
                ped = null;
            }
        }, 8500);
    }
});

mp.events.add('Client:DeletePed', () => {
    if (ped != null) {
        ped.destroy();
        ped = null;
    }
});

mp.events.add('Client:CreateMarker', (posx, posy, posz, markerid) => {
    if (marker != null) {
        marker.destroy();
        marker = null;
    }
    size = 1.5;
    if (markerid == 3) {
        size = 0.5;
    }
    marker = mp.markers.new(markerid, new mp.Vector3(posx, posy, posz + 0.2), size, {
        direction: new mp.Vector3(0, 0, 0),
        rotation: new mp.Vector3(0, 0, 0),
        color: [63, 103, 145, 225],
        visible: true,
        dimension: 0
    });
});

mp.events.add('Client:DeleteMarker', () => {
    if (marker != null) {
        marker.destroy();
        marker = null;
    }
});

//3D Text
mp.events.addDataHandler("Vehicle:Text3D", (entity, value, oldValue) => {
    if (value && mp.vehicles.exists(entity) && 0 !== entity.handle) {
        if (!containsObject(entity, list3D)) {
            list3D.push(entity);
        }
    } else {
        if (containsObject(entity, list3D)) {
            list3D = list3D.filter(function (element) {
                element != entity;
            });
        }
    }
});

mp.events.addDataHandler("Player:WalkingStyle", (entity, value) => {

    if (mp.players.exists(entity) && 0 !== entity.handle && !entity.hasVariable("Player:Crouching")) {
        if (entity.type === "player") {
            entity.resetStrafeClipset();
            setWalkingStyle(entity, value);
        }
    }
});

mp.events.addDataHandler("Player:Crouching", (entity, value) => {
    if (mp.players.exists(entity) && 0 !== entity.handle && entity.type === "player") {
        if (value == 1) {
            entity.resetStrafeClipset();
            entity.setMovementClipset(movementClipSet, clipSetSwitchTime);
            entity.setStrafeClipset(strafeClipSet);
        } else {
            entity.resetMovementClipset(clipSetSwitchTime);
            entity.resetStrafeClipset();
        }
    }
});

//Saltychat
mp.events.addDataHandler("Player:Voice", (entity, value, oldValue) => {
    if (hudWindow != null) {
        if (value == -1 && showSaltyError == false) {
            mp.events.call("Client:EnableSaltyError");
        }
    }
});

//Vehicle Doors
mp.events.addDataHandler("Vehicle:Doors", (entity, value, oldValue) => {
    if (!value) return;
    entity.doors = JSON.parse(value);
    if (entity.doors) {
        entity.doors.forEach((state, index) => {
            if (index >= 0 && index <= 5) {
                if (state) entity.setDoorOpen(index, false, false);
                else entity.setDoorShut(index, false);
            }
        })
    }
});

//Vehicle Windows
mp.events.addDataHandler("Vehicle:Windows", (entity, value, oldValue) => {
    if (!value) return;
    entity.windows = JSON.parse(value);
    if (entity.windows) {
        entity.windows.forEach((state, index) => {
            if (index >= 0 && index <= 3) {
                if (state) entity.rollDownWindow(index);
                else entity.rollUpWindow(index);
            }
        })
    }
});

/*mp.events.add('Client:EnableSaltyError', () => {
    if (hudWindow != null) {
        if (showSaltyError == false && afk == false && ping == false) {
            mp.events.call('Client:ShowHud');
            showSaltyError = true;
            hudWindow.execute(`gui.menu.showSaltyError();`)
            mp.events.callRemote('SaltyChat_Disconnected', localPlayer.remoteId);
            mp.events.call("SaltyChat_Disconnected");
            mp.events.call("Client:SetTalkstate", -1);
            hideMenus();
            if(showMenu)
            {
                hudWindow.execute(`gui.menu.showMenu('0','0','0','0','0','0','0','0');`)
            }
            if(showInventory == true)
            {
                mp.events.call("Client:ShowInventory");
            }
        }
    }
})

mp.events.add('Client:DisableSaltyError', () => {
    if (hudWindow != null) {
        if (showSaltyError == true) {
            showSaltyError = false;l
            hudWindow.execute(`gui.menu.showSaltyError();`)
            mp.events.call("SaltyChat_InitToTalkClient", localPlayer.remoteId);
            mp.events.call('Client:ShowHud');
        }
    }
})*/

mp.events.add('Client:SetTalkstate', (gettalkstate) => {
    if (hudWindow != null) {
        if (gettalkstate == 1 && talkstate != 0) return;
        if (gettalkstate == 5 && talkstate == 2) return;
        if (gettalkstate == 5) {
            gettalkstate = 0;
        }
        talkstate = gettalkstate;
        hudWindow.execute(`gui.speedometer.setTalkState('${gettalkstate}');`)
    }
})

//Admin Infomessage
mp.events.add('Client:AdminInfoMessage', (msg, time) => {
    if (hudWindow != null) {
        hudWindow.execute(`gui.speedometer.setInfomessage('${msg}','${time}');`)
    }
})

//Shopsystem
mp.events.add('Client:BuyShopItem', (itemname, price, choose, shopname, itemsize) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:BuyShopItem', itemname, price, choose, shopname, itemsize);
    }
});

mp.events.add('Client:BuyShopItem2', (text1, text2) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:BuyShopItem2', text1, text2);
    }
});

mp.events.add('Client:ShowShop', (json, update, shopname, check) => {
    if (hudWindow != null) {
        if (update == 0) {
            showShop = !showShop;
            if (check == 1) {
                mp.events.call('Client:UpdateHud3');
            }
            hudWindow.execute(`gui.shop.showShop('${json}', '${update}', '${shopname}');`)
            if (showShop == true) {
                if (shopname != '24/7 Laden' && lastShop != '24/7 Laden' && shopname != 'Snackpoint' && lastShop != 'Snackpoint' && shopname != 'Apotheke' && lastShop != 'Apotheke' && shopname != 'Bar' && lastShop != 'Bar' && shopname != 'Waffendealer' && lastShop != 'Waffendealer' && shopname != 'Big. D' && lastShop != 'Big. D') {
                    showShop2 = !showShop2;
                    hudWindow.execute(`gui.shop.showShopMenu2('n/A', 'n/A', 'n/A', 0, 0);`);
                }
                if (check == 1) {
                    lastShop = shopname;
                    setTimeout(() => {
                        mp.gui.cursor.show(true, true);
                    }, 35);
                    mp.game.ui.displayHud(false);
                    showHideChat(false);
                    enableDisableRadar(false);
                }
            } else {
                if (check == 1) {
                    setTimeout(() => {
                        lastShop = 'n/A';
                        mp.gui.cursor.show(false, false);
                    }, 35);
                    mp.game.ui.displayHud(true);
                    showHideChat(true);
                    enableDisableRadar(true);
                }
            }
        } else {
            if (check == 1) {
                mp.events.call('Client:UpdateHud3');
            }
            hudWindow.execute(`gui.shop.showShop('${json}', '${update}', '${shopname}');`)
        }
    }
});

mp.events.add('Client:ShowShop2', (text1, text2, shopname, update, show, hud, hudupdate) => {
    if (hudWindow != null) {
        if (shopname == "n/A" && !showShop2) return;
        if (update == 0) {
            showShop2 = !showShop2;
            if (show == 1 && hud == 1) {
                mp.events.call('Client:UpdateHud3');
            }
            hudWindow.execute(`gui.shop.showShopMenu2('${text1}', '${text2}', '${shopname}', '${update}');`)
            if (showShop2 == true) {
                if (show == 1) {
                    lastShop = shopname;
                    setTimeout(() => {
                        mp.gui.cursor.show(true, true);
                    }, 35);
                    mp.game.ui.displayHud(false);
                    showHideChat(false);
                    enableDisableRadar(false);
                    localPlayer.freezePosition(true);
                }
            } else {
                if (show == 1) {
                    setTimeout(() => {
                        mp.gui.cursor.show(false, false);
                    }, 35);
                    mp.game.ui.displayHud(true);
                    showHideChat(true);
                    enableDisableRadar(true);
                    localPlayer.freezePosition(false);
                    lastShop = 'n/A';
                }
            }
        } else {
            if (!hudupdate) {
                mp.events.call('Client:UpdateHud3');
            }
            hudWindow.execute(`gui.shop.showShopMenu2('${text1}', '${text2}', '${shopname}', '${update}');`)
        }
    }
});

//Fuelsystem
mp.events.add("Client:ShowFuelStation", (prodprice, fuel, maxfuel, products) => {
    if (hudWindow != null) {
        showFuel = true;
        mp.gui.cursor.show(true, true);
        showHideChat(false);
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.hud.showFuelStation('${prodprice}','${fuel}','${maxfuel}','${products}');`)
    }
});

mp.events.add("Client:GetFuel", (price, fuel, newfuel) => {
    if (hudWindow != null) {
        showFuel = false;
        mp.gui.cursor.show(false, false);
        showHideChat(true);
        if (price > -1) {
            mp.events.call('Client:UpdateHud3');
            mp.events.callRemote('Server:GetFuel', price, fuel, newfuel);
        } else {
            hudWindow.execute(`gui.hud.hideFuelStation('0');`)
        }
    }
});

mp.events.add("Client:SelectCrosshair", (crosshair) => {
    mp.events.callRemote('Server:SelectCrosshair', crosshair);
    crosshair = crosshair;
});

mp.events.add("Client:SelectWalkingStyle", () => {
    mp.events.callRemote('Server:SelectWalkingStyle');
});

mp.events.addDataHandler("Vehicle:Tuning", (entity, value, oldValue) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned || !entity) return;
    if (entity.type === "vehicle" && mp.vehicles.exists(entity) && 0 !== entity.handle && entity.dimension == localPlayer.dimension) {
        if (value.length > 10) {
            let arrComponents = value.split(',');
            if (arrComponents) {
                if (!containsObject(entity, vehicleListTuning)) {
                    vehicleListTuning.push(entity);
                }
                let tuning;
                let component;
                for (let i = 0; i < arrComponents.length; i++) {
                    if (arrComponents[i] && i != 66 && i != 67 && i != 68 && i != 69) {
                        tuning = i;
                        component = arrComponents[i];
                        if (isNaN(tuning) || isNaN(component)) return;
                        if (tuning != 66 && tuning != 67 && tuning != 68 && tuning != 69 && tuning != 55 && tuning != 56 && tuning != 57 && tuning != 53 && tuning < 58) {
                            if (tuning == 22) {
                                if (component == -1) {
                                    entity.toggleMod(22, false);
                                    entity.setMod(parseInt(tuning), -1);
                                    mp.game.invoke("0xE41033B25D003A07", entity.handle, 255);
                                } else {
                                    entity.toggleMod(22, true);
                                    entity.setMod(parseInt(tuning), 0);
                                    mp.game.invoke("0xE41033B25D003A07", entity.handle, parseInt(component));
                                }
                            } else {
                                entity.setMod(parseInt(tuning), parseInt(component));
                            }
                        } else if (tuning == 55) {
                            entity.setWindowTint(parseInt(component));
                        } else if (tuning == 57) {
                            if (component > -1) {
                                entity.toggleMod(20, true);
                                if (component == 0) {
                                    entity.setTyreSmokeColor(230, 16, 34);
                                } else if (component == 1) {
                                    entity.setTyreSmokeColor(2, 35, 250);
                                } else if (component == 2) {
                                    entity.setTyreSmokeColor(44, 196, 10);
                                } else if (component == 3) {
                                    entity.setTyreSmokeColor(245, 208, 0);
                                } else if (component == 4) {
                                    entity.setTyreSmokeColor(128, 14, 124);
                                } else if (component == 5) {
                                    entity.setTyreSmokeColor(240, 115, 48);
                                } else if (component == 6) {
                                    entity.setTyreSmokeColor(14, 168, 207);
                                } else if (component == 7) {
                                    entity.setTyreSmokeColor(209, 82, 207);
                                } else if (component == 8) {
                                    entity.setTyreSmokeColor(95, 158, 160);
                                } else if (component == 9) {
                                    entity.setTyreSmokeColor(255, 255, 255);
                                } else if (component == 10) {
                                    entity.setTyreSmokeColor(1, 4, 10);
                                }
                            } else {
                                entity.toggleMod(20, false);
                                entity.setTyreSmokeColor(0, 0, 0);
                            }
                        } else if (tuning >= 58 && tuning <= 63) {
                            if (tuning == 58) {
                                entity.setHandling("FINITIALDRIVEFORCE", component);
                                setVehicleSpeed(entity);
                            } else if (tuning == 59) {
                                entity.setHandling("FDRIVEINERTIA", component);
                            } else if (tuning == 60) {
                                entity.setEnginePowerMultiplier(component);
                                entity.setEngineTorqueMultiplier(component);
                            } else if (tuning == 61) {
                                entity.setHandling("FBRAKEBIASFRONT", component);
                            } else if (tuning == 62) {
                                entity.setHandling("FBRAKEFORCE", component);
                            } else if (tuning == 63)
                                entity.setHandling("FDRIVEBIASFRONT", component);
                        }
                    }
                }
            }
        }
    }
});

mp.events.addDataHandler("Vehicle:Sync", (entity, value, oldValue) => {
    if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.typ == 'vehicle') {
        setTimeout(() => {
            let syncComponents = value.split(",");
            if (syncComponents) {
                if (syncComponents[2] == '1') {
                    entity.setAlarm(true);
                    entity.startAlarm();
                } else {
                    entity.setAlarm(false);
                    entity.startAlarm();
                }
                if (syncComponents[3] == '1') {
                    entity.setTyreBurst(0, false, 1000);
                    entity.setTyreBurst(1, false, 1000);
                    entity.setTyreBurst(4, false, 1000);
                    entity.setTyreBurst(5, false, 1000);
                    entity.setBurnout(false);
                } else {
                    entity.setBurnout(false);
                }
                if (syncComponents[4] == '1') {
                    entity.setHealth(175);
                    entity.setEngineHealth(175);
                    entity.setBodyHealth(175);
                    entity.setDamage(0.32, 0.74, 0.01, 300, 20, true);
                }
                if (syncComponents[5]) {
                    if (syncComponents[5].length > 1) {
                        if (syncComponents[5].split('|')[0] == '1') {
                            if (syncComponents[5].split('|')[1] != 0) {
                                entity.position = new mp.Vector3(entity.position.x, entity.position.y, parseFloat(syncComponents[5].split('|')[1]));
                                entity.freezePosition(true);
                            } else {
                                setTimeout(() => {
                                    entity.freezePosition(true);
                                }, 265);
                            }
                        } else {
                            entity.freezePosition(false);
                        }
                    } else {
                        if (syncComponents[5] == '1') {
                            entity.freezePosition(true);
                            entity.setInvincible(true);
                        } else {
                            entity.freezePosition(false);
                            entity.setInvincible(false);
                        }
                    }
                }
            }
        }, 615);
    }
})

mp.events.addDataHandler("Vehicle:NitroStatus", (entity, value, oldValue) => {
    try {
        let spawned = localPlayer.getVariable('Player:Spawned');
        if (!spawned) return;
        if (mp.vehicles.exists(entity) && 0 !== entity.handle && entity.type === "vehicle" && entity.dimension == localPlayer.dimension) {
            if (value == 1) {
                vehiclesWithNitro.push(entity);
                setVehicleSpeed(entity);
            } else {
                vehiclesWithNitro = vehiclesWithNitro.filter(function (element) {
                    element != entity;
                });
                setVehicleSpeed(entity);
            }
        }
    } catch (e) {
        mp.console.logInfo("Error [Vehicle:NitroStatus]: " + e, true, true);
    }
});

mp.events.add("Client:ShowTuningMenu", (stock) => {
    if (hudWindow == null) return;
    let csv;
    if (showTuning == false) {
        if (localPlayer.vehicle.getClass() == 13 || localPlayer.vehicle.getClass() == 21) {
            hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Dieses Fahrzeug kann nicht getuned werden!','error','top-left',3500);`)
            return;
        }
        let getTuningsync = localPlayer.vehicle.getVariable('Vehicle:Tuning');
        let color = localPlayer.vehicle.getVariable('Vehicle:Color');
        let componentNumbers = [];

        if (!getTuningsync || getTuningsync.length <= 10) {
            for (let i = 0; i <= 57; i++) {
                if (i <= 53 && i != 17 && i != 19 && i != 20 && i != 21 && i != 49 && i != 50 && i != 51 && i != 52) {
                    componentNumbers[i] = localPlayer.vehicle.getMod(i);
                } else {
                    componentNumbers[i] = -1;
                }
            }
            componentNumbers[55] = localPlayer.vehicle.getMod(55);
            if (componentNumbers[55] == -1) {
                componentNumbers[55] = 4;
            }
            componentNumbers[58] = localPlayer.vehicle.getHandling("FINITIALDRIVEFORCE");
            componentNumbers[59] = localPlayer.vehicle.getHandling("FDRIVEINERTIA");
            componentNumbers[60] = 1;
            componentNumbers[61] = localPlayer.vehicle.getHandling("FBRAKEBIASFRONT");
            componentNumbers[62] = localPlayer.vehicle.getHandling("FBRAKEFORCE");
            componentNumbers[63] = localPlayer.vehicle.getHandling("FDRIVEBIASFRONT");
            componentNumbers[66] = color.split(',')[0];
            componentNumbers[67] = color.split(',')[1];
            componentNumbers[68] = color.split(',')[3];
            componentNumbers[69] = color.split(',')[2];
            csv = componentNumbers.join(",");
        } else {
            csv = getTuningsync;
        }
    }
    mp.events.call('Client:UpdateHud3');
    mp.events.call('Client:ShowSpeedometer');
    if (showTuning == false) {
        if (!containsObject(localPlayer.vehicle, vehicleListTuning)) {
            vehicleListTuning.push(localPlayer.vehicle);
        }
        hudWindow.execute(`gui.hud.showTuning('${csv}','${stock}');`);
    } else {
        hudWindow.execute(`gui.hud.showTuning('null','0');`);
    }
    showTuning = !showTuning;
    showCursorTemp = !showCursorTemp;
    if (showTuning == true) {
        mp.gui.cursor.show(true, true);
        showHideChat(false);
    } else {
        mp.gui.cursor.show(false, false);
        showHideChat(true);
    }
});

mp.events.add("Client:HideTuningMenu", () => {
    if (hudWindow == null) return;
    mp.events.call('Client:ShowSpeedometer');
    mp.events.call('Client:UpdateHud3');
    hudWindow.execute(`gui.hud.showTuning('null','0');`);
    showTuning = !showTuning;
    showCursorTemp = !showCursorTemp;
    mp.gui.cursor.show(false, false);
    showHideChat(true);
});

mp.events.add("Client:TuningSync", (csv) => {
    mp.events.callRemote('Server:TuningSync', csv);
});

mp.events.add("Client:TuningSet", (csv, stock) => {
    mp.events.callRemote('Server:TuningSet', csv, stock);
});

mp.events.add("Client:TuningPreview", (tuning, component, perlm) => {
    if (localPlayer.vehicle) {
        if (isNaN(tuning) || isNaN(component)) return;
        if (tuning != 66 && tuning != 67 && tuning != 68 && tuning != 69 && tuning != 55 && tuning != 56 && tuning != 57 && tuning != 53 && tuning < 58) {
            if (tuning == 22) {
                if (component == -1) {
                    localPlayer.vehicle.toggleMod(22, false);
                    localPlayer.vehicle.setMod(parseInt(tuning), -1);
                    mp.game.invoke("0xE41033B25D003A07", localPlayer.vehicle.handle, 255);
                } else {
                    localPlayer.vehicle.toggleMod(22, true);
                    localPlayer.vehicle.setMod(parseInt(tuning), 0);
                    mp.game.invoke("0xE41033B25D003A07", localPlayer.vehicle.handle, parseInt(component));
                }
            } else {
                localPlayer.vehicle.setMod(parseInt(tuning), parseInt(component));
            }
        } else if (tuning == 55) {
            localPlayer.vehicle.setWindowTint(parseInt(component));
        } else if (tuning == 57) {
            if (component > -1) {
                localPlayer.vehicle.toggleMod(20, true);
                if (component == 0) {
                    localPlayer.vehicle.setTyreSmokeColor(230, 16, 34);
                } else if (component == 1) {
                    localPlayer.vehicle.setTyreSmokeColor(2, 35, 250);
                } else if (component == 2) {
                    localPlayer.vehicle.setTyreSmokeColor(44, 196, 10);
                } else if (component == 3) {
                    localPlayer.vehicle.setTyreSmokeColor(245, 208, 0);
                } else if (component == 4) {
                    localPlayer.vehicle.setTyreSmokeColor(128, 14, 124);
                } else if (component == 5) {
                    localPlayer.vehicle.setTyreSmokeColor(240, 115, 48);
                } else if (component == 6) {
                    localPlayer.vehicle.setTyreSmokeColor(14, 168, 207);
                } else if (component == 7) {
                    localPlayer.vehicle.setTyreSmokeColor(209, 82, 207);
                } else if (component == 8) {
                    localPlayer.vehicle.setTyreSmokeColor(95, 158, 160);
                } else if (component == 9) {
                    localPlayer.vehicle.setTyreSmokeColor(255, 255, 255);
                } else if (component == 10) {
                    localPlayer.vehicle.setTyreSmokeColor(1, 4, 10);
                }
            } else {
                localPlayer.vehicle.toggleMod(20, false);
                localPlayer.vehicle.setTyreSmokeColor(0, 0, 0);
            }
        } else if (tuning >= 58 && tuning <= 63) {
            if (tuning == 58) {
                localPlayer.vehicle.setHandling("FINITIALDRIVEFORCE", component);
                setVehicleSpeed(localPlayer.vehicle);
            } else if (tuning == 59) {
                localPlayer.vehicle.setHandling("FDRIVEINERTIA", component);
            } else if (tuning == 60) {
                mp.events.callRemote('Server:TuningPreview', 60, component, perlm, null);
                localPlayer.vehicle.setEnginePowerMultiplier(component);
                localPlayer.vehicle.setEngineTorqueMultiplier(component);
            } else if (tuning == 61) {
                localPlayer.vehicle.setHandling("FBRAKEBIASFRONT", component);
            } else if (tuning == 62) {
                localPlayer.vehicle.setHandling("FBRAKEFORCE", component);
            } else if (tuning == 63)
                localPlayer.vehicle.setHandling("FDRIVEBIASFRONT", component);
        } else {
            mp.events.callRemote('Server:TuningPreview', parseInt(tuning), parseInt(component), perlm, null);
        }
    }
});

mp.events.add("Client:GetDefaultChiptuning", () => {
    if (hudWindow != null && localPlayer.vehicle) {
        var defaultHandling = [];
        defaultHandling[0] = localPlayer.vehicle.getHandling("FINITIALDRIVEFORCE");
        defaultHandling[1] = localPlayer.vehicle.getHandling("FDRIVEINERTIA");
        defaultHandling[2] = 1;
        defaultHandling[3] = localPlayer.vehicle.getHandling("FBRAKEBIASFRONT");
        defaultHandling[4] = localPlayer.vehicle.getHandling("FBRAKEFORCE");
        defaultHandling[5] = localPlayer.vehicle.getHandling("FDRIVEBIASFRONT");
        hudWindow.execute(`gui.hud.setDefaultChiptuning('${JSON.stringify(defaultHandling)}');`)
    }
});

mp.events.add("Client:GetMaxTuning", () => {
    if (localPlayer.vehicle) {
        if (hudWindow != null) {
            var maxtuning = [];
            for (let i = 0; i < 56; i++) {
                let tuningcomp = localPlayer.vehicle.getNumMods(i);
                if (!tuningcomp || tuningcomp <= 0) {
                    maxtuning[i] = -1;
                } else {
                    maxtuning[i] = tuningcomp;
                    maxtuning[i] = maxtuning[i] - 1;
                }
            }
            maxtuning[22] = 11;
            maxtuning[56] = -1;
            maxtuning[57] = -1;
            maxtuning[53] = -1;
            maxtuning[40] = -1;
            maxtuning[55] = -1;
            maxtuning[50] = -1;
            maxtuning[51] = -1;
            maxtuning[52] = -1;
            maxtuning[17] = -1;
            maxtuning[19] = -1;
            maxtuning[20] = -1;
            maxtuning[21] = -1;
            maxtuning[64] = -1;
            maxtuning[65] = -1;
            if (localPlayer.vehicle.getClass() != 8 && localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 21) {
                maxtuning[56] = 10;
                maxtuning[57] = 10;
                maxtuning[40] = 0;
                maxtuning[53] = 5;
                maxtuning[55] = 5;
            }
            if (localPlayer.vehicle.getClass() == 8) {
                maxtuning[57] = 10;
            }
            hudWindow.execute(`gui.hud.setMaxTuning('${JSON.stringify(maxtuning)}');`)
        }
    }
});

mp.events.add("Client:RandomTuning", () => {
    if (localPlayer.vehicle) {
        if (hudWindow != null) {
            var maxtuning = [];
            var randomtuning = [];
            var random = 0;
            for (let i = 0; i < 56; i++) {
                let tuningcomp = localPlayer.vehicle.getNumMods(i);
                if (!tuningcomp || tuningcomp <= 0) {
                    maxtuning[i] = -1;
                } else {
                    maxtuning[i] = tuningcomp;
                    maxtuning[i] = maxtuning[i] - 1;
                }
            }
            maxtuning[22] = 11;
            maxtuning[56] = -1;
            maxtuning[57] = -1;
            maxtuning[53] = -1;
            maxtuning[40] = -1;
            maxtuning[55] = -1;
            maxtuning[50] = -1;
            maxtuning[51] = -1;
            maxtuning[52] = -1;
            maxtuning[17] = -1;
            maxtuning[19] = -1;
            maxtuning[20] = -1;
            maxtuning[21] = -1;
            maxtuning[40] = -1;
            maxtuning[66] = 159;
            maxtuning[67] = 159;
            maxtuning[68] = 159;
            maxtuning[69] = 159;
            maxtuning[58] = localPlayer.vehicle.getHandling("FINITIALDRIVEFORCE");
            maxtuning[59] = localPlayer.vehicle.getHandling("FDRIVEINERTIA");
            maxtuning[60] = 1;
            maxtuning[61] = localPlayer.vehicle.getHandling("FBRAKEBIASFRONT");
            maxtuning[62] = localPlayer.vehicle.getHandling("FBRAKEFORCE");
            maxtuning[63] = localPlayer.vehicle.getHandling("FDRIVEBIASFRONT");
            maxtuning[64] = -1;
            maxtuning[65] = -1;
            if (localPlayer.vehicle.getClass() != 8 && localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 21) {
                maxtuning[56] = 10;
                maxtuning[57] = 10;
                maxtuning[53] = 5;
                maxtuning[40] = 0;
                maxtuning[55] = 5;
            }
            if (localPlayer.vehicle.getClass() == 8) {
                maxtuning[57] = 10;
            }
            for (i = 0; i < maxtuning.length; i++) {
                randomtuning[i] = maxtuning[i];
            }
            for (i = 0; i < maxtuning.length; i++) {
                if (maxtuning[i] != -1 && i != 58 && i != 59 && i != 60 && i != 61 && i != 62 && i != 63 && i != 40) {
                    random = getRndInteger(-1, maxtuning[i]);
                    mp.events.call("Client:TuningPreview", i, random, false);
                    randomtuning[i] = random;
                }
            }
            if (localPlayer.vehicle.getClass() != 8 && localPlayer.vehicle.getClass() != 13 && localPlayer.vehicle.getClass() != 14 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 16 && localPlayer.vehicle.getClass() != 21) {
                maxtuning[40] = 0;
                randomtuning[40] = 0;
                mp.events.call("Client:TuningPreview", 40, 0, false);
            }
            mp.events.call("Client:TuningSync", randomtuning.join(','));
        }
    }
});

function hideMenus(check = true) {
    let admin = localPlayer.getVariable('Player:Adminlevel');
    let adminduty = localPlayer.getVariable('Player:AdminLogin');
    let group = localPlayer.getVariable('Player:Group');
    let grouprang = localPlayer.getVariable('Player:GroupRang');
    let faction = localPlayer.getVariable('Player:Faction');
    let factionrang = localPlayer.getVariable('Player:FactionRang');
    let job = localPlayer.getVariable('Player:Job');
    if (cctvShow == true) {
        mp.events.call('Client:StartStopCCTV');
    }
    if (showMdc == true) {
        setTimeout(function () {
            mp.events.call('Client:MDCSettings', 'showmdc', 'off');
        }, 150);
    }
    if (moebelModus == true) {
        moebelModus = false;
        mp.events.callRemote('Server:SetMoebelModus', false);
    }
    if (showCityhall == true) {
        if (hudWindow != null) {
            hudWindow.execute(`gui.menu.hideStadthalle();`)
        }
        setTimeout(function () {
            showCityhall = false;
            mp.gui.cursor.show(false, false);
        }, 150);
    }
    if (showMenu == true) {
        mp.events.call("Client:HideMenu");
        if (hudWindow != null) {
            hudWindow.execute(`gui.menu.showMenu('${admin}','${adminduty}','${group}','${grouprang}','${faction}','${factionrang}','${job}');`)
        }
    }
    if (showDl == true) {
        showDl = false;
    }
    if (hack == true) {
        mp.events.call('Client:StopHack2');
    }
    if (showwardrobe == true) {
        mp.events.call('Client:ShowWardrobe', 'null');
    }
    if (afk == true && check == false) {
        afk = false;
        nokeys = false;
        lastclick = (Date.now() / 1000);
        localPlayer.freezePosition(false);
        if (localPlayer.vehicle) {
            localPlayer.vehicle.freezePosition(true);
        }
        mp.events.call('Client:ShowHud');
        mp.events.callRemote('Server:SetAFK');
        showHideChat(true);
        if (hudWindow != null) {
            hudWindow.execute(`gui.menu.setafk();`)
        }
    }
    if (ping == true) {
        ping = false;
        nokeys = false;
        localPlayer.freezePosition(false);
        if (localPlayer.vehicle) {
            localPlayer.vehicle.freezePosition(true);
        }
        mp.events.call('Client:ShowHud');
        showHideChat(true);
        if (hudWindow != null) {
            hudWindow.execute(`gui.menu.setping();`)
        }
    }
    if (clothMenu == true || clothMenu2 == true || clothMenu3 == true) {
        mp.events.callRemote('Server:HideClothMenu', true, false);
    }
    if (clothMenu4 == true) {
        mp.events.callRemote('Server:HideClothMenu', true, false);
    }
    if (showFurniture == true) {
        hudWindow.execute(`gui.furniture.hideMenu();`)
    }
    if (showSped == true) {
        mp.events.call('Client:HideSpedition');
    }
    if (showFuel == true) {
        mp.events.call('Client:GetFuel', -1, 0, 0);
    }
    if (showShop == true) {
        mp.events.call('Client:ShowShop', null, 0, 'n/A', 1);
    }
    if (showShop2 == true) {
        mp.events.call('Client:ShowShop2', 'n/A', 'n/A', 'n/A', 0, 1, 1, false);
    }
    if (showArrest == true) {
        mp.events.call('Client:ShowArrest', 'n/A');
    }
    if (showRadio == true) {
        mp.events.call('Client:ShowRadioSystem', '');
    }
    if (showMusic == true) {
        mp.events.call('Client:ShowMusicStation');
    }
    if (showTicket == true) {
        mp.events.call('Client:ShowTicket', 'n/A');
    }
    if (showRecept == true) {
        mp.events.call('Client:ShowRecept', 'n/A');
    }
    if (showGov == true) {
        setTimeout(function () {
            mp.events.call('Client:ShowGovMenu', 0, 0, 0);
        }, 150);
    }
    if (showAmmu == true) {
        if (showAmmuPolice == false) {
            mp.events.call('Client:HideAmmu', 0);
        } else {
            mp.events.call('Client:HideAmmuPolice', 0);
        }
    }
    if (showHandy == true) {
        showHandy = false;
        mp.events.call('Client:UpdateHud3');
        hudWindow.execute(`gui.smartphone.hideSmartphone();`)
        mp.events.callRemote('Server:HideSmartphone');
        mp.gui.cursor.show(false, false);
        showHideChat(true);
    }
    if (showTuning == true && hudWindow != null) {
        hudWindow.execute(`gui.hud.resetTuning('0');`)
        hudWindow.execute(`gui.hud.syncTuningFunc('0');`)
        mp.events.call('Client:HideTuningMenu');
    }
    if (showgangzone == true) {
        mp.events.call('Client:HideGangzone');
    }
    if (showcrafting == true) {
        mp.events.call('Client:ShowCraft', 0);
    }
    if (showTab == true) {
        setTimeout(function () {
            mp.events.call('Client:UpdateHud3');
            hudWindow.execute(`gui.menu.showTabList('n/A');`)
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            showTab = false;
            mp.gui.cursor.show(false, false);
        }, 150);
    }
    if (showCenterMenu == true) {
        setTimeout(function () {
            showCenterMenu = false;
            hudWindow.execute(`gui.menu.showCenterMenu('n/A','n/A','n/A');`)
            mp.events.call('Client:UpdateHud3');
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            mp.gui.cursor.show(false, false);
            if (centerHeader == "Führungszeugnis" || centerHeader == "Fahrzeugdiagnose" || centerHeader == "Untersuchung") {
                mp.events.callRemote('Server:StopAnimation');
            }
        }, 150);
    }
    if (showDealer == true) {
        setTimeout(function () {
            mp.events.call('Client:DealerShipSettings', 'abort', 'n/A');
        }, 150);
    }
    if (showCarSetting == true) {
        setTimeout(function () {
            mp.events.call('Client:VehicleSettings', 'hide', '0');
        }, 150);
    }
    if (startLockpicking == true && lastProgress) {
        if (!lastProgress.includes('mecha') && !lastProgress.includes('cleaning') && !lastProgress.includes('milking') && !lastProgress.includes('tomato')) {
            setTimeout(function () {
                mp.events.call('Client:FinishProgress', 'failed');
            }, 150);
        }
    }
    if (showBusPlan == true) {
        setTimeout(function () {
            showBusPlan = false;
            mp.events.call('Client:UpdateHud3');
            hudWindow.execute(`gui.hud.showBusPlan('n/A','n/A','n/A','0');`)
        }, 150);
    }
    if (showMecha == true) {
        showMecha = false;
    }
    if (barberMenu == true) {
        hudWindow.execute(`gui.hud.abortHair();`)
    }
    if (tattooShop == true) {
        hudWindow.execute(`gui.hud.abortTattoo();`)
    }
    if (showWheel == true) {
        hudWindow.execute(`gui.selectwheel.hideWheel();`)
        wheelSelected = 0;
        showWheel = false;
        mp.gui.cursor.show(false, false);
    }
    if (showPerso == true) {
        mp.events.call('Client:UpdateHud3');
        showPerso = !showPerso;
        nokeys = !nokeys;
        hudWindow.execute(`gui.hud.showPerso('-1');`)
    }
    if (showLics == true) {
        mp.events.call('Client:UpdateHud3');
        showLics = !showLics;
        nokeys = !nokeys;
        hudWindow.execute(`gui.hud.showLics('-1','-1');`)
    }
    if (nokeys == true) {
        setTimeout(function () {
            nokeys = false;
        }, 150);
    }
}

function distanceVector(v1, v2) {
    var dx = v1.x - v2.x;
    var dy = v1.y - v2.y;
    var dz = v1.z - v2.z;
    return Math.sqrt(dx * dx + dy * dy + dz * dz);
}

function setVehicleSpeed(vehicle) {
    if (vehicle) {
        if (vehicle.hasVariable('Vehicle:Speedlimit') && vehicle.getVariable('Vehicle:Speedlimit') > 0) {
            vehicle.setMaxSpeed(vehicle.getVariable('Vehicle:Speedlimit') / 3.6);
        }
        let funmodus = localPlayer.getVariable('Player:Funmodus');
        if (funmodus) {
            vehicle.setMaxSpeed(999 / 3.6);
            return;
        }
        let transmission = vehicle.getVariable('Vehicle:Transmission');
        if (!transmission || transmission == null) {
            transmission = 1;
        }
        let boost = vehicle.getHandling("FINITIALDRIVEFORCE");
        let speed = vehicle.getVariable('Vehicle:MaxSpeed');
        if (speed == 1) {
            speed = mp.game.vehicle.getVehicleModelMaxSpeed(vehicle.model) * 3.6;
        }
        let percent = 0;
        if (boost > 1 || activateNitro == 1) {
            if (boost > 1) {
                percent = (boost * 10) - 5;
            }
            if (activateNitro == 1) {
                let nitroBonus = speed / 100 * 5;
                percent = percent + nitroBonus;
                vehicle.setEnginePowerMultiplier(transmission + 12);
                vehicle.setEngineTorqueMultiplier(transmission + 12);
            } else {
                vehicle.setEngineTorqueMultiplier(transmission);
            }
            vehicle.setMaxSpeed((speed + percent) / 3.6);
            if (hudWindow != null) {
                hudWindow.execute(`gui.speedometer.updateMaxSpeed('${speed + percent}');`)
            }
        } else {
            if (hudWindow != null) {
                hudWindow.execute(`gui.speedometer.updateMaxSpeed('${speed}');`)
            }
            vehicle.setEngineTorqueMultiplier(transmission);

            if (vehicle.hasVariable('Vehicle:Speedlimit') && vehicle.getVariable('Vehicle:Speedlimit') > 0) {
                speed = vehicle.getVariable('Vehicle:Speedlimit');
            }

            vehicle.setMaxSpeed(speed / 3.6);
        }
    }
}

function getSpeedBonus(vehicle) {
    let percent = 0;
    if (vehicle) {
        let boost = vehicle.getHandling("FINITIALDRIVEFORCE");
        if (boost > 1) {
            percent = (boost * 10) - 5;
        }
    }
    return percent;
}

function GetAdminRang(entity, rang) {
    let adminrang = "Kein Admin";
    if (entity.hasVariable('Player:AdminRang') && entity.getVariable('Player:AdminRang').length > 3) {
        return entity.getVariable('Player:AdminRang');
    }
    switch (rang) {
        case 1: {
            adminrang = 'Probe Moderator';
            break;
        }
        case 2: {
            adminrang = 'Moderator';
            break;
        }
        case 3: {
            adminrang = 'Supporter';
            break;
        }
        case 4: {
            adminrang = 'Administrator';
            break;
        }
        case 5: {
            adminrang = 'High Administrator';
            break;
        }
        case 6: {
            adminrang = 'Manager';
            break;
        }
        case 7: {
            adminrang = 'Development';
            break;
        }
        case 8: {
            adminrang = 'Projektleiter';
            break;
        }
    }
    if (!adminrang) {
        adminrang = "Teammitglied";
    }
    return adminrang;
}

function updateSpeedometer() {
    if (localPlayer.vehicle && showSpeedo == true) {
        if (hudWindow != null) {
            let engineHealth = localPlayer.vehicle.getHealth();
            if(engineHealth < 0) engineHealth = 0;
            if(engineHealth > 1000) engineHealth = 1000;
            let speed = localPlayer.vehicle.getSpeed() * 3.6;
            let locked = localPlayer.vehicle.getDoorLockStatus();
            let engine = localPlayer.getVariable('Player:VehicleEngine');
            let belt = localPlayer.getConfigFlag(32, true); //Seatbelt
            let fuel = localPlayer.vehicle.getVariable('Vehicle:Fuel');
            let maxfuel = localPlayer.vehicle.getVariable('Vehicle:MaxFuel');
            let battery = localPlayer.vehicle.getVariable('Vehicle:Battery');
            let oel = localPlayer.vehicle.getVariable('Vehicle:Oel');
            let beltvalue = 0;
            if (belt) {
                beltvalue = 1;
            }
            let enginevalue = 0;
            if (engine) {
                enginevalue = 1;
            }
            hudWindow.execute(`gui.speedometer.updateSpeedometerSpeed('${speed}','${engineHealth}','${locked}','${enginevalue}','${beltvalue}','${fuel}','${maxfuel}','${battery}','${oel}');`)
        }
    }
}

function CheckForSoda() {
    let atSoda = 0;
    soda = [
        mp.game.joaat("prop_vend_soda_01"),
        mp.game.joaat("prop_vend_soda_02"),
        mp.game.joaat("prop_vend_fridge01"),
    ];
    var sodaRange = 0.60;
    for (let i = 0; i < soda.length; i++) {
        const sodaHash = soda[i];
        var check = mp.game.object.getClosestObjectOfType(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, sodaRange, sodaHash, false, true, true);
        if (check) {
            if (soda[i] == 992069095) {
                atSoda = 1;
            } else {
                atSoda = 2;
            }
            break;
        }
    }
    return atSoda;
}

function CheckForTrash() {
    let atTrash = -1;
    trash = [
        mp.game.joaat("prop_bin_01a"),
        mp.game.joaat("prop_bin_02a"),
        mp.game.joaat("prop_bin_03a"),
        mp.game.joaat("prop_bin_05a"),
    ];
    var trashRange = 0.65;
    for (let i = 0; i < soda.length; i++) {
        const trashHash = trash[i];
        var check = mp.game.object.getClosestObjectOfType(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, trashRange, trashHash, false, true, true);
        if (check) {
            atTrash = check;
            break;
        }
    }
    return atTrash;
}

function CheckForATM() {
    let atATM = -1;
    atm = [
        mp.game.joaat("prop_atm_01"),
        mp.game.joaat("prop_atm_02"),
        mp.game.joaat("prop_atm_03"),
        mp.game.joaat("prop_fleeca_atm"),
    ];
    var atmRange = 0.75;
    for (let i = 0; i < atm.length; i++) {
        const atmHash = atm[i];
        var check = mp.game.object.getClosestObjectOfType(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, atmRange, atmHash, false, true, true);
        if (check) {
            atATM = check;
            break;
        }
    }
    return atATM;
}

function UpdateNameTags1(nametags) {
    try {
        //Nametags
        const graphics = mp.game.graphics;
        const screenRes = graphics.getScreenResolution(0, 0);
        let admindutynt = 0;
        if (localPlayer.hasVariable('Player:AdminLogin')) {
            admindutynt = parseInt(localPlayer.getVariable('Player:AdminLogin'));
        }

        nametags.forEach(nametag => {
            let [player, x, y, distance] = nametag;

            let adminSettings = player.getVariable('Player:Adminsettings');

            if (distance <= maxDistance && adminSettings.split(',')[1] == '0') {
                let scale = (distance / maxDistance);
                if (scale < 0.6) scale = 0.6;

                y -= scale * (0.005 * (screenRes.y / 1080));

                let nname = 'Unbekannt';
                if (admindutynt == 1 || (nameTagList.length > 0 && nameTagList.includes(player.name)) || nametag == 2) {
                    nname = player.name;
                }

                let foundDrone = false;

                if (player.vehicle && player.vehicle.getVariable('Vehicle:Name') == 'rcmavic') {
                    foundDrone = true;
                }

                if (!foundDrone) {
                    if (admindutynt == 0 && player.getAlpha() == 255) {
                        let admindutytemp = 0;
                        if (player.hasVariable('Player:AdminLogin')) {
                            admindutytemp = parseInt(player.getVariable('Player:AdminLogin'));
                        }
                        let realname = player.getVariable('Player:Name');
                        let adminlevel = parseInt(player.getVariable('Player:Adminlevel'));

                        if (admindutytemp == 1) {
                            graphics.drawText(realname + ' [' + player.remoteId + ']\n~r~' + GetAdminRang(player, adminlevel) + '\n', [x, y], {
                                font: 4,
                                color: color,
                                scale: [0.45, 0.45],
                                outline: true
                            });
                        }
                    } else {
                        let admindutytemp = 0;
                        if (player.hasVariable('Player:AdminLogin')) {
                            admindutytemp = parseInt(player.getVariable('Player:AdminLogin'));
                        }
                        var healthplayer = player.getVariable('Player:HealthSync');
                        if (healthplayer > 100) {
                            healthplayer = healthplayer-100;
                        }
                        var armourplayer = player.getArmour();
                        let realname = player.getVariable('Player:Name');
                        let adminlevel = parseInt(player.getVariable('Player:Adminlevel'));
                        if (!adminlevel) {
                            adminlevel = 0;
                        }

                        if (player.getAlpha() == 255) {
                            if (admindutytemp == 0) {
                                graphics.drawText(player.name + ' [' + player.remoteId + ']\nLeben: ' + healthplayer + '%, Rüstung: ' + armourplayer + '%\n', [x, y], {
                                    font: 4,
                                    color: color,
                                    scale: [0.45, 0.45],
                                    outline: true
                                });
                            } else {
                                graphics.drawText(realname + ' [' + player.remoteId + ']\n~r~' + GetAdminRang(player, adminlevel) + '\n', [x, y], {
                                    font: 4,
                                    color: color,
                                    scale: [0.45, 0.45],
                                    outline: true
                                });
                            }
                        }
                    }
                }
            }
        })
    } catch { }
}

function UpdateNameTags2(nametags) {
    try {
        //Nametags
        const graphics = mp.game.graphics;
        const screenRes = graphics.getScreenResolution(0, 0);
        let admindutynt = 0;
        if (localPlayer.hasVariable('Player:AdminLogin')) {
            admindutynt = parseInt(localPlayer.getVariable('Player:AdminLogin'));
        }

        nametags.forEach(nametag => {
            let [player, x, y, distance] = nametag;

            let adminSettings = player.getVariable('Player:Adminsettings');

            if (distance <= maxDistance && adminSettings.split(',')[1] == '0') {
                let scale = (distance / maxDistance);
                if (scale < 0.6) scale = 0.6;

                y -= scale * (0.005 * (screenRes.y / 1080));

                let nname = 'Unbekannt';
                if (admindutynt == 1 || nametagSystem == 2 || (nameTagList.length > 0 && nameTagList.includes(player.name))) {
                    nname = player.name;
                }

                let foundDrone = false;

                if (player.vehicle && player.vehicle.getVariable('Vehicle:Name') == 'rcmavic') {
                    foundDrone = true;
                }

                if (!foundDrone) {
                    if (admindutynt == 0 && player.getAlpha() == 255) {
                        let admindutytemp = 0;
                        if (player.hasVariable('Player:AdminLogin')) {
                            admindutytemp = parseInt(player.getVariable('Player:AdminLogin'));
                        }
                        let realname = player.getVariable('Player:Name');
                        let adminlevel = parseInt(player.getVariable('Player:Adminlevel'));

                        if (admindutytemp == 0) {
                            graphics.drawText(nname + ' [' + player.remoteId + ']\n', [x, y], {
                                font: 4,
                                color: color,
                                scale: [0.45, 0.45],
                                outline: true
                            });
                        } else {
                            graphics.drawText(realname + ' [' + player.remoteId + ']\n~r~' + GetAdminRang(player, adminlevel) + '\n', [x, y], {
                                font: 4,
                                color: color,
                                scale: [0.45, 0.45],
                                outline: true
                            });
                        }
                    } else {
                        let admindutytemp = 0;
                        if (player.hasVariable('Player:AdminLogin')) {
                            admindutytemp = parseInt(player.getVariable('Player:AdminLogin'));
                        }
                        var healthplayer = player.getVariable('Player:HealthSync');
                        if (healthplayer > 100) {
                            healthplayer = healthplayer-100;
                        }
                        var armourplayer = player.getArmour();
                        let realname = player.getVariable('Player:Name');
                        let adminlevel = parseInt(player.getVariable('Player:Adminlevel'));
                        if (!adminlevel) {
                            adminlevel = 0;
                        }

                        if (player.getAlpha() == 255) {
                            if (admindutytemp == 0) {
                                graphics.drawText(player.name + ' [' + player.remoteId + ']\nLeben: ' + healthplayer + '%, Rüstung: ' + armourplayer + '%\n', [x, y], {
                                    font: 4,
                                    color: color,
                                    scale: [0.45, 0.45],
                                    outline: true
                                });
                            } else {
                                graphics.drawText(realname + ' [' + player.remoteId + ']\n~r~' + GetAdminRang(player, adminlevel) + '\n', [x, y], {
                                    font: 4,
                                    color: color,
                                    scale: [0.45, 0.45],
                                    outline: true
                                });
                            }
                        }
                    }
                }
            }
        })
    } catch { }
}

const createObject = (model, pos, rot, dim) => {
    mp.game.streaming.requestModel(mp.game.joaat(model));
    while (!mp.game.streaming.hasModelLoaded(mp.game.joaat(model)))
        mp.game.wait(0);
    return mp.objects.new(mp.game.joaat(model), pos, {
        rotation: rot,
        alpha: 255,
        dimension: parseInt(dim)
    });
}

//Fingerpointing
let pointing = {
    active: false,
    interval: null,
    lastSent: 0,
    start: function () {
        if (!this.active) {
            this.active = true;

            mp.game.streaming.requestAnimDict("anim@mp_point");

            while (!mp.game.streaming.hasAnimDictLoaded("anim@mp_point")) {
                mp.game.wait(0);
            }
            localPlayer.setConfigFlag(36, true)
            localPlayer.taskMoveNetwork("task_mp_pointing", 0.5, false, "anim@mp_point", 24);
            mp.game.streaming.removeAnimDict("anim@mp_point");

            this.interval = setInterval(this.process.bind(this), 0);
        }
    },

    gameplayCam: mp.cameras.new("gameplay"),
    lastSync: 0,

    getRelativePitch: function () {
        let camRot = this.gameplayCam.getRot(2);

        return camRot.x - localPlayer.getPitch();
    },

    process: function () {
        if (this.active) {

            if (localPlayer.isInAnyVehicle(true)) {
                this.active = false;
                return;
            }

            mp.game.invoke("0x921ce12c489c4c41", localPlayer.handle);

            let camPitch = this.getRelativePitch();

            if (camPitch < -70.0) {
                camPitch = -70.0;
            } else if (camPitch > 42.0) {
                camPitch = 42.0;
            }
            camPitch = (camPitch + 70.0) / 112.0;

            let camHeading = mp.game.cam.getGameplayCamRelativeHeading();

            let cosCamHeading = mp.game.system.cos(camHeading);
            let sinCamHeading = mp.game.system.sin(camHeading);

            if (camHeading < -180.0) {
                camHeading = -180.0;
            } else if (camHeading > 180.0) {
                camHeading = 180.0;
            }
            camHeading = (camHeading + 180.0) / 360.0;

            let coords = localPlayer.getOffsetFromGivenWorldCoords((cosCamHeading * -0.2) - (sinCamHeading * (0.4 * camHeading + 0.3)), (sinCamHeading * -0.2) + (cosCamHeading * (0.4 * camHeading + 0.3)), 0.6);
            let blocked = (typeof mp.raycasting.testPointToPoint([coords.x, coords.y, coords.z - 0.2], [coords.x, coords.y, coords.z + 0.2], localPlayer.handle, 7) !== 'undefined');

            mp.game.invoke('0xd5bb4025ae449a4e', localPlayer.handle, "Pitch", camPitch)
            mp.game.invoke('0xd5bb4025ae449a4e', localPlayer.handle, "Heading", camHeading * -1.0 + 1.0)
            mp.game.invoke('0xb0a6cfd2c69c1088', localPlayer.handle, "isBlocked", blocked)
            mp.game.invoke('0xb0a6cfd2c69c1088', localPlayer.handle, "isFirstPerson", mp.game.invoke('0xee778f8c7e1142e2', mp.game.invoke('0x19cafa3c87f7c2ff')) == 4)

            if ((Date.now() - this.lastSent) > 100) {
                this.lastSent = Date.now();
                mp.events.callRemote('Server:FingerPointSync', camPitch, camHeading);
            }
        }
    }
}

mp.events.add("Client:FingerPointSync", (id, camPitch, camHeading) => {
    let netPlayer = getPlayerByRemoteId(parseInt(id));
    if (netPlayer != null) {
        if (netPlayer != localPlayer) {
            netPlayer.lastReceivedPointing = Date.now();

            if (!netPlayer.pointingInterval) {
                netPlayer.pointingInterval = setInterval((function () {
                    if ((Date.now() - netPlayer.lastReceivedPointing) > 1000) {
                        clearInterval(netPlayer.pointingInterval);

                        netPlayer.lastReceivedPointing = undefined;
                        netPlayer.pointingInterval = undefined;

                        mp.game.invoke("0xd01015c7316ae176", netPlayer.handle, "Stop");

                        netPlayer.setConfigFlag(36, false);

                    }
                }).bind(netPlayer), 503);

                mp.game.streaming.requestAnimDict("anim@mp_point");

                while (!mp.game.streaming.hasAnimDictLoaded("anim@mp_point")) {
                    mp.game.wait(0);
                }

                netPlayer.setConfigFlag(36, true)
                netPlayer.taskMoveNetwork("task_mp_pointing", 0.5, false, "anim@mp_point", 24);
                mp.game.streaming.removeAnimDict("anim@mp_point");
            }
            mp.game.invoke('0xd5bb4025ae449a4e', netPlayer.handle, "Pitch", camPitch)
            mp.game.invoke('0xd5bb4025ae449a4e', netPlayer.handle, "Heading", camHeading * -1.0 + 1.0)
            mp.game.invoke('0xb0a6cfd2c69c1088', netPlayer.handle, "isBlocked", 0);
            mp.game.invoke('0xb0a6cfd2c69c1088', netPlayer.handle, "isFirstPerson", 0);
        }
    }
});

//Report
mp.events.add("Client:ReportPlayer", (id) => {
    if (hudWindow != null) {
        mp.events.call('Client:UpdateHud3');
        showTab = false;
        setTimeout(function () {
            mp.events.callRemote('Server:ReportPlayer', id);
        }, 115);
    }
})

//CCTV
mp.events.add("Client:StartStopCCTV", () => {
    if (hudWindow != null) {
        cctvShow = !cctvShow;
        localPlayer.freezePosition(cctvShow);
        nokeys = cctvShow;
        mp.game.ui.displayHud(cctvShow);
        showHideChat(cctvShow);
        enableDisableRadar(cctvShow);
        if (cctvShow == true) {
            mp.events.call("Client:UnSetSmartphoneObj");
            mp.game.graphics.setTimecycleModifier("heliGunCam");
            mp.game.graphics.setTimecycleModifierStrength(0.3);
            mp.events.call('Client:MDCSettings', 'hideshow', 'n/A');
        } else {
            mp.game.invoke("0x0F07E7745A236711");
            mp.game.invoke("0x31B73D1EA9F01DA2");
            mp.events.call('Client:MDCSettings', 'stopcctv');
            mp.events.call('Client:MDCSettings', 'hideshow', 'n/A');
            mp.events.call("Client:SetSmartphoneObj");
        }
    }
})

//Helicam
mp.events.add("Client:StartHeliCamSettings", () => {
    set = false;
    if (nokeys == true) {
        set = true;
        nokeys == false;
        mp.game.ui.displayHud(true);
        showHideChat(true);
        enableDisableRadar(true);
        hideMenus();
    }
    mp.events.call('Client:ShowHud');
    hideMenus();
    if (set == false && nokeys == false) {
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        nokeys = true;
    }
});

//Speedcam
mp.events.add("Client:CheckSpeed", (maxspeed) => {
    if (hudWindow != null && localPlayer.vehicle) {
        let speed = localPlayer.vehicle.getSpeed() * 3.6;
        let percent = speed / 100 * 5;
        if ((speed - percent) > maxspeed) {
            mp.events.call("Client:PlaySound", "speedcamera.ogg", 0);
            mp.game.graphics.startScreenEffect("SwitchShortNeutralIn", 0, false);
            mp.events.callRemote('Server:SpeedCheck', speed - percent);
        }
    }
})


//Speedlimit
mp.events.add("Client:Speedlimit", (speed) => {
    if (localPlayer.vehicle) {
        localPlayer.vehicle.setMaxSpeed(speed / 3.6);
        if (speed != -1) {
            hudWindow.execute(`gui.speedometer.updateMaxSpeed('${(speed / 3.6) - 1}');`)
        } else {
            hudWindow.execute(`gui.speedometer.updateMaxSpeed('${mp.game.vehicle.getVehicleModelMaxSpeed(localPlayer.vehicle.model) * 3.6}');`)
        }
    }
})

//Dealership
mp.events.add("Client:DealerShipSettings", (setting, value) => {
    if (hudWindow != null) {
        if (setting == 'testdrive' || setting == 'buyvehicle') {
            value = value + "," + oldVehicleShipColor;
        }
        mp.events.callRemote('Server:DealerShipSettings', setting, value);
    }
})

mp.events.add("Client:UpdateDealerShipVehicle", (name, maxspeed) => {
    if (hudWindow != null) {
        if (dealerVehicle != null) {
            dealerVehicle.destroy()
            dealerVehicle = null;
        }
        if (dealerShipBizz != 29 && dealerShipBizz != 31 && dealerShipBizz != 32) {
            dealerVehicle = mp.vehicles.new(mp.game.joaat(name.toLowerCase()), new mp.Vector3(232.94792, -994.24974 + 0.7, -99.49524), {
                heading: 30.937542,
                color: oldVehicleShipColor,
                locked: true,
                engine: false,
                dimension: localPlayer.dimension,
                numberPlate: name
            });
        } else {
            if (dealerShipBizz == 29) {
                dealerVehicle = mp.vehicles.new(mp.game.joaat(name.toLowerCase()), new mp.Vector3(656.799, -2669.9346 - 0.6, 6.149509), {
                    heading: -159.37766,
                    color: oldVehicleShipColor,
                    locked: true,
                    engine: false,
                    dimension: localPlayer.dimension,
                    numberPlate: name
                });
            } else if (dealerShipBizz == 31) {
                dealerVehicle = mp.vehicles.new(mp.game.joaat(name.toLowerCase()), new mp.Vector3(-737.6955 + 1.32, -1367.0311, 0.10216603), {
                    heading: 94.24052,
                    color: oldVehicleShipColor,
                    locked: true,
                    engine: false,
                    dimension: localPlayer.dimension,
                    numberPlate: name
                });
            } else if (dealerShipBizz == 32) {
                dealerVehicle = mp.vehicles.new(mp.game.joaat(name.toLowerCase()), new mp.Vector3(-987.2336 + 1.5, -2988.3975, 14.587158), {
                    heading: 110.78762,
                    color: oldVehicleShipColor,
                    locked: true,
                    engine: false,
                    dimension: localPlayer.dimension,
                    numberPlate: name
                });
            }
        }

        if (maxspeed == 0) {
            defaultHandling[0] = localPlayer.vehicle.getHandling("FINITIALDRIVEFORCE");
            defaultHandling[1] = localPlayer.vehicle.getHandling("FDRIVEINERTIA");
            defaultHandling[2] = 1;
            defaultHandling[3] = localPlayer.vehicle.getHandling("FBRAKEBIASFRONT");
            defaultHandling[4] = localPlayer.vehicle.getHandling("FBRAKEFORCE");
            defaultHandling[5] = localPlayer.vehicle.getHandling("FDRIVEBIASFRONT");
        }

        dealerVehicle.freezePosition(true);
        setTimeout(function () {
            dealerVehicle.setColours(parseInt(oldVehicleShipColor), parseInt(oldVehicleShipColor));
        }, 75);
    }
});

mp.events.add("Client:UpdateDealerShipColor", (color) => {
    if (hudWindow != null) {
        if (dealerVehicle != null) {
            dealerVehicle.setColours(parseInt(color), parseInt(color));
        }
        oldVehicleShipColor = color;
    }
});

mp.events.add("Client:ShowDealerShip", (name, vehiclename, json1, csv, bizzid, multiplier) => {
    if (hudWindow != null) {
        mp.game.ui.displayHud(false);
        showHideChat(false);
        enableDisableRadar(false);
        mp.gui.cursor.show(true, true);
        mp.events.call('Client:ShowHud');
        oldVehicleShipColor = 0;
        showDealer = !showDealer;
        dealerShipBizz = bizzid;
        showDl = false;
        vehicleListDl = [];
        hudWindow.execute(`gui.hud.showDealerShip('${name}','${json1}','${csv}','${multiplier}');`)
        if (showDealer == true) {
            if (defCamera != null) {
                defCamera.delete();
                defCamera = null;
            }
            if (dealerVehicle != null) {
                dealerVehicle.destroy()
                dealerVehicle = null;
            }
            if (bizzid != 29 && bizzid != 31 && bizzid != 32) {
                dealerVehicle = mp.vehicles.new(mp.game.joaat(vehiclename.toLowerCase()), new mp.Vector3(232.94792, -994.24974 + 0.7, -99.49524), {
                    heading: 30.937542,
                    color: oldVehicleShipColor,
                    locked: true,
                    engine: false,
                    dimension: localPlayer.dimension,
                    numberPlate: name
                });
            } else {
                if (bizzid == 29) {
                    dealerVehicle = mp.vehicles.new(mp.game.joaat(vehiclename.toLowerCase()), new mp.Vector3(656.799, -2669.9346 - 0.6, 6.149509), {
                        heading: -159.37766,
                        color: oldVehicleShipColor,
                        locked: true,
                        engine: false,
                        dimension: localPlayer.dimension,
                        numberPlate: name
                    });
                } else if (bizzid == 31) {
                    dealerVehicle = mp.vehicles.new(mp.game.joaat(vehiclename.toLowerCase()), new mp.Vector3(-737.6955 + 1.32, -1367.0311, 0.10216603), {
                        heading: 94.24052,
                        color: oldVehicleShipColor,
                        locked: true,
                        engine: false,
                        dimension: localPlayer.dimension,
                        numberPlate: name
                    });
                } else if (bizzid == 32) {
                    dealerVehicle = mp.vehicles.new(mp.game.joaat(vehiclename.toLowerCase()), new mp.Vector3(-987.2336 + 1.5, -2988.3975, 14.587158), {
                        heading: 110.78762,
                        color: oldVehicleShipColor,
                        locked: true,
                        engine: false,
                        dimension: localPlayer.dimension,
                        numberPlate: name
                    });
                }
            }
            dealerVehicle.freezePosition(true);
            dealerVehicle.setColours(parseInt(oldVehicleShipColor), parseInt(oldVehicleShipColor));
            if (bizzid != 29 && bizzid != 31 && bizzid != 32) {
                defCamera = new Camera('defCamera', new mp.Vector3(226.7165, -994.4755, -96.0680), new mp.Vector3(226.8619, -994.4674, -96.1345));
            } else if (bizzid == 29) {
                defCamera = new Camera('defCamera', new mp.Vector3(640.319, -2674.2814, 10.835), new mp.Vector3(655.983, -2671.5249, 8.3476));
            } else if (bizzid == 31) {
                defCamera = new Camera('defCamera', new mp.Vector3(-737.513, -1351.9404, 6.1740), new mp.Vector3(-737.115, -1368.139, 0.5914));
            } else if (bizzid == 32) {
                defCamera = new Camera('defCamera', new mp.Vector3(-1003.160, -2958.1293, 24.498), new mp.Vector3(-979.30511, -3001.514, 12.945));
            }
        } else {
            if (defCamera != null) {
                defCamera.delete();
                defCamera = null;
            }
            if (dealerVehicle != null) {
                dealerVehicle.destroy()
                dealerVehicle = null;
            }
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            mp.gui.cursor.show(false, false);
        }
    }
});

//CenterMenu
mp.events.add("Client:ShowCenterMenu", (rules, json, header) => {
    if (hudWindow != null) {
        mp.events.call('Client:UpdateHud3');
        showCenterMenu = !showCenterMenu;
        centerHeader = header;
        if (showCenterMenu == true) {
            hudWindow.execute(`gui.menu.showCenterMenu('${rules}','${json}','${header}');`)
            mp.game.ui.displayHud(false);
            showHideChat(false);
            enableDisableRadar(false);
            mp.gui.cursor.show(true, true);
        } else {
            hudWindow.execute(`gui.menu.showCenterMenu('n/A','n/A','n/A');`)
            mp.game.ui.displayHud(true);
            showHideChat(true);
            enableDisableRadar(true);
            mp.gui.cursor.show(false, false);
            if (centerHeader == "Führungszeugnis" || centerHeader == "Fahrzeugdiagnose" || centerHeader == "Untersuchung") {
                mp.events.callRemote('Server:StopAnimation');
            }
        }
    }
});

//Weaponsystem
mp.events.add("Client:SelectGun", (itemid) => {
    for (i = 0; i < inventory.length; i++) {
        if (inventory[i].type == 5 && inventory[i].itemid == itemid) {
            let status = inventory[i].props.split(',')[1];
            let weaponstatus = status == 0 ? checkWeaponClass(inventory[i].props.split(',')[2]) : 0;
            if (countWeapons() >= 4 && status == 0) {
                if (hudWindow != null) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du kannst keine weiteren Waffen mehr ausrüsten!','error','top-left',2500);`)
                }
                return;
            }
            if (weaponstatus == 0) {
                mp.events.callRemote('Server:SelectGun', itemid, status, false);
            } else {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du hast schon eine Waffe dieser Kategorie ausgerüstet!','error','top-left',2500);`)
            }
            return;
        }
    }
});

mp.events.add("Client:WeaponPreview", (index, component) => {
    if (localPlayer.weapon) {
        if (index == 1) {
            //Add/remove weapon tint
            mp.game.invoke("0x50969B9B89ED5738", localPlayer.handle, localPlayer.weapon >> 0, component >> 0);
        } else {
            if (index == 2) {
                //Add weapon component
                mp.game.invoke("0xD966D51AA5B28BB9", localPlayer.handle, localPlayer.weapon >> 0, mp.game.joaat(component) >> 0);
            } else {
                //Remove weapon component
                mp.game.invoke("0x1E8BE90C74FB4C09", localPlayer.handle, localPlayer.weapon >> 0, mp.game.joaat(component) >> 0);
            }
        }
    }
});

mp.events.add("Client:RemoveWeaponComponent", (component) => {
    //Remove weapon component
    mp.game.invoke("0x1E8BE90C74FB4C09", localPlayer.handle, localPlayer.weapon >> 0, mp.game.joaat(component) >> 0);
});

mp.events.add("Client:ResetWeaponComp", (index) => {
    if (index == 1) {
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Der Waffenfarbton wurde resetet!','success','top-left',2500);`)
        mp.game.invoke("0x50969B9B89ED5738", localPlayer.handle, localPlayer.weapon >> 0, parseInt(oldTint) >> 0);
    } else {
        hudWindow.execute(`gui.hud.resetComponents('${1}')`);
        hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Die Waffenkomponenten wurden resetet!','success','top-left',2500);`)
    }
});

mp.events.add("Client:WeaponSet", (index, component, choose) => {
    if (localPlayer.weapon) {
        if (index == 1) {
            if (parseInt(component) == oldTint) {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Deine Waffe hat diesen Farbton bereits!','error','top-left',2500)`)
                return;
            }
        }
        mp.events.callRemote('Server:WeaponSet', index, component, choose);
    }
});

mp.events.add("Client:UpdateWeaponComponent", (index, component, csv) => {
    if (localPlayer.weapon) {
        if (index == 1) {
            oldTint = parseInt(component);
            if (!oldTint) {
                oldTint = 0;
            }
            //Weapon tint
            mp.game.invoke("0x50969B9B89ED5738", localPlayer.handle, localPlayer.weapon >> 0, parseInt(component) >> 0);
        } else {
            if (index == 2) {
                //Add weapon component
                mp.game.invoke("0xD966D51AA5B28BB9", localPlayer.handle, localPlayer.weapon >> 0, mp.game.joaat(component) >> 0);
                hudWindow.execute(`gui.hud.updateWeaponComponents('${csv}');`)
            } else {
                hudWindow.execute(`gui.hud.updateWeaponComponents('${csv}');`)
                hudWindow.execute(`gui.hud.resetComponents('${1}')`);
            }
        }
    }
});

mp.events.addDataHandler("Player:WeaponTint", (entity, value) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    if (mp.players.exists(entity) && 0 !== entity.handle && entity.type === "player" && entity.dimension == localPlayer.dimension) {
        var hastint = entity.hasVariable("Player:WeaponTint");
        if (hastint) {
            var tint = entity.getVariable("Player:WeaponTint");
            if (parseInt(tint) > -1) {
                mp.game.invoke("0x50969B9B89ED5738", entity.handle, entity.weapon >> 0, parseInt(tint) >> 0);
            }
        }
    }
});

mp.events.addDataHandler("Player:WeaponComponents", (entity, value) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (!spawned) return;
    if (mp.players.exists(entity) && 0 !== entity.handle && entity.type === "player" && entity.dimension == localPlayer.dimension) {
        var components = value;
        var componentsArray = [];
        componentsArray = components.split('|');
        if (listWeaponComponents[entity.remoteId] && listWeaponComponents[entity.remoteId].length > 3) {
            var listArray = [];
            listArray = listWeaponComponents[entity.remoteId].split('|');
            for (var i = 0; i < listArray.length; i++) {
                if (listArray[i] && listArray[i].length > 3 && (componentsArray[i] != listArray[i] || components == "|" || !componentsArray[i])) {
                    mp.game.invoke("0x1E8BE90C74FB4C09", entity.handle, entity.weapon >> 0, mp.game.joaat(listArray[i]) >> 0);
                }
            }
        }
        listWeaponComponents[entity.remoteId] = components;

        if (components != "|") {
            for (var i = 0; i < componentsArray.length; i++) {
                if (componentsArray[i].length > 3) {
                    mp.game.invoke("0xD966D51AA5B28BB9", entity.handle, entity.weapon >> 0, mp.game.joaat(componentsArray[i]) >> 0);
                }
            }
        }
    }
});

mp.events.add("Client:ShowAmmunationMenu", (item, price1, price2, weapon, check) => {
    if (hudWindow == null) return;
    let currentTint = 0;
    if (check == 1) {
        showAmmuPolice = !showAmmuPolice;
    }
    showAmmu = !showAmmu;
    showCursorTemp = !showCursorTemp;
    if (showAmmu == true) {
        mp.events.call('Client:ShowShop2', 'n/A', 'n/A', 'n/A', 0, 0, 0, false);
        currentTint = mp.game.invoke("0x2B9EEDC07BD06B9F", localPlayer.handle, localPlayer.weapon >> 0);
        if (!currentTint) {
            currentTint = 0;
        }
        oldTint = currentTint;
        if (!oldTint) {
            oldTint = 0;
        }
    }
    hudWindow.execute(`gui.hud.showAmmunation('${item}','${price1}','${price2}','${currentTint}','${weapon}','${check}');`)
});

mp.events.add("Client:HideAmmu", (set) => {
    if (hudWindow == null) return;
    showAmmu = false;
    showCursorTemp = false;
    hudWindow.execute(`gui.hud.showAmmunation('n/A','0','0','0','n/A');`);
    mp.game.invoke("0x50969B9B89ED5738", localPlayer.handle, localPlayer.weapon >> 0, oldTint >> 0);
    hudWindow.execute(`gui.hud.resetComponents('${1}')`);
    if (set == 1) {
        mp.events.callRemote('Server:ShowPreShop', 'Ammunation', 0, 1, 0);
    }
});

mp.events.add("Client:HideAmmuPolice", (set) => {
    if (hudWindow == null) return;
    showAmmu = false;
    showAmmuPolice = false;
    showCursorTemp = false;
    hudWindow.execute(`gui.hud.showAmmunation('n/A','0','0','0','n/A');`);
    mp.game.invoke("0x50969B9B89ED5738", localPlayer.handle, localPlayer.weapon >> 0, oldTint >> 0);
    hudWindow.execute(`gui.hud.resetComponents('${1}')`);
    if (set == 1) {
        mp.events.callRemote('Server:ShowPreShop', 'Waffenkammer LSPD', 0, 1, 0);
    }
});

//Musicsystem
mp.events.add("Client:ShowMusicStation", (status) => {
    if (hudWindow == null) return;
    mp.events.call('Client:UpdateHud3');
    showMusic = !showMusic;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showmusicmenu('${status}');`)
    mp.gui.cursor.show(showMusic, showMusic);
    if (showMusic == true) {
        showHideChat(false);
    } else {
        showHideChat(true);
    }
});

//Radiomenu
mp.events.add("Client:ShowRadioSystem", (freqz) => {
    if (hudWindow == null) return;
    mp.events.call('Client:UpdateHud3');
    showRadio = !showRadio;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showradiomenu('${freqz}');`)
    mp.gui.cursor.show(showRadio, showRadio);
    if (showRadio == true) {
        showHideChat(false);
    } else {
        showHideChat(true);
    }
});

mp.events.add("Client:SetRadioFreq", (freq) => {
    if (hudWindow == null) return;
    if (freq == 'Aus') {
        mp.events.call('Client:ShowRadioSystem', '');
    } else {
        mp.events.callRemote('Server:SetRadioFreq', freq);
    }
});

mp.events.add("Client:Joinradio", (freq) => {
    if (hudWindow != null) {
        mp.events.callRemote('SaltyChat_Joinradio', freq);
    }
})

mp.events.add("Client:Leaveradio", (freq) => {
    if (hudWindow != null) {
        mp.events.callRemote('SaltyChat_Removeradio', freq);
    }
})

mp.events.add("Client:Setspeaker", (status) => {
    if (hudWindow != null) {
        mp.events.callRemote('SaltyChat_Setspeaker', status);
    }
})

mp.events.add("Client:SendOnRadio", (radioChannel, status) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showCarSetting == true || showCityhall == true || showSped == true || showFuel == true || showShop == true || showShop2 == true || startRange == true || showDealer == true || showTab == true || showHandy == true || showTuning == true || barberMenu == true || tattooShop == true) return;
    mp.events.callRemote('SaltyChat_IsSending', radioChannel, status);
    if (status && animationSet == 0) {
        animationSet = 1;
        mp.events.callRemote('Server:PlayAnimation', 'radio2', true);
    } else {
        if (animationSet == 0) return;
        setTimeout(() => {
            animationSet = 0;
            mp.events.callRemote('Server:StopAnimationSync');
        }, 75);
    }
})

//Arrest
mp.events.add("Client:ShowArrest", (name) => {
    if (hudWindow == null) return;
    mp.events.call('Client:UpdateHud3');
    showArrest = !showArrest;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showarrestmenu('${name}');`)
    mp.gui.cursor.show(showArrest, showArrest);
    if (showArrest == true) {
        showHideChat(false);
    } else {
        showHideChat(true);
    }
});

//Ticket
mp.events.add("Client:ShowTicket", (name) => {
    if (hudWindow == null) return;
    mp.events.call('Client:UpdateHud3');
    showTicket = !showTicket;
    nokeys = !nokeys;
    localPlayer.freezePosition(showTicket);
    hudWindow.execute(`gui.hud.showticketmenu('${name}');`)
    mp.gui.cursor.show(showTicket, showTicket);
    if (showTicket == true) {
        showHideChat(false);
    } else {
        showHideChat(true);
    }
});

//Recept
mp.events.add("Client:ShowRezept", (name) => {
    if (hudWindow == null) return;
    mp.events.call('Client:UpdateHud3');
    showRecept = !showRecept;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showreceptmenu('${name}');`)
    mp.gui.cursor.show(showRecept, showRecept);
    if (showRecept == true) {
        showHideChat(false);
    } else {
        showHideChat(true);
    }
});


//Personalausweis
mp.events.add("Client:ShowPerso", (data) => {
    if (hudWindow == null || nokeys == true || death == true || cuffed == true) return;
    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du schaust dir einen Personalausweis an!','success','top-left',1500);`)
    mp.events.call('Client:UpdateHud3');
    showPerso = !showPerso;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showPerso('${data}');`)
});

//Lizenzen
mp.events.add("Client:ShowLics", (data, data2) => {
    if (hudWindow == null || nokeys || death == true || cuffed == true) return;
    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Du schaust dir Lizenzen an!','success','top-left',1500);`)
    mp.events.call('Client:UpdateHud3');
    showLics = !showLics;
    nokeys = !nokeys;
    hudWindow.execute(`gui.hud.showLics('${data}','${data2}');`)
});

//ShowPreShop
mp.events.add("Client:ShowPreShop", (shopname) => {
    if (hudWindow == null) return;
    mp.events.callRemote('Server:ShowPreShop', shopname, 0, 1, 0);
});

//Blockplayer
mp.events.add("Client:BlockPlayer", (id) => {
    if (id == true) {
        triggerAntiCheat = true;
        hideMenus();
    } else {
        triggerAntiCheat = false;
    }
});

//HideMenus
mp.events.add("Client:HideMenus", () => {
    hideMenus();
});

//Animals
mp.events.addDataHandler("Ped:Death", (entity, value, oldValue) => {
    if (mp.peds.exists(entity) && 0 !== entity.handle && entity.type == 'ped') {
        let pedDeath = value;
        if (pedDeath == 1 && getPlayerHealth(entity) > 0) {
            entity.setHealth(0);
            setTimeout(() => {
                entity.freezePosition(true);
            }, 455);
        }
    }
});

mp.events.add("Client:UpdateAnimals", (ped) => {
    if (ped && ped.typ == 'ped') {
        ped.setAsEnemy(true);
        ped.freezePosition(false);
        ped.setCanBeDamaged(true);
        ped.setInvincible(true);
        ped.setHealth(375);
        ped.setOnlyDamagedByPlayer(true);
        ped.setCombatAbility(100);
        ped.setCombatRange(1);
        ped.setCombatMovement(2);
        ped.setCombatAttributes(46, true);
        ped.setCombatAttributes(17, true);
        ped.setCombatAttributes(5, true);
        ped.setFleeAttributes(0.0, false);
        ped.setProofs(false, false, false, false, false, false, false, false);
        ped.taskWanderInArea(1714.7175, -571.90814, 144.50644, 95, 0, 1.25);
        ped.setKeepTask(true);
    }
});

//Chat
function showHideChat(setChat) {
    if (parseInt(checkChat) == 0) {
        mp.gui.chat.show(false);
        return;
    }
    mp.gui.chat.show(setChat);
}

//Weaponsystem
function updateWeapons() {
    if (hudWindow == null) return;
    let ammo = localPlayer.weaponAmmo;
    let actualWeapon = getActualWeapon();
    let actualWeaponString;
    if (actualWeapon.description) {
        actualWeaponString = actualWeapon.description.toLowerCase()
    } else {
        actualWeaponString = 'faust'
    }
    hudWindow.execute(`gui.speedometer.updateWeaponList('${JSON.stringify(inventory)}','${actualWeaponString}','${ammo}');`)
    return;
}

function countWeapons() {
    let count = 0;
    if (inventory) {
        for (i = 0; i < inventory.length; i++) {
            if (inventory[i] && inventory[i].type == 5) {
                let status = inventory[i].props.split(',')[1];
                if (parseInt(status) == 1) {
                    count++
                }
            }
        }
    }
    return count;
}

function checkForZeroWeapons() {
    if (hudWindow == null) return;
    for (i = 0; i < inventory.length; i++) {
        if ((antiCheatWait == 0 || (Date.now() / 1000) > antiCheatWait) && inventory[i].type == 5 && (inventory[i].description.toLowerCase() == "granate" || inventory[i].description.toLowerCase() == "rauchgranate" || inventory[i].description.toLowerCase() == "bzgas" || inventory[i].description.toLowerCase() == "molotowcocktail" || inventory[i].description.toLowerCase() == "snowball" || inventory[i].description.toLowerCase().includes("schutzweste"))) {
            mp.events.callRemote('Server:CheckForEmptyWeapon');
            return;
        }
    }
    return;
}

function checkWeaponClass(getclass) {
    for (i = 0; i < inventory.length; i++) {
        if (inventory[i].type == 5 && inventory[i].props.split(',')[1] == 1 && inventory[i].props.split(',')[2] == getclass) {
            return -1;
        };
    }
    return 0;
}

function switchWeapon(check) {
    let spawned = localPlayer.getVariable('Player:Spawned');
    let adminSettings = localPlayer.getVariable('Player:Adminsettings');
    if (showSaltyError == true || triggerAntiCheat == true || localPlayer.isTypingInTextChat || !spawned || nokeys == true || death == true || arrested == true || cuffed == true || showMenu == true || showInventory == true || showWheel == true || showFurniture == true || InteriorSwitch == true || prisonCount > 0 || showCenterMenu == true || showCarSetting == true || showSped == true || showCityhall == true || showFuel == true || showBank == true || showHandy == true || showAmmu == true || showShop == true || showShop2 == true || showDealer == true || showTab == true || startRange == true || adminSettings.split(',')[1] == '1' || barberMenu == true || tattooShop == true) return;
    let isPauseActive = mp.game.ui.isPauseMenuActive();
    let playerIsAiming = localPlayer.isAiming;
    var aiming = mp.game.player.isFreeAiming();
    if (!isPauseActive && !playerIsAiming && !aiming && !mp.gui.cursor.visible) {
        if (inventory) {
            var count = 0;
            if (check == -1) {
                oldCheck--;
                if (oldCheck <= 0) {
                    oldCheck = 7331;
                } else if ((oldCheck >= 10 && oldCheck <= 7331)) {
                    oldCheck = maxWeapons;
                }
                check = oldCheck;
            } else if (check == -2) {
                oldCheck++;
                if (oldCheck > maxWeapons && oldCheck < 7331) {
                    oldCheck = 7331;
                } else if (oldCheck >= 7331) {
                    oldCheck = 1;
                }
                check = oldCheck;
            }
            if (check == 1337) {
                localPlayer.weapon = mp.game.joaat('weapon_unarmed');
                return;
            } else {
                if (check == 7331) {
                    localPlayer.weapon = mp.game.joaat('weapon_unarmed');
                    return;
                }
                for (i = 0; i < inventory.length; i++) {
                    if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("schutzweste") && inventory[i].props.split(',')[1] == 1) {
                        count++;
                        if (count == check) {
                            localPlayer.weapon = mp.game.joaat(getWeaponByHash(inventory[i].description));
                            return;
                        }
                    };
                }
            }
        }
    }
}

function getActualWeapon() {
    for (i = 0; i < inventory.length; i++) {
        if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("schutzweste") && inventory[i].props.split(',')[1] == 1) {
            if (localPlayer.weapon == mp.game.joaat(getWeaponByHash(inventory[i].description))) {
                return inventory[i];
            }
        };
    }
    return "Faust";
}

function getWeaponByHash(itemname) {
    switch (itemname.toLowerCase()) {
        case "pistole": {
            return "weapon_pistol";
        }
        case "pistole-mk2": {
            return "weapon_pistol_mk2";
        }
        case "pistole-50": {
            return "weapon_pistol50";
        }
        case "combat-pistole": {
            return "weapon_combatpistol";
        }
        case "heavy-pistole": {
            return "weapon_heavypistol";
        }
        case "keramik-pistole": {
            return "weapon_ceramicpistol";
        }
        case "flaregun": {
            return "weapon_flaregun";
        }
        case "revolver": {
            return "weapon_revolver";
        }
        case "revolver-mk2": {
            return "weapon_revolver_mk2";
        }
        case "sns-pistole": {
            return "weapon_snspistol";
        }
        case "sns-pistole-mk2": {
            return "weapon_snspistol_mk2";
        }
        case "taser": {
            return "weapon_stungun";
        }
        case "dolch": {
            return "weapon_dagger";
        }
        case "baseballschläger": {
            return "weapon_bat";
        }
        case "brechstange": {
            return "weapon_crowbar";
        }
        case "taschenlampe": {
            return "weapon_flashlight";
        }
        case "golfschläger": {
            return "weapon_golfclub";
        }
        case "axt": {
            return "weapon_hatchet";
        }
        case "schlagring": {
            return "weapon_knuckle";
        }
        case "messer": {
            return "weapon_knife";
        }
        case "machete": {
            return "weapon_machete";
        }
        case "klappmesser": {
            return "weapon_switchblade";
        }
        case "schlagstock": {
            return "weapon_nightstick";
        }
        case "poolcue": {
            return "weapon_poolcue";
        }
        case "micro-smg": {
            return "weapon_microsmg";
        }
        case "mp5": {
            return "weapon_smg";
        }
        case "mp5-mk2": {
            return "weapon_smg_mk2";
        }
        case "assault-smg": {
            return "weapon_assaultsmg";
        }
        case "combat-pdw": {
            return "weapon_combatpdw";
        }
        case "tec9": {
            return "weapon_machinepistol";
        }
        case "mini-smg": {
            return "weapon_minismg";
        }
        case "pumpshotgun": {
            return "weapon_pumpshotgun";
        }
        case "pumpshotgun-mk2": {
            return "weapon_pumpshotgun_mk2";
        }
        case "sawnoffshotgun": {
            return "weapon_sawnoffshotgun4";
        }
        case "combatshotgun": {
            return "weapon_combatshotgun";
        }
        case "musket": {
            return "weapon_musket";
        }
        case "angriffsgewehr": {
            return "weapon_assaultrifle";
        }
        case "angriffsgewehr-mk2": {
            return "weapon_assaultrifle_mk2";
        }
        case "karabiner-gewehr": {
            return "weapon_carbinerifle";
        }
        case "karabiner-gewehr-mk2": {
            return "weapon_carbinerifle_mk2";
        }
        case "spezial-karabiner": {
            return "weapon_specialcarbine";
        }
        case "spezial-karabiner-mk2": {
            return "weapon_specialcarbine_mk2";
        }
        case "m16": {
            return "weapon_tacticalrifle";
        }
        case "kompaktgewehr": {
            return "weapon_compactrifle";
        }
        case "maschinengewehr": {
            return "weapon_mg";
        }
        case "gusenberg": {
            return "weapon_gusenberg";
        }
        case "sniper": {
            return "weapon_sniperrifle";
        }
        case "rpg": {
            return "weapon_rpg";
        }
        case "granate": {
            return "weapon_grenade";
        }
        case "snowball": {
            return "weapon_snowball";
        }
        case "bzgas": {
            return "weapon_bzgas";
        }
        case "molotowcocktail": {
            return "weapon_molotov";
        }
        case "rauchgranate": {
            return "weapon_smokegrenade";
        }
        case "feuerlöscher": {
            return "weapon_fireextinguisher";
        }
    }
};

//Functions
function getClosestVehicle(position) {
    try {
        let closest = 5.5;
        let closestVeh = null;

        mp.vehicles.forEachInStreamRange(v => {
            let dist = mp.game.system.vdist(position.x, position.y, position.z, v.position.x, v.position.y, v.position.z);

            if (dist < closest) {
                closest = dist;
                closestVeh = v;
            }
        });

        return {
            distance: closest,
            vehicle: closestVeh
        };
    } catch { }
}

function getClosestPed(position) {
    try {
        let closest = 6.15;
        let closestPed = null;

        mp.peds.forEachInStreamRange(p => {
            let posi = p.getCoords(true);
            let dist = mp.game.system.vdist(position.x, position.y, position.z, posi.x, posi.y, posi.z);

            if (dist < closest) {
                closest = dist;
                closestPed = p;
            }
        });

        return {
            distance: closest,
            ped: closestPed
        };
    } catch { }
}

function setWalkingStyle(player, style) {
    if (!style || style.length < 5) {
        player.resetMovementClipset(0.0);
    } else {
        if (!mp.game.streaming.hasClipSetLoaded(style)) {
            mp.game.streaming.requestClipSet(style);
            while (!mp.game.streaming.hasClipSetLoaded(style)) mp.game.wait(0);
        }

        player.setMovementClipset(style, 0.0);
    }
}

function getPlayerByRemoteId(remoteId) {
    let pla = mp.players.atRemoteId(remoteId);
    if (pla == undefined || pla == null) {
        return null;
    }
    return pla;
}

function containsObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }
    return false;
}

function containsAttachment(name, entity) {
    let check = false;
    entityAttachments.forEach(function (item, index, array) {
        if (item && item.entityid == entity.remoteId) {
            if (item.name == name) {
                check = true;
                return check;
            }
        }
    });
    return check;
}

function getRndInteger(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

function getPlayerHealth(playerobject)
{
    if(playerobject != null)
    {
        return playerobject.getHealth()+100;
    }
    else
    {
        return 200;
    }
}

function updateHealthArmor() {
    if (hudWindow == null) return;
    var model = localPlayer.model == mp.game.joaat('mp_f_freemode_01') ? 1 : localPlayer.model == mp.game.joaat('mp_m_freemode_01') ? 1 : 0;
    if (oldHealth > -1 && oldHealth != getPlayerHealth(localPlayer)) {
        if (!death && (antiCheatWait == 0 || (Date.now() / 1000) > antiCheatWait) && model == 1 && oldHealth > 0 && getPlayerHealth(localPlayer) > 0 && getPlayerHealth(localPlayer) > oldHealth && (getPlayerHealth(localPlayer)-100) != 100 && getPlayerHealth(localPlayer) <= 275) {
            callAntiCheat("Lebens Cheat", "Lebenswert: " + parseInt(getPlayerHealth(localPlayer)-100), false);
            antiCheatWait = (Date.now() / 1000) + (3);
            localPlayer.setHealth(200);
            oldHealth = 200;
            return;
        }
        oldHealth = getPlayerHealth(localPlayer);
        hudWindow.execute(`gui.hud.updateBar(1, '${oldHealth-100}');`)
    }
    if (oldArmor > -1 && oldArmor != localPlayer.getArmour()) {
        if (!death && (antiCheatWait == 0 || (Date.now() / 1000) > antiCheatWait) && model == 1 && oldArmor > 0 && localPlayer.getArmour() > 0 && (oldArmor >= 100 || localPlayer.getArmour() > oldArmor)) {
            callAntiCheat("Schutzwesten Cheat", "Rüstungswert: " + localPlayer.getArmour(), false);
            return;
        }
        oldArmor = localPlayer.getArmour();
        hudWindow.execute(`gui.hud.updateBar(2, '${oldArmor}');`)
    }
}

function antiCheatCheck() {
    antiCheatTime++;
    //Money Anticheat
    if (localPlayer.getMoney() > 0) {
        callAntiCheat("Geld Cheat", "Geldwert: " + localPlayer.getMoney() + "$", false);
        localPlayer.setMoney(0);
    }
    //Waffen Anticheat
    let weaponFound = false;
    if (inventory && wait == false && localPlayer.weapon && localPlayer.weapon != mp.game.joaat('weapon_unarmed') && localPlayer.weapon != mp.game.joaat('weapon_snowball') && !startRange) {
        for (i = 0; i < inventory.length; i++) {
            if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("snowball") && !inventory[i].description.toLowerCase().includes("schutzweste") && inventory[i].props.split(',')[1] == 1) {
                if (mp.game.joaat(getWeaponByHash(inventory[i].description)) == localPlayer.weapon) {
                    weaponFound = true;
                }
            }
        }
        if (weaponFound == false && String(localPlayer.weapon) != '966099553') {
            callAntiCheat("Waffen Cheat", String(localPlayer.weapon), true);
        }
    }
    //Munitions Anticheat
    if (inventory && wait == false && !startRange) {
        for (i = 0; i < inventory.length; i++) {
            if (inventory[i].type == 5 && !inventory[i].description.toLowerCase().includes("schutzweste") && !inventory[i].description.toLowerCase().includes("snowball") && !inventory[i].description.toLowerCase().includes("feuerlöscher") && inventory[i].props.split(',')[1] == 1) {
                ammo = localPlayer.getWeaponAmmo(mp.game.joaat(getWeaponByHash(inventory[i].description)));
                if (ammo > parseInt(inventory[i].props.split(',')[0])) {
                    callAntiCheat("Munitions Cheat", "n/A", true);
                } else {
                    inventory[i].misc = ammo;
                }
            }
        }
    }
    let funmodus = localPlayer.getVariable('Player:Funmodus');
    let devmodus = localPlayer.getVariable('Player:DevModus');
    //Speedhack
    if (mp.players.local.vehicle && !funmodus && !devmodus && ping == false && !localPlayer.isFalling()) {
        if (speedTime == 0 || (speedTime != 0 && Date.now() / 1000 > speedTime)) {
            speedTime = Date.now() / 1000 + (35);
            speedTrys = 0;
        }
        let speed = localPlayer.vehicle.getSpeed() * 3.6;
        if (speed >= 575) {
            speedTrys++;
            if (speedTrys >= 2) {
                callAntiCheat("Speedhack", "Geschwindigkeit: " + speed + " KM/H", true);
            }
        }
    }
    //Teleport Anticheat
    if (wait2 == 0 && !localPlayer.isFalling() && !localPlayer.isRagdoll() && !funmodus && !devmodus && ping == false && !specTarget) {
        if (oldposition != null) {
            distance = distanceVector(oldposition, localPlayer.position);
            if (!mp.players.local.vehicle) {
                if (distance >= 39) {
                    if (flyTime == 0 || (flyTime != 0 && Date.now() / 1000 > flyTime)) {
                        flyTime = Date.now() / 1000 + (35);
                        flyTrys = 0;
                    }
                    flyTrys++;
                    if (flyTrys >= 3) {
                        callAntiCheat("Teleport/Flyhack Cheat", "Distanz: " + distance.toFixed(2) + "m", true);
                    }
                }
            } else {
                if (distance >= 39 && parseInt(localPlayer.vehicle.getClass()) != 15 && parseInt(localPlayer.vehicle.getClass()) != 16 && checkCarPos(25)) {
                    if (flyTime == 0 || (flyTime != 0 && Date.now() / 1000 > flyTime)) {
                        flyTime = Date.now() / 1000 + (35);
                        flyTrys = 0;
                    }
                    flyTrys++;
                    if (flyTrys >= 3) {
                        callAntiCheat("Teleport/Flyhack Cheat", "Distanz: " + distance.toFixed(2) + "m", true);
                    }
                }
            }
        }
    }
    //Alle 2 Minuten
    if (antiCheatTime >= 120) {
        antiCheatTime = 0;
    }
    oldposition = localPlayer.position;
}

//Anticheat
function callAntiCheat(cheatname, props, screenshot) {
    if (calledAntiCheat == 0 || (Date.now() / 1000) > calledAntiCheat) {
        mp.events.callRemote('Server:CallAntiCheat', cheatname, props, screenshot);
        calledAntiCheat = (Date.now() / 1000) + (5);
    }
}

function checkCarPos(maxHeight = 50) {
    if (localPlayer.vehicle) {
        if (parseInt(localPlayer.vehicle.getClass()) != 15 && parseInt(localPlayer.vehicle.getClass()) != 16) {
            this.range_to_btm = mp.game.gameplay.getGroundZFor3dCoord(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z, parseFloat(0), false);
            if (localPlayer.position.z - this.range_to_btm > maxHeight + this.range_to_btm) {
                return true
            }
            return false
        }
    }
}

//Disabled Controles pressed
function disabledControlPressed() {
    //Weaponswitch
    if (mp.game.controls.isDisabledControlPressed(2, 14)) {
        switchWeapon(-1);
    }

    if (mp.game.controls.isDisabledControlPressed(2, 15)) {
        switchWeapon(-2);
    }
}

//Attachments
function RegisterAttachment(name, prop, boneindex, position, rotation) {
    registredAttachments.push({
        "name": name,
        "prop": prop,
        "boneindex": boneindex,
        "position": position,
        "rotation": rotation
    });
}

mp.events.addDataHandler("Player:Attachments", (entity, value) => {
    let spawned = localPlayer.getVariable('Player:Spawned');
    var attachment = null;
    if (!spawned) return;
    if (mp.players.exists(entity) && 0 !== entity.handle && entity.type === "player" && entity.dimension == localPlayer.dimension) {
        let newAttachments = null;
        if (value.length > 1) {
            newAttachments = value.split(',');
        }
        //Old Attachments
        var tempObj = null;
        if (entityAttachments) {
            entityAttachments.forEach(function (item, index, array) {
                if (item && value && item.entityid == entity.remoteId) {
                    if (value.length <= 1 || !newAttachments || (newAttachments && !newAttachments.includes(item.name))) {
                        if (item.object && item.delete == false) {
                            tempObj = item;
                            setTimeout(() => {
                                if (tempObj.object) {
                                    tempObj.object.detach(false, false);
                                    tempObj.object.destroy();
                                    tempObj.delete = true;
                                }
                            }, 20);
                        }
                    }
                }
            });
        }
        entityAttachments = entityAttachments.filter(am => am.delete == false);
        //New Attachments
        if (newAttachments) {
            for (let i = 0; i < newAttachments.length; i++) {
                if (newAttachments[i] && !containsAttachment(newAttachments[i], entity) && newAttachments[i] != 'n/A') {
                    attachment = GetAttachmentByName(newAttachments[i]);
                    if (!attachment) continue;

                    mp.game.streaming.requestModel(mp.game.joaat(attachment.prop));

                    const object = {
                        name: attachment.name,
                        entityid: entity.remoteId,
                        object: createObject(attachment.prop, new mp.Vector3(entity.position.x, entity.position.y, entity.position.z - 12.5, localPlayer.dimension), new mp.Vector3(0.0, 0.0, 0.0)),
                        delete: false
                    }

                    object.object.setCollision(false, false);
                    setTimeout(function () {

                        object.object.attachTo(entity.handle,
                            entity.getBoneIndex(parseInt(attachment.boneindex)),
                            attachment.position.x, attachment.position.y, attachment.position.z,
                            attachment.rotation.x, attachment.rotation.y, attachment.rotation.z,
                            true, false, false, false, 2, true);

                        if (entity.remoteId == localPlayer.remoteId && attachment.name.includes('Ciga')) {
                            setCiga = true;
                        }

                        if (entity.remoteId == localPlayer.remoteId && attachment.name.includes('Joint')) {
                            setJoint = true;
                            mp.events.call("Client:DrugEvent");
                        }

                        entityAttachments.push(object);
                    }, 125);
                }
            }
        }
    }
});

//GetAttachmentByName
function GetAttachmentByName(name) {
    var retItem = null;
    registredAttachments.forEach(function (item, index, array) {
        if (item.name == name) {
            retItem = item;
            return;
        }
    });
    return retItem;
}

//EnterDrivingSchoolCP
function EnterDrivingSchoolCP() {
    if (drivingSchoolCount < drivingSchoolPositions.length - 1) {
        let speed = localPlayer.vehicle.getSpeed() * 3.6;
        if (speed > 112 && localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() != 'dinghy' && localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() != 'havok') {
            hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Du fährst zu schnell, fahr nochmal zurück und mach das anständig!','info','top-left',4350);`);
            return;
        }
        if (drivingSchoolCount == 1) {
            if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() != 'havok') {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Achte auf deine Geschwindigkeit, du kannst auch den Geschwindigkeitsregler nutzen - /limitspeed!','info','top-left',4350);`);
            } else {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Achte auf deine Flughöhe, die nächsten Punkte befinden sich auf ca. 500m Flughöhe!','info','top-left',4350);`);
            }
        }
        if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'sentinel') {
            if (drivingSchoolCount == drivingSchoolPositions.length - 3) {
                hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Bei der nächsten Markierung, bitte Rückwärts einparken!','info','top-left',4350);`);
            }
            if (drivingSchoolCount == drivingSchoolPositions.length - 2) {
                if ((localPlayer.vehicle.getRotation(5).z < 48 || localPlayer.vehicle.getRotation(5).z > 52)) {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Bitte nochmal, das hat noch nicht ganz gepasst!','info','top-left',4350);`);
                    return;
                } else {
                    hudWindow.execute(`gui.hud.sendNotificationWithoutButton('Manfred: Perfekt eingeparkt!','info','top-left',2350);`);
                }
            }
        }

        if (drivingSchoolHandle != null) {
            drivingSchoolHandle.destroy();
            drivingSchoolHandle = null;
        }

        drivingSchoolCount++;

        if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() != 'havok') {
            drivingSchoolHandle = mp.checkpoints.new(49, new mp.Vector3(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y, drivingSchoolPositions[drivingSchoolCount].z - 0.5), 1.65, {
                color: [255, 0, 0, 255],
                visible: true,
                dimension: 0
            });
        } else {
            drivingSchoolHandle2 = mp.colshapes.newCircle(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y, 15, 0);
        }

        mp.game.ui.setNewWaypoint(drivingSchoolPositions[drivingSchoolCount].x, drivingSchoolPositions[drivingSchoolCount].y);

    } else {
        if (drivingSchoolHandle != null) {
            drivingSchoolHandle.destroy();
            drivingSchoolHandle = null;
        }

        drivingSchoolCount = 0;
        drivingSchoolPositions = [];

        if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'sentinel') {
            mp.events.callRemote('Server:FinishCar', 1)
        } else if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'akuma') {
            mp.events.callRemote('Server:FinishCar', 2)
        } else if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'pounder') {
            mp.events.callRemote('Server:FinishCar', 3)
        } else if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'dinghy') {
            mp.events.callRemote('Server:FinishCar', 4)
        } else if (localPlayer.vehicle.getVariable('Vehicle:Name').toLowerCase() == 'havok') {
            mp.events.callRemote('Server:FinishCar', 5)
        } else {
            mp.events.callRemote('Server:FinishCar', -1)
        }
    }
}

//Radar
function enableDisableRadar(set) {
    let inHouse = parseInt(localPlayer.getVariable('Player:InHouse'));
    if (inHouse > -1) {
        mp.game.ui.displayRadar(false);
        return;
    }
    mp.game.ui.displayRadar(set);
}

//Getrandomint
function getRandomInt(max) {
    return Math.floor(Math.random() * max);
}

function pointingAt(distance) {
    gameplayCam = mp.cameras.new('gameplay');
    direction = gameplayCam.getDirection();
    coords = gameplayCam.getCoord();

    const farAway = new mp.Vector3((direction.x * distance) + (coords.x), (direction.y * distance) + (coords.y), (direction.z * distance) + (coords.z));

    const result = mp.raycasting.testPointToPoint(coords, farAway, [1, 16]);

    if (result === undefined) {
        return null;
    }
    return result;
}

function shuffle(a) {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
    return a;
}

function CheckInputRotation(cam, zoomvalue) {
    rightAxisX = mp.game.controls.getDisabledControlNormal(0, 220);
    rightAxisY = mp.game.controls.getDisabledControlNormal(0, 221);
    rotation = livestreamCam.getRot(2);
    if (rightAxisX != 0.0 || rightAxisY != 0.0) {
        new_z = rotation.z + rightAxisX * -1.0 * (livestreamspeed_ud) * (zoomvalue + 0.1);
        new_x = Math.max(Math.min(20.0, rotation.x + rightAxisY * -1.0 * (livestreamspeed_lr) * (zoomvalue + 0.1)), -89.5);
        livestreamCam.setRot(new_x, 0.0, new_z, 2);
    }
}

const getClosestBone = (raycast) => {
    let data = [];
    bones.forEach((bone, index) => {
        const boneIndex = raycast.entity.getBoneIndexByName(bone);
        const bonePos = raycast.entity.getWorldPositionOfBone(boneIndex);
        if (bonePos) {
            data.push({
                id: index,
                boneIndex: boneIndex,
                name: bone,
                bonePos: bonePos,
                locked: !raycast.entity.doors[index] || !raycast.entity.doors[index] && !raycast.entity.isDoorFullyOpen(index) ? false : true,
                raycast: raycast,
                veh: raycast.entity,
                distance: mp.game.gameplay.getDistanceBetweenCoords(bonePos.x, bonePos.y, bonePos.z, raycast.position.x, raycast.position.y, raycast.position.z, false),
                pushTime: Date.now() / 1000
            });
        }
    })

    return data.sort((a, b) => a.distance - b.distance)[0];
}

const getLocalTargetVehicle = (range = 5.0) => {
    let startPosition = mp.players.local.getBoneCoords(12844, 0.5, 0, 0);
    const res = mp.game.graphics.getScreenActiveResolution(1, 1);
    const secondPoint = mp.game.graphics.screen2dToWorld3d([res.x / 2, res.y / 2, (2 | 4 | 8)]);
    if (!secondPoint) return null;

    startPosition.z -= 0.3;
    const target = mp.raycasting.testPointToPoint(startPosition, secondPoint, mp.players.local, (2 | 4 | 8 | 16));

    if (target && target.entity.type === 'vehicle' && mp.game.gameplay.getDistanceBetweenCoords(target.entity.position.x, target.entity.position.y, target.entity.position.z, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, false) < range) return target;
    return null;
}

const drawTarget3d = (pos, textureDict = "mpmissmarkers256", textureName = "corona_shade", scaleX = 0.005, scaleY = 0.01) => {
    const position = mp.game.graphics.world3dToScreen2d(pos);
    if (!position) return;
    mp.game.graphics.drawSprite(textureDict, textureName, position.x, position.y, scaleX, scaleY, 0, 0, 0, 0, 200);
}