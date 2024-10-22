/** @type {import('next').NextConfig} */
const nextConfig = {
    experimental: {
        typedRoutes: true,
    },
    pageExtensions: ["page.tsx"],
};

module.exports = nextConfig;
