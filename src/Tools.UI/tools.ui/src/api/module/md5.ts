import request from '@/utils/axios';

enum MD5Digit{
  Digit16 = 16,
  Digit32 = 32
}
interface MD5Input {
  SourceString: string;
  Base64: boolean;
  Digit: MD5Digit;
  Capital: boolean;
}

interface Md5Output{
  Md5Digit32:string,
  Md5Digit16:string,
  Md5Digit32Lower:string,
  Md5Digit16Lower:string,
  Md5Digit32Upper:string,
  Md5Digit16Upper:string,
  Base64Md5Digit32:string,
  Base64Md5Digit16:string
}
export const md5 = (md5input:MD5Input) => {
    return request<string>({
        url: '/api/md5',
        method: 'get',
        params: md5input
    });
};
export const allmd5 = (md5input:string) => {
  return request<Md5Output>({
      url: '/api/md5/all',
      method: 'get',
      params: md5input
  });
};
