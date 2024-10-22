import { NextPage } from "next";
import { ReactElement, ReactNode } from "react";

/**
 * Custom page type for layout support
 * See: https://nextjs.org/docs/basic-features/layouts
 */
export type NextPageWithLayout<P = {}, IP = P> = NextPage<P, IP> & {
    getLayout?: (page: ReactElement) => ReactNode;
};
