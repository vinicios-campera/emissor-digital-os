import { UserCredential } from "firebase/auth";
import { UserResponse } from "../api/order-service-api";

export type ActionMap<M extends { [index: string]: any }> = {
  [Key in keyof M]: M[Key] extends undefined
    ? {
        type: Key;
      }
    : {
        type: Key;
        payload: M[Key];
      };
};

export type AuthUser = null | Record<string, any>;
export type AuthUserApi = null | UserResponse;

export type AuthState = {
  isAuthenticated: boolean;
  isInitialized: boolean;
  user: AuthUser;
  userApi: AuthUserApi;
};

export type FirebaseContextType = {
  isAuthenticated: boolean;
  isInitialized: boolean;
  user: AuthUser;
  userApi: AuthUserApi;
  method: "firebase";
  login: (email: string, password: string) => Promise<UserCredential>;
  loginGoogle: () => Promise<UserCredential>;
  logout: () => Promise<void>;
};
