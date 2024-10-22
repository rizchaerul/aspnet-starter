import { NextPageWithLayout } from "@/types/layout";

const IndexPage: NextPageWithLayout = () => {
    return (
        <div>
            <h1 className="text-3xl font-bold underline">Hello world!</h1>
            {/* <Button>Button</Button> */}
        </div>
    );
};

// IndexPage.getLayout = TODO
export default IndexPage;
