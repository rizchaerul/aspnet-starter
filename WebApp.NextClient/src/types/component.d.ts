import { FC, ReactNode } from "react";

/**
 * Extend FC so that it have children by default
 */
export type FCWC<T = {}> = FC<T & { children?: ReactNode }>;
