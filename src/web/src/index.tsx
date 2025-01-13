// simplebar
import "simplebar/src/simplebar.css";

// i18n
import "./locales/i18n";

// lazy image
import "react-lazy-load-image-component/src/effects/black-and-white.css";
import "react-lazy-load-image-component/src/effects/blur.css";
import "react-lazy-load-image-component/src/effects/opacity.css";

import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFnsV3";
import { ptBR } from "@mui/x-date-pickers/locales";
import {
  CollapseDrawerProvider,
  SettingsProvider,
} from "@vinicios-campera/kernel-react";
import { ptBR as PTBR } from "date-fns/locale/pt-BR";
import ReactDOM from "react-dom/client";
import { HelmetProvider } from "react-helmet-async";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import { AuthProvider } from "./contexts/FirebaseContext";
import reportWebVitals from "./reportWebVitals";
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(
  <AuthProvider>
    <HelmetProvider>
      <LocalizationProvider
        dateAdapter={AdapterDateFns}
        adapterLocale={PTBR}
        localeText={
          ptBR.components.MuiLocalizationProvider.defaultProps.localeText
        }
      >
        <SettingsProvider>
          <CollapseDrawerProvider>
            <BrowserRouter>
              <App />
            </BrowserRouter>
          </CollapseDrawerProvider>
        </SettingsProvider>
      </LocalizationProvider>
    </HelmetProvider>
  </AuthProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
