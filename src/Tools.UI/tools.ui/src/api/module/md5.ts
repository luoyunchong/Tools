import request from '@/utils/axios';

enum MD5Digit{
  digit16 = 16,
  digit32 = 32
}
interface MD5Input {
  sourceString: string;
  base64: boolean;
  digit: MD5Digit;
  capital: boolean;
}


export const md5 = (md5input:MD5Input) => {
    return request<string>({
        url: '/api/md5',
        method: 'get',
        params: md5input
    });
};
export const allmd5 = (md5input:string)=> {
  return request<Md5Output>({
      url: '/api/md5/all',
      method: 'get',
      params: {sourceString:md5input}
  });
};
