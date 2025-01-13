import { enUS, ptBR } from "@mui/material/locale";
import { enUS as enUSFNS, ptBR as ptBRFNS } from "date-fns/locale";

export const allLangs = [
  {
    label: "Português",
    value: "br",
    systemValue: ptBR,
    datefnsValue: ptBRFNS,
    icon: "/assets/icons/flags/ic_flag_br.svg",
  },
  {
    label: "English",
    value: "en",
    systemValue: enUS,
    datefnsValue: enUSFNS,
    icon: "/assets/icons/flags/ic_flag_en.svg",
  },
];

export const defaultLang = allLangs[0];

export const firebaseConfig = {
  apiKey: "AIzaSyDYMZEHOh2TRUgA9uB-eh94ADRf4JlqHPY",
  authDomain: "emissor-digital-de-os-ed4b2.firebaseapp.com",
  projectId: "emissor-digital-de-os-ed4b2",
  storageBucket: "emissor-digital-de-os-ed4b2.appspot.com",
  messagingSenderId: "307689100498",
  appId: "1:307689100498:web:a57a16171594b4da13890b",
  measurementId: "G-W0MVMRDP2C",
};
