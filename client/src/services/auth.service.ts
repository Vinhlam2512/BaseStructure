import axios from 'axios';
import AxiosService from '../axios/axiosInstance';
import Cookies from 'js-cookie';

const AuthService = {
  async Login(username: string, password: string): Promise<Token> {
    const data = {
      username: username,
      password: password
    };

    try {
      const response = await AxiosService.post<HttpResponse<Token>>(
        'api/v1/auth',
        data
      );
      const token: Token = response.data.value;
      Cookies.set('accessToken', token.accessToken);
      Cookies.set('refreshToken', token.refreshToken);
      return token;
    } catch (err: any) {
      console.log(err);
      throw new Error(err.detail);
    }
  },

  async RefreshToken(): Promise<Token> {
    const refreshToken = Cookies.get('refreshToken');
    const data = {
      accessToken: '',
      refreshToken: refreshToken
    };

    try {
      const response = await axios.post<HttpResponse<Token>>(
        `${process.env.NEXT_PUBLIC_BASE_URL_API}api/v1/token/refresh`,
        data
      );
      const token: Token = response.data.value;
      Cookies.set('accessToken', token.accessToken);
      Cookies.set('refreshToken', token.refreshToken);
      return token;
    } catch (err: any) {
      console.log(err);
      throw new Error(err.detail);
    }
  }
};

export default AuthService;
