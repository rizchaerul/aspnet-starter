import { NextPageWithLayout } from "@/types/layout";
import { AppProps } from "next/app";
import { Fragment, useEffect, useState } from "react";

import "@/styles/globals.css";

type AppPropsWithLayout = AppProps & {
    Component: NextPageWithLayout;
};

export default function MyApp({ Component, pageProps }: AppPropsWithLayout) {
    const [isReady, setIsReady] = useState(false);
    const getLayout = Component.getLayout ?? ((page) => page);

    useEffect(() => {
        setIsReady(true);
    }, []);

    return <Fragment>{isReady && getLayout(<Component {...pageProps} />)}</Fragment>;
}
