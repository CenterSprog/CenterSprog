// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/ClassEntity.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace gRPCClient {
  public static partial class ClassEntityService
  {
    static readonly string __ServiceName = "ClassEntityService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestCreateClassEntity> __Marshaller_RequestCreateClassEntity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestCreateClassEntity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseCreateClassEntity> __Marshaller_ResponseCreateClassEntity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseCreateClassEntity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestGetClassEntity> __Marshaller_RequestGetClassEntity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestGetClassEntity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseGetClassEntity> __Marshaller_ResponseGetClassEntity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseGetClassEntity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestGetClassEntities> __Marshaller_RequestGetClassEntities = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestGetClassEntities.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseGetClassEntities> __Marshaller_ResponseGetClassEntities = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseGetClassEntities.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestUpdateClassParticipants> __Marshaller_RequestUpdateClassParticipants = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestUpdateClassParticipants.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseUpdateClassParticipants> __Marshaller_ResponseUpdateClassParticipants = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseUpdateClassParticipants.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestGetClassParticipants> __Marshaller_RequestGetClassParticipants = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestGetClassParticipants.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseGetClassParticipants> __Marshaller_ResponseGetClassParticipants = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseGetClassParticipants.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestGetClassAttendance> __Marshaller_RequestGetClassAttendance = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestGetClassAttendance.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseGetClassAttendance> __Marshaller_ResponseGetClassAttendance = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseGetClassAttendance.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.RequestGetClassAttendanceByUsername> __Marshaller_RequestGetClassAttendanceByUsername = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.RequestGetClassAttendanceByUsername.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCClient.ResponseGetClassAttendanceByUsername> __Marshaller_ResponseGetClassAttendanceByUsername = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCClient.ResponseGetClassAttendanceByUsername.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestCreateClassEntity, global::gRPCClient.ResponseCreateClassEntity> __Method_createClassEntity = new grpc::Method<global::gRPCClient.RequestCreateClassEntity, global::gRPCClient.ResponseCreateClassEntity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "createClassEntity",
        __Marshaller_RequestCreateClassEntity,
        __Marshaller_ResponseCreateClassEntity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestGetClassEntity, global::gRPCClient.ResponseGetClassEntity> __Method_getClassEntityById = new grpc::Method<global::gRPCClient.RequestGetClassEntity, global::gRPCClient.ResponseGetClassEntity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getClassEntityById",
        __Marshaller_RequestGetClassEntity,
        __Marshaller_ResponseGetClassEntity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestGetClassEntities, global::gRPCClient.ResponseGetClassEntities> __Method_getClassEntities = new grpc::Method<global::gRPCClient.RequestGetClassEntities, global::gRPCClient.ResponseGetClassEntities>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getClassEntities",
        __Marshaller_RequestGetClassEntities,
        __Marshaller_ResponseGetClassEntities);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestUpdateClassParticipants, global::gRPCClient.ResponseUpdateClassParticipants> __Method_updateParticipants = new grpc::Method<global::gRPCClient.RequestUpdateClassParticipants, global::gRPCClient.ResponseUpdateClassParticipants>(
        grpc::MethodType.Unary,
        __ServiceName,
        "updateParticipants",
        __Marshaller_RequestUpdateClassParticipants,
        __Marshaller_ResponseUpdateClassParticipants);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestGetClassParticipants, global::gRPCClient.ResponseGetClassParticipants> __Method_getClassParticipants = new grpc::Method<global::gRPCClient.RequestGetClassParticipants, global::gRPCClient.ResponseGetClassParticipants>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getClassParticipants",
        __Marshaller_RequestGetClassParticipants,
        __Marshaller_ResponseGetClassParticipants);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestGetClassAttendance, global::gRPCClient.ResponseGetClassAttendance> __Method_getClassAttendance = new grpc::Method<global::gRPCClient.RequestGetClassAttendance, global::gRPCClient.ResponseGetClassAttendance>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getClassAttendance",
        __Marshaller_RequestGetClassAttendance,
        __Marshaller_ResponseGetClassAttendance);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCClient.RequestGetClassAttendanceByUsername, global::gRPCClient.ResponseGetClassAttendanceByUsername> __Method_getClassAttendanceByUsername = new grpc::Method<global::gRPCClient.RequestGetClassAttendanceByUsername, global::gRPCClient.ResponseGetClassAttendanceByUsername>(
        grpc::MethodType.Unary,
        __ServiceName,
        "getClassAttendanceByUsername",
        __Marshaller_RequestGetClassAttendanceByUsername,
        __Marshaller_ResponseGetClassAttendanceByUsername);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::gRPCClient.ClassEntityReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for ClassEntityService</summary>
    public partial class ClassEntityServiceClient : grpc::ClientBase<ClassEntityServiceClient>
    {
      /// <summary>Creates a new client for ClassEntityService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ClassEntityServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ClassEntityService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ClassEntityServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ClassEntityServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ClassEntityServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseCreateClassEntity createClassEntity(global::gRPCClient.RequestCreateClassEntity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return createClassEntity(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseCreateClassEntity createClassEntity(global::gRPCClient.RequestCreateClassEntity request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_createClassEntity, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseCreateClassEntity> createClassEntityAsync(global::gRPCClient.RequestCreateClassEntity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return createClassEntityAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseCreateClassEntity> createClassEntityAsync(global::gRPCClient.RequestCreateClassEntity request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_createClassEntity, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassEntity getClassEntityById(global::gRPCClient.RequestGetClassEntity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassEntityById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassEntity getClassEntityById(global::gRPCClient.RequestGetClassEntity request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getClassEntityById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassEntity> getClassEntityByIdAsync(global::gRPCClient.RequestGetClassEntity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassEntityByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassEntity> getClassEntityByIdAsync(global::gRPCClient.RequestGetClassEntity request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getClassEntityById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassEntities getClassEntities(global::gRPCClient.RequestGetClassEntities request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassEntities(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassEntities getClassEntities(global::gRPCClient.RequestGetClassEntities request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getClassEntities, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassEntities> getClassEntitiesAsync(global::gRPCClient.RequestGetClassEntities request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassEntitiesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassEntities> getClassEntitiesAsync(global::gRPCClient.RequestGetClassEntities request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getClassEntities, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseUpdateClassParticipants updateParticipants(global::gRPCClient.RequestUpdateClassParticipants request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return updateParticipants(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseUpdateClassParticipants updateParticipants(global::gRPCClient.RequestUpdateClassParticipants request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_updateParticipants, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseUpdateClassParticipants> updateParticipantsAsync(global::gRPCClient.RequestUpdateClassParticipants request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return updateParticipantsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseUpdateClassParticipants> updateParticipantsAsync(global::gRPCClient.RequestUpdateClassParticipants request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_updateParticipants, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassParticipants getClassParticipants(global::gRPCClient.RequestGetClassParticipants request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassParticipants(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassParticipants getClassParticipants(global::gRPCClient.RequestGetClassParticipants request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getClassParticipants, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassParticipants> getClassParticipantsAsync(global::gRPCClient.RequestGetClassParticipants request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassParticipantsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassParticipants> getClassParticipantsAsync(global::gRPCClient.RequestGetClassParticipants request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getClassParticipants, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassAttendance getClassAttendance(global::gRPCClient.RequestGetClassAttendance request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassAttendance(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassAttendance getClassAttendance(global::gRPCClient.RequestGetClassAttendance request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getClassAttendance, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassAttendance> getClassAttendanceAsync(global::gRPCClient.RequestGetClassAttendance request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassAttendanceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassAttendance> getClassAttendanceAsync(global::gRPCClient.RequestGetClassAttendance request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getClassAttendance, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassAttendanceByUsername getClassAttendanceByUsername(global::gRPCClient.RequestGetClassAttendanceByUsername request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassAttendanceByUsername(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::gRPCClient.ResponseGetClassAttendanceByUsername getClassAttendanceByUsername(global::gRPCClient.RequestGetClassAttendanceByUsername request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_getClassAttendanceByUsername, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassAttendanceByUsername> getClassAttendanceByUsernameAsync(global::gRPCClient.RequestGetClassAttendanceByUsername request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return getClassAttendanceByUsernameAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::gRPCClient.ResponseGetClassAttendanceByUsername> getClassAttendanceByUsernameAsync(global::gRPCClient.RequestGetClassAttendanceByUsername request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_getClassAttendanceByUsername, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override ClassEntityServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ClassEntityServiceClient(configuration);
      }
    }

  }
}
#endregion