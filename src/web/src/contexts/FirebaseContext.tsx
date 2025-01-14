import { initializeApp } from "firebase/app";
import {
  GoogleAuthProvider,
  getAuth,
  onAuthStateChanged,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
} from "firebase/auth";
import { ReactNode, createContext, useEffect, useReducer } from "react";
import { firebaseConfig } from "../contants";
import { CallApi, GetOSApiClient } from "../utils/apiHelpers";
import {
  ActionMap,
  AuthState,
  AuthUser,
  AuthUserApi,
  FirebaseContextType,
} from "./auth";

const firebaseApp = initializeApp({
  ...firebaseConfig,
  apiKey: process.env.REACT_APP_FIREBASE_API_KEY,
});

const AUTH = getAuth(firebaseApp);

const initialState: AuthState = {
  isAuthenticated: false,
  isInitialized: false,
  user: null,
  userApi: null,
};

enum Types {
  Initial = "INITIALISE",
}

type FirebaseAuthPayload = {
  [Types.Initial]: {
    isAuthenticated: boolean;
    user: AuthUser;
    userApi: AuthUserApi;
  };
};

type FirebaseActions =
  ActionMap<FirebaseAuthPayload>[keyof ActionMap<FirebaseAuthPayload>];

const reducer = (state: AuthState, action: FirebaseActions) => {
  if (action.type === "INITIALISE") {
    const { isAuthenticated, user, userApi } = action.payload;
    return {
      ...state,
      isAuthenticated,
      isInitialized: true,
      user,
      userApi,
    };
  }

  return state;
};

const AuthContext = createContext<FirebaseContextType | null>(null);

type AuthProviderProps = {
  children: ReactNode;
};

function AuthProvider({ children }: AuthProviderProps) {
  const [state, dispatch] = useReducer(reducer, initialState);

  useEffect(
    () =>
      onAuthStateChanged(AUTH, async (user) => {
        if (user) {
          await CallApi(
            GetOSApiClient((user as AuthUser)?.accessToken).getUser()
          )
            .then((data) =>
              dispatch({
                type: Types.Initial,
                payload: {
                  isAuthenticated: true,
                  user: user,
                  userApi: data,
                },
              })
            )
            .catch((err) => {
              dispatch({
                type: Types.Initial,
                payload: {
                  isAuthenticated: false,
                  user: null,
                  userApi: null,
                },
              });
            });
        } else {
          dispatch({
            type: Types.Initial,
            payload: {
              isAuthenticated: false,
              user: null,
              userApi: null,
            },
          });
        }
      }),
    [dispatch]
  );

  const login = (email: string, password: string) =>
    signInWithEmailAndPassword(AUTH, email, password);

  const loginGoogle = () => {
    let provider = new GoogleAuthProvider();
    provider.addScope("email");
    provider.addScope("profile");
    return signInWithPopup(AUTH, provider);
  };

  const logout = () => signOut(AUTH);

  return (
    <AuthContext.Provider
      value={{
        ...state,
        method: "firebase",
        user: {
          id: state?.user?.uid,
          email: state?.user?.email,
          accessToken: state?.user?.accessToken,
        },
        userApi: state?.userApi,
        login,
        loginGoogle,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { AuthContext, AuthProvider };
