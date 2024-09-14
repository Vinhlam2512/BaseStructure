/** @type {import('next').NextConfig} */
const nextConfig = {
  async redirects() {
    return [
      {
        source: '/',
        destination: '/home',
        permanent: false // Set to true for a permanent redirect (HTTP 308)
      }
    ];
  }
};

export default nextConfig;
