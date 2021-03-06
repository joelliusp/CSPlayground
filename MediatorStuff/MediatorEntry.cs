﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MediatR;
using System.IO;

namespace MediatorStuff
{
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Planning.Bindings.Resolvers;
    using NinSyn = Ninject.Extensions.Conventions.Syntax;

    public class MediatorEntry
    {
        public static void Main()
        {
            DumbMediator dm = new DumbMediator();
            dm.DumbMediate();
            //IMediator m = BuildMediator();
            //Task<string> task = m.Send(new Ping());
            //task.Wait();
            //Console.WriteLine(task.Result);
            //Task t = m.Publish(new PingNotice());
            //t.Wait();
        }


        public static IMediator BuildMediator()
        {
            IKernel kernel = new StandardKernel();
            kernel.Components.Add<IBindingResolver, ContravariantBindingResolver>();
            kernel.Bind((scan) => {
                NinSyn.IIncludingNonPublicTypesSelectSyntax fromAsm = scan.FromAssemblyContaining<IMediator>();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax allClasses = fromAsm.SelectAllClasses();
                allClasses.BindDefaultInterface();
            });
            kernel.Bind<TextWriter>().ToConstant(Console.Out);
            kernel.Bind((scan) => {
                NinSyn.IIncludingNonPublicTypesSelectSyntax fromAsm = scan.FromAssemblyContaining<Ping>();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax allClasses = fromAsm.SelectAllClasses();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax inherited = allClasses.InheritedFrom(typeof(IRequestHandler<,>));
                inherited.BindAllInterfaces();
            });
            kernel.Bind((scan) => {
                NinSyn.IIncludingNonPublicTypesSelectSyntax fromAsm = scan.FromAssemblyContaining<Ping>();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax allClasses = fromAsm.SelectAllClasses();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax inherited = allClasses.InheritedFrom(typeof(IRequestHandler<>));
                inherited.BindAllInterfaces();
            });
            kernel.Bind(scan => {
                NinSyn.IIncludingNonPublicTypesSelectSyntax fromAsm = scan.FromAssemblyContaining<PingNotice>();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax allClasses = fromAsm.SelectAllClasses();
                NinSyn.IJoinFilterWhereExcludeIncludeBindSyntax inherited = allClasses.InheritedFrom(typeof(INotificationHandler<>));
                inherited.BindAllInterfaces();
            });

            //kernel.Bind(typeof(IPipelineBehavior<,>)).To(typeof(RequestPreProcessorBehavior<,>));
            //kernel.Bind(typeof(IPipelineBehavior<,>)).To(typeof(RequestPostProcessorBehavior<,>));
            //kernel.Bind(typeof(IPipelineBehavior<,>)).To(typeof(GenericPipelineBehavior<,>));
            //kernel.Bind(typeof(IRequestPreProcessor<>)).To(typeof(GenericRequestPreProcessor<>));
            //kernel.Bind(typeof(IRequestPostProcessor<,>)).To(typeof(GenericRequestPostProcessor<,>));


            kernel.Bind<SingleInstanceFactory>().ToMethod(ctx => {
                return t => {
                    var something = ctx.Kernel.TryGet(t);
                    return something;
                };
            });

            kernel.Bind<MultiInstanceFactory>().ToMethod(ctx => {
                return t => {
                    var allSomethings = ctx.Kernel.GetAll(t);
                    return allSomethings;
                };
            });

            IMediator m = kernel.Get<IMediator>();

            return m;
        }


    }
}
