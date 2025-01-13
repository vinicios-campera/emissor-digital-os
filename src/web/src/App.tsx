import {
  MotionLazyContainer,
  ProgressBarStyle,
  ScrollToTop,
  ThemeProvider,
} from "@vinicios-campera/kernel-react";
import { SnackbarProvider } from "notistack";
import Router from "./routes";

function App() {
  return (
    <MotionLazyContainer>
      <ThemeProvider>
        <SnackbarProvider
          anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
          dense
          maxSnack={5}
          preventDuplicate
        >
          <ProgressBarStyle />
          <ScrollToTop />
          <Router />
        </SnackbarProvider>
      </ThemeProvider>
    </MotionLazyContainer>
  );
}

export default App;
