﻿using AgricultureManager.Core.Application.Shared.Models;
using MediatR;

namespace AgricultureManager.Core.Application.Shared.Interfaces.Mediator
{
    public interface IReq : IRequest<ResponseLess> { }

    public interface IReq<T> : IRequest<Response<T>> { }

    public interface IReqHandler<TIn> : IRequestHandler<TIn, ResponseLess>
        where TIn : IReq
    { }

    public interface IReqHandler<TIn, TOut> : IRequestHandler<TIn, Response<TOut>>
        where TIn : IReq<TOut>
    { }


}
